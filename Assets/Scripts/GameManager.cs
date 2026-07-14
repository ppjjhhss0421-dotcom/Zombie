using UnityEngine;

public class GameManager : MonoBehaviour
{
  
    public static GameManager Instance
        {
          get
            {
                if (m_Instance == null)
                {
                    m_Instance = FindObjectOfType<GameManager>();
                  
                }
                return m_Instance;
            }
        }

    private static GameManager m_Instance; // 싱글톤이 할당될 변수

    private int score = 0; // 점수
    public bool IsGameOver { get; private set; } // 게임 오버 상태
    
    private void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance != this)
        {
          Destroy(gameObject);
        }
        
    }

 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindAnyObjectByType<PlayerHealth>().onDeath += EndGame; // 플레이어가 죽으면 endGame 메서드 호출

    }

    public void AddScore(int newScore)
    {
        if (!IsGameOver)
        {
            score += newScore; // 점수 증가
            UIManager.Instance.UpdateScoreText(score); // UI 업데이트
        }
    }

    public void EndGame()
            {
        IsGameOver = true; // 게임 오버 상태로 변경
        UIManager.Instance.SetActiveGameOverUI(true); // 게임 오버 UI 활성화
    }

   
}
