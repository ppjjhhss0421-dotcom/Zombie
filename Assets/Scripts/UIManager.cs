using UnityEngine;
using UnityEngine.SceneManagement; //씬 관리 관련 코드 가져오기
using UnityEngine.UI; // UI 관련 코드 가져오기

public class UIManager : MonoBehaviour
{
    public static UIManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }
            return m_instance;
        }
    }

    private static UIManager m_instance; //싱글턴이 활당될 변수

    public Text ammoText; //탄약 UI 텍스트
    public Text scoreText; //점수 UI 텍스트
    public Text waveText; //웨이브 UI 텍스트
    public GameObject gameOverUI; //게임오버 UI

    public void UpdateAmmoText(int magAmmo, int remainAmmo)
    {
        ammoText.text = magAmmo + "/" + remainAmmo;
    }

    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }

    public void UpdateWaveText(int Wave, int count)
    {
        waveText.text = "Wave: " + Wave + " | Enemies Left: " + count;
    }

    public void SetActiveGameOverUI(bool active)
    {
        gameOverUI.SetActive(active);
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //현재 씬을 다시 로드
    }

    
}   