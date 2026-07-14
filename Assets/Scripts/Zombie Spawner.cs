using UnityEngine;
using System.Collections.Generic;

public class ZombieSpawner : MonoBehaviour
{
    public Zombie zombieprefab; // 생성할 좀비 프리팹

    public ZombieData[] zombieDatas; // 좀비 데이터
    public Transform[] spawnPoints; // 생성 지점

    private List<Zombie> zombies = new List<Zombie>(); // 생성된 좀비 리스트
    private int wave; // 현재 웨이브

    // Update is called once per frame
    private void Update()
    {
        if(GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            return;
        }

        if (zombies.Count == 0)
        {
           
            SpawnWave();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        UIManager.Instance.UpdateWaveText(wave, zombies.Count);
      
    }

    private void SpawnWave()
    {
      wave++;

        int spawnCount = Mathf.RoundToInt(wave * 1.5f); // 웨이브에 따라 생성할 좀비 수 결정

        for (int i = 0; i < spawnCount; i++)
        {
            CreateZombie();
        }
    }

    private void CreateZombie()
    {
        ZombieData zombieData = zombieDatas[Random.Range(0, zombieDatas.Length)]; // 랜덤으로 좀비 데이터 선택
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; // 랜덤으로 생성 지점 선택
        Zombie zombie = Instantiate(zombieprefab, spawnPoint.position, spawnPoint.rotation); // 좀비 생성

        zombie.Setup(zombieData); // 좀비 데이터 설정
        zombies.Add(zombie); // 생성된 좀비 리스트에 추가

        zombie.onDeath += () => zombies.Remove(zombie); // 좀비가 죽으면 리스트에서 제거
        zombie.onDeath += () => Destroy(zombie.gameObject, 10f); // 좀비가 죽으면 게임 오브젝트 제거
        zombie.onDeath += () => GameManager.Instance.AddScore(100); // 좀비가 죽으면 점수 추가
    }

}
