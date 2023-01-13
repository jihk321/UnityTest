using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using UnityEngine.UI; // UI 관련 코드
using System.Collections;
using System.Collections.Generic;

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
    public GameObject RankPrefab; // 텍스트 프리팹
    private List<GameObject> ranktext = new List<GameObject>();
    public GameObject RankBoard;
    public CanvasGroup reloadGroup;
    public int endscore;
    private bool checkbullet = true;

    // private string[] player = new string[100];
    // private int[] score = new int[100];
    private Dictionary<string, int> playerScore;
    // int index=0;
    
    private void Awake()
    {
        playerScore = new Dictionary<string, int>();
    }
    // 탄약 텍스트 갱신
    void Start()
    {
        LoadPlayerScoreData();
        ShowAllData();    
    }

    private void LoadPlayerScoreData()
    {
        int numberOfPlayer;

        if ( !PlayerPrefs.HasKey("PlayerCount") )
            PlayerPrefs.SetInt("PlayerCount", 0);

        numberOfPlayer = PlayerPrefs.GetInt("PlayerCount");

        for (int i = 0; i < numberOfPlayer; i++) {
            playerScore.Add(
                PlayerPrefs.GetString($"PlayerName_{i}"),
                PlayerPrefs.GetInt($"PlayerScore_{i}")
            );
        }
    }

    private void SavePlayerScoreData()
    {
        int index = 0;
        
        foreach (KeyValuePair<string, int> data in playerScore) {
            PlayerPrefs.SetString($"PlayerName_{index}", data.Key);
            PlayerPrefs.SetInt($"PlayerScore_{index}", data.Value);
            index++;
        }

        PlayerPrefs.SetInt("PlayerCount", index);

        PlayerPrefs.Save();
    }

    private void OnEnable() {
        
    }
    private void OnDisable() {
        
    }
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

    public void GameOvers() { // 게임 오버일 때 실행할 것들
        SetActiveGameoverUI(true);
        SetActiveCrosshair(false);
        scoreui.SetActive(true);
    }

    public void NameButton() {
        string username = inputscore.text;
        // Debug.Log(username);
        SetScore(username,endscore);
        // scoreui.SetActive(false);  // 이름 입력후 ui 사라짐.
        Rank();
        // GetScore();
    }

    public void ZeroBullet(int bullet){
        if (bullet == 0 && checkbullet == true && !GameManager.instance.isGameover){
            checkbullet = false;
            SetActiveCrosshair(false);
            StartCoroutine(SetReloadMessage(1.0f));
        }
        else if (bullet != 0){
            reloadtext.SetActive(false);
            SetActiveCrosshair(true);
            }
    }

    private IEnumerator SetReloadMessage(float time) {
        
        // Debug.Log("reload message part");
        reloadtext.SetActive(true);

        reloadGroup.alpha = 0;
        StartCoroutine(FadeIn(reloadGroup, time-0.2f));

        yield return new WaitForSeconds(time);

        StartCoroutine(FadeOut(reloadGroup, 0.5f));
        reloadtext.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        checkbullet = true;
    }
    // 게임 재시작
    public void GameRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SetScore(string player, int score) {
        // if ( !PlayerPrefs.HasKey("PlayerCount") )
        //     PlayerPrefs.SetInt("PlayerCount", 0);
        
        // index = PlayerPrefs.GetInt("PlayerCount");
        // for (int i = 0; i < index; i++ ) {
        //     saveName = PlayerPrefs.GetString($"PlayerName{i}");
        //     saveScore = PlayerPrefs.GetInt($"Score{i}");
            
        //     // 플레이어 이름이 등록되어 있으며 기존 점수보다 높을 때 갱신. 
        //     if (player == saveName && saveScore < score) { 
        //         PlayerPrefs.SetInt($"Score{i}", score);
        //         PlayerPrefs.Save();
        //         return ;
        //     } else if (player == saveName && saveScore > score) {
        //         return ;
        //     }

        //     Debug.Log("index : " + i);
        // }

        if ( playerScore.ContainsKey(player) && playerScore[player] < score) {
            playerScore[player] = score;
        }
        else if (!playerScore.ContainsKey(player)) {
            playerScore.Add(player, score);
        }

        SavePlayerScoreData();
    }

    public void ShowAllData() {
        foreach (KeyValuePair<string, int> data in playerScore) {
            Debug.Log($"name : {data.Key} score: {data.Value}");
        }
    }


    private void Rank() {
        RankBoard.SetActive(true);
        Debug.Log(playerScore.Count);
        for (int i = 0; i < playerScore.Count; i++) {
            GameObject Ranks = Instantiate(RankPrefab,RankBoard.transform);
            Text[] textlist = Ranks.GetComponentsInChildren<Text>();
            int ranknum = i + 1;
            
            textlist[0].text =  ranknum.ToString();
            textlist[1].text = PlayerPrefs.GetString($"PlayerName_{i}");
            textlist[2].text = PlayerPrefs.GetInt($"PlayerScore_{i}").ToString();
            ranktext.Add(Ranks);
        }
    }

    private IEnumerator FadeIn(CanvasGroup target, float sec) {
        float time = Mathf.Lerp(0f, 1f , sec * Time.deltaTime);
        while (target.alpha < 0.9f) {
            target.alpha += 0.1f;
            yield return new WaitForSeconds(time);
        }
    }
    private IEnumerator FadeOut(CanvasGroup target, float sec) {
        float time = Mathf.Lerp(0f, 1f , sec * Time.deltaTime);

        while (target.alpha > 0.1f) {
            target.alpha -= 0.1f;
            yield return new WaitForSeconds(time);
        }
    }

}