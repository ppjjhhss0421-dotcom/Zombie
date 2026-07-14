using UnityEngine;
using UnityEngine.AI;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items; // 스폰할 아이템 배열
    public Transform playerTransform; // 플레이어의 Transform

    public float maxDistance = 5f; // 아이템 스폰 최대 거리

    public float timeBetSpawnMax = 7f; // 아이템 스폰 간격
    public float timeBetSpawnMin = 2f; // 마지막 아이템 스폰 시간
    private float timeBetSpawn; // 마지막 아이템 스폰 시간

    private float lastSpawnTime; // 마지막 아이템 스폰 시간

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax); // 아이템 스폰 간격 초기화
        lastSpawnTime = 0; // 마지막 아이템 스폰 시간 초기화
    }

    // Update is called once per frame
    private void Update()
    {
        if(Time.time >= lastSpawnTime + timeBetSpawn && playerTransform != null) // 마지막 아이템 스폰 시간 + 아이템 스폰 간격이 현재 시간보다 작거나 같으면
        {
            lastSpawnTime = Time.time; // 마지막 아이템 스폰 시간 업데이트
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax); // 아이템 스폰 간격 랜덤으로 설정
            Spawn(); // 아이템 스폰  
        }
    }

    private void Spawn()
    {
        

        Vector3 spawnPosition = GetRandomPointOnNavMesh(playerTransform.position, maxDistance); // 플레이어 위치를 기준으로 랜덤한 위치를 가져옴
        spawnPosition += Vector3.up * 0.5f; // y축을 0.5만큼 올려서 아이템이 땅에 묻히지 않도록 함
      
        GameObject selectedItem = items[Random.Range(0, items.Length)]; // 랜덤한 아이템 선택
        GameObject item = Instantiate(selectedItem, spawnPosition, Quaternion.identity);
       
        Destroy(item, 5f); // 5초 후 아이템 제거
    }

    private Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        Vector3 randomPos = Random.insideUnitSphere * distance + center; // 중심점에서 distance 범위 내의 랜덤한 위치를 가져옴

        NavMeshHit hit;

        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas); // 랜덤한 위치를 NavMesh 상의 위치로 변환
        return hit.position; // 변환된 위치 반환
    }
}
