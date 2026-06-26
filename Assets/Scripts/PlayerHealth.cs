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
        
    }
    protected override void OnEnable() //LivingEntity의 OnEnable()을 오버라이드하여 체력 슬라이더를 초기화
    {
        base.OnEnable();
    }

    public override void RestoreHealth(float newHealth) //LivingEntity의 RestoreHealth()를 오버라이드하여 체력 회복 시 체력 슬라이더를 업데이트
    {
        base.RestoreHealth(newHealth);
    }

    public override void onDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)//LivingEntity의 onDamage()를 오버라이드하여 체력 슬라이더를 업데이트
    {
        base.onDamage(damage, hitPoint, hitNormal);
    }

    public override void Die() //LivingEntity의 Die()를 오버라이드하여 사망 시 애니메이션과 오디오 재생
    {
        base.Die();
    }

    private void OnTriggerEnter(Collider other) //아이템과 충돌했을 때 호출되는 메서드
    {
        
    }
}
