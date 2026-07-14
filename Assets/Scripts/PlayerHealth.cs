using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    public Slider healthSlider; //체력을 표시할 UI 슬라이더

    public AudioClip deathClip; //사망 시 재생할 오디오 클립
    public AudioClip hitClip; //피격 시 재생할 오디오 클립
    public AudioClip itemPickupClip; //아이템 획득 시 재생할 오디오 클립

    private AudioSource playerAudioPlayer; //플레이어의 오디오 소스
    private Animator playerAnimator; //플레이어의 애니메이터

    private PlayerMovement playerMovement; //플레이어의 이동 스크립트
    private PlayerShooter playerShooter; //플레이어의 슈팅 스크립트

    private void Awake() //사용할 컴포넌트 가져오기
    {
        playerAnimator = GetComponent<Animator>();
        playerAudioPlayer = GetComponent<AudioSource>();

        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();
    }
    protected override void OnEnable() //LivingEntity의 OnEnable()을 오버라이드하여 체력 슬라이더를 초기화
    {
        base.OnEnable();

        healthSlider.gameObject.SetActive(true); //체력 슬라이더 활성화
        healthSlider.maxValue = startingHealth; //슬라이더 최대값 설정

        healthSlider.value = health; //슬라이더 현재값 설정

        playerMovement.enabled = true; //플레이어 이동 활성화
        playerShooter.enabled = true; //플레이어 슈팅 활성화
    }

    public override void RestoreHealth(float newHealth) //LivingEntity의 RestoreHealth()를 오버라이드하여 체력 회복 시 체력 슬라이더를 업데이트
    {
        base.RestoreHealth(newHealth);

        healthSlider.value = health; //슬라이더 현재값 업데이트
    }

    public override void onDamage(float damage, Vector3 hitPoint, Vector3 hitDirection)//LivingEntity의 onDamage()를 오버라이드하여 체력 슬라이더를 업데이트
    {
        if (!dead)
        {
            playerAudioPlayer.PlayOneShot(hitClip); //피격 시 피격 오디오 재생
        }
        base.onDamage(damage, hitPoint, hitDirection);

        healthSlider.value = health; //슬라이더 현재값 업데이트
    }

    public override void Die() //LivingEntity의 Die()를 오버라이드하여 사망 시 애니메이션과 오디오 재생
    {
        base.Die();

        healthSlider.gameObject.SetActive(false); //체력 슬라이더 비활성화

        playerAudioPlayer.PlayOneShot(deathClip); //사망 오디오 재생

        playerAnimator.SetTrigger("Die"); //사망 애니메이션 재생

        playerMovement.enabled = false; //플레이어 이동 비활성화
        playerShooter.enabled = false; //플레이어 슈팅 비활성화
    }

    private void OnTriggerEnter(Collider other) //아이템과 충돌했을 때 호출되는 메서드
    {
        if (!dead)
        {

            Iitem item = other.GetComponent<Iitem>(); //충돌한 오브젝트에서 IItem 인터페이스를 가져옴

            if (item != null) //아이템이 존재하면
            {
                item.Use(gameObject); //아이템 사용
                playerAudioPlayer.PlayOneShot(itemPickupClip); //아이템 획득 오디오 재생
            }
        }
    }
}
