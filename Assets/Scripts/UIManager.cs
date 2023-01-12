using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using UnityEngine.UI; // UI 관련 코드
using System.Collections;

// 필요한 UI에 즉시 접근하고 변경할 수 있도록 허용하는 UI 매니저
public class UIManager : MonoBehaviour {
    // 싱글톤 접근용 프로퍼티
    public static UIManager instance
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

    private static UIManager m_instance; // 싱글톤이 할당될 변수

    public Text ammoText; // 탄약 표시용 텍스트
    public Text scoreText; // 점수 표시용 텍스트
    public Text waveText; // 적 웨이브 표시용 텍스트
    public GameObject gameoverUI; // 게임 오버시 활성화할 UI 
    public GameObject crosshair; // 크로스헤어 
    public GameObject reloadtext; // 리로드 메세지 
    private Coroutine routine;

    public GameObject scoreui;
    public InputField inputscore;
    private bool checkbullet = true;

    // 탄약 텍스트 갱신
    public void UpdateAmmoText(int magAmmo, int remainAmmo) {
        ammoText.text = magAmmo + "/" + remainAmmo;
    }

    // 점수 텍스트 갱신
    public void UpdateScoreText(int newScore) {
        scoreText.text = "Score : " + newScore;
    }

    // 적 웨이브 텍스트 갱신
    public void UpdateWaveText(int waves, int count) {
        waveText.text = "Wave : " + waves + "\nEnemy Left : " + count;
    }

    // 게임 오버 UI 활성화
    public void SetActiveGameoverUI(bool active) {
        gameoverUI.SetActive(active);
    }

    public void SetActiveCrosshair(bool active) {
        crosshair.SetActive(active);
    }

    public void GameOvers() {
        SetActiveGameoverUI(true);
        SetActiveCrosshair(false);
        scoreui.SetActive(true);

    }

    public void ZeroBullet(int bullet){
        if (bullet == 0 && checkbullet == true && !GameManager.instance.isGameover){
            checkbullet = false;
            SetActiveCrosshair(false);
            StartCoroutine(SetReloadMessage(1.0f));
        }
        else if (bullet != 0)
            SetActiveCrosshair(true);
    }

    private IEnumerator SetReloadMessage(float time) {
        
        Debug.Log("reload message part");
        reloadtext.SetActive(true);
        yield return new WaitForSeconds(time);
        reloadtext.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        checkbullet = true;
    }
    // 게임 재시작
    public void GameRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}