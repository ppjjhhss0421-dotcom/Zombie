using System.Collections;
using UnityEngine;
using UnityEngine.AI;   // AI, 네비게이션 시스템 관련 코드 가져오기

// 좀비 AI 구현
public class Zombie : LivingEntity
{
    public LayerMask whatIsTarget;      // 추적 대상 레이어

    private LivingEntity targetEntity;  // 추적 대상
    private NavMeshAgent navMeshAgent;  // 경로 계산 AI 에이전트

    public ParticleSystem hitEffect;    // 피격 시 재생할 파티클 효과
    public AudioClip deathSound;        // 사망 시 재생할 소리
    public AudioClip hitSound;          // 피격 시 재생할 소리

    private Animator zombieAnimator;        // 애니메이터 컴포넌트
    private AudioSource zombieAudioPlayer;  // 오디오 소스 컴포넌트
    private Renderer zombieRenderer;        // 랜더러 컴포넌트

    public float damage = 20f;          // 공격력
    public float timeBatAttack = 0.5f;  // 공격 간격
    private float lastAttakTime;        // 마지막 공격 시점

    // 추적할 대상이 존재하는 알려주는 프로퍼티
    private bool hasTarget
    {
        get
        {
            // 추적할 대상이 존재하고, 대상이 사망하지 않았다면 true
            if(targetEntity != null && !targetEntity.dead)
            {
                return true;
            }

            // 그렇지 않다면 false
            return false;
        }
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();  // NavMeshAgent 컴포넌트 가져오기
        zombieAnimator = GetComponent<Animator>();// Animator 컴포넌트 가져오기
        zombieAudioPlayer = GetComponent<AudioSource>(); // AudioSource 컴포넌트 가져오기

        // 랜더러는 좀비의 자식 오브젝트에 존재하므로 GetComponentInChildren() 메서드로 가져와야 한다.
        zombieRenderer = GetComponentInChildren<Renderer>(); // Renderer 컴포넌트 가져오기
        // 초기화
    }

    // 좀비 AI의 초기 스펙을 결정하는 셋업 메서드
    public void Setup(ZombieData zombieData)
    {
        startingHealth = zombieData.health;  // 체력
        health = zombieData.health;          // 체력

        damage = zombieData.damage;          // 공격력
        navMeshAgent.speed = zombieData.speed;  // 이동 속도
        zombieRenderer.material.color = zombieData.skinColor;  // 스킨 색상
    }

    private void Start()
    {
        // 게임 오브젝트 활성화와 동시에 AI의 추적 루틴 시작
        StartCoroutine(UpdatePath());
    }
private void Update()
    {
        // 추적 대상의 존재 여부에 따라 다른 애니메이션 재생
        zombieAnimator.SetBool("HasTarget", hasTarget);
    }

    // 주기적으로 추적할 대상의 위치를 찾아 경로 갱신
    private IEnumerator UpdatePath()
    {
        // 살아 있는 동안 무한 루프
        while (!dead)
        {
            if(hasTarget)
            {
                navMeshAgent.isStopped = false; // 추적 대상이 존재하면 이동 시작
                navMeshAgent.SetDestination(targetEntity.transform.position); // 추적 대상의 위치를 목적지로 설정
            }
            else
            {
                navMeshAgent.isStopped = true; // 추적 대상이 존재하지 않으면 이동 중지

                Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, whatIsTarget); // 반경 20미터 내의 추적 대상 검색

                for(int i=0; i<colliders.Length; i++)
                {
                    LivingEntity livingEntity = colliders[i].GetComponent<LivingEntity>(); // 추적 대상의 LivingEntity 컴포넌트 가져오기
                    if(livingEntity != null && !livingEntity.dead) // 추적 대상이 존재하고, 사망하지 않았다면
                    {
                        targetEntity = livingEntity; // 추적 대상으로 설정
                        break; // 반복문 종료
                    }
                }
            }
            // 0.25초 주기로 처리 반복
            yield return new WaitForSeconds(0.25f);
        }
    }

    // 대미지를 입었을 때 실행할 처리
    public override void onDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if(!dead) // 살아 있는 상태에서만 처리
        {
            // 피격 효과 파티클 생성
            hitEffect.transform.position = hitPoint; // 피격 위치로 이동
            hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal); // 피격 방향으로 회전
            hitEffect.Play(); // 파티클 재생
            // 피격 효과음 재생
            zombieAudioPlayer.PlayOneShot(hitSound);
        }
        // LivingEntity의 OnDamage()를 실행하여 대미지 적용
        base.onDamage(damage, hitPoint, hitNormal);
    }

    // 사망 처리
    public override void Die()
    {
        // LivingEntity의 Die()를 실행하여 기본 사망 처리 실행
        base.Die();

        Collider[] zombieColliders = GetComponents<Collider>(); // 좀비의 모든 콜라이더 가져오기
        for(int i=0; i<zombieColliders.Length; i++)
        {
            zombieColliders[i].enabled = false; // 콜라이더 비활성화
        }

        navMeshAgent.isStopped = true; // 이동 중지
        navMeshAgent.enabled = false; // NavMeshAgent 비활성화

        zombieAnimator.SetTrigger("Die"); // 사망 애니메이션 재생

        zombieAudioPlayer.PlayOneShot(deathSound); // 사망 효과음 재생
    }

    private void OnTriggerStay(Collider other)
    {
        // 트리거 충돌한 상대방 게임 오브젝트가 추적 대상이라면 공격 실행

        if(!dead && Time.time >= lastAttakTime + timeBatAttack)
        {
            LivingEntity attackTarget = other.GetComponent<LivingEntity>(); // 충돌한 상대방의 LivingEntity 컴포넌트 가져오기
            if(attackTarget != null && attackTarget == targetEntity) // 추적 대상과 동일한지 확인
            {
                lastAttakTime = Time.time; // 마지막 공격 시점 갱신
                Vector3 hitPoint = other.ClosestPoint(transform.position); // 충돌 지점 계산
                Vector3 hitNormal = transform.position - other.transform.position; // 충돌 방향 계산
                attackTarget.onDamage(damage, hitPoint, hitNormal); // 추적 대상에게 대미지 적용
            }
        }
    }
}