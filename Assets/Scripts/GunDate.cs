using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GunDate", fileName = "GunDate")]
public class GunDate : ScriptableObject
{
    public AudioClip shootClip;  //발사 소리
    public AudioClip reloadClip; //재장전 소리

    public float damage=25; // 공격력

    public int startAmmoRemain=100; // 처음에 주어질 전체 탄알
    public int magCapacity=25; // 탄창 용량

    public float timeBestFire = 0.12f; //탄알발사 간격
    public float reloadTime = 1.8f; // 재장전 소요시간
}
