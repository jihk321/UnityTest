using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserSetting : MonoBehaviour
{
    public static UserSetting instance {
        get {
            if (m_instance == null)
                m_instance = FindObjectOfType<UserSetting>();
            return m_instance;
        }
    }
    private static UserSetting m_instance;
    [SerializeField] private InputField inputName;
    [SerializeField] private GameObject scoreboard;
    public int score {get; set;}
    // Start is called before the first frame update

    // private void Update() {
    //     if (Input.GetKeyDown(KeyCode.Return))
    //         setUser();
    // }

    public void SetShowInput() {
        scoreboard.SetActive(true);
    }
    private void setUser() {
        string playerName = inputName.text;
        PlayerPrefs.SetString("Player", playerName);
        Debug.Log(playerName);

    }

    private void SetScore(string name, float score) {
        PlayerPrefs.SetString("Player", name);
        PlayerPrefs.SetFloat("Score", score);
    }
}
