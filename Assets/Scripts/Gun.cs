using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum State
    {
        Ready, // 발사 준비됨
        Empty, // 탄창이 빔
        Reloading // 재장전 중
    }  

    public State state { get; private set; }// 현재 총의 상태

    public Transform fireTransform; // 총알이 발사될 위치

    public ParticleSystem muzzleFlashEffect; // 총구 화염 효과
    public ParticleSystem sheliEjectEffect; // 탄피 배출 효과

    private LineRenderer bulletLineRenderer; // 총알 궤적을 그리기 위한 LineRenderer

    private AudioSource gunAudioPlayer; // 총 소리를 재생하기 위한 AudioSource

    public GunDate gunDate; // 총의 데이터

    private float fireDistance = 50f; // 총알이 도달할 최대 거리

    public int ammoRemain = 100; // 남은 전체 탄알
    public int magAmmo; // 현재 탄창에 남아있는 탄알

    private float lastFireTime; // 마지막으로 총을 발사한 시간

    private void Awake()
    {
     gunAudioPlayer = GetComponent<AudioSource>(); // AudioSource 컴포넌트 가져오기
     bulletLineRenderer = GetComponent<LineRenderer>(); // LineRenderer 컴포넌트 가져오기
     bulletLineRenderer.positionCount = 2; // 총알 궤적을 그리기 위해 2개의 점이 필요
     bulletLineRenderer.enabled = false; // 처음에는 총알 궤적을 보이지 않도록 설정
    }

    private void OnEnable()
    {
     ammoRemain = gunDate.startAmmoRemain; // 전체 탄알 수 초기화
        magAmmo = gunDate.magCapacity; // 탄창에 남아있는 탄알 수 초기화

        state = State.Ready; // 총의 상태를 발사 준비로 설정
        lastFireTime = 0; // 마지막 발사 시간 초기화
    }


    public void Fire()
    {
      
    }

    private void Shot()
    {
      
    }

    private IEnumerator ShotEffect( Vector3 hitPosition)
    {
        bulletLineRenderer.enabled = true;

        yield return new WaitForSeconds(0.03f); // 총알 궤적을 잠시 보여줌

        bulletLineRenderer.enabled = false;

    }

    public bool Reload()
    {
        return true; // 재장전 시작
    }

    private IEnumerator ReloadRoutine()
    {
     state = State.Reloading; // 재장전 상태로 변경
       
     yield return new WaitForSeconds(gunDate.reloadTime); // 재장전 시간 대기

    state = State.Ready; // 재장전 완료 후 발사 준비 상태로 변경
  
    }
}
