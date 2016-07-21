using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
    private float playTime = 0; 

    public GameObject rankingPanel;
    public GameObject settingPanel;
    public GameObject exitPanel;
    public GameObject logoPanel;

    public dataController datacontroller;
 
    public void OnRankingBackBtnDown()
    {
        rankingPanel.SetActive(false);
    }
    public void OnCloseYesBtnDown()
    {
        QuitGame();
    }
    public void OnCloseNoBtnDown()
    {
        Time.timeScale = 1;
        exitPanel.SetActive(false);
    }
    public void OnSettingBtnDown()
    {
        if (settingPanel.activeSelf)
            settingPanel.SetActive(false);
        else
        {
            settingPanel.SetActive(true);
        }
    }
    public void OnRankingBtnDown()
    {
        if (rankingPanel.activeSelf)
            rankingPanel.SetActive(false);
        else
        {
            rankingPanel.SetActive(true);
            datacontroller.requestRankingList();
        }
    }
    public void QuitGame()
    {
        playTime += PlayerPrefs.GetInt("playTime");
        PlayerPrefs.SetInt("playTime", (int)playTime);
        datacontroller.updatePlaytime();
        datacontroller.updateEndtime();
        Application.Quit();
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        playTime += Time.deltaTime;
        
        if (!logoPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        { //Quit game
            if (!exitPanel.activeSelf)
            {
                Time.timeScale = 0;
                exitPanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                exitPanel.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("delete");
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            QuitGame();
            //PlayerPrefs.DeleteAll();
        }
    }
}
