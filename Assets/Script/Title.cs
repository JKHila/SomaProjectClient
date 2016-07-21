using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
    private float playTime = 0; 

    public GameObject rankingPanel;
    public GameObject settingPanel;
    public GameObject exitPanel;

    public dataController datacontroller;
 
    public void OnCloseYesBtnDown()
    {
        QuitGame();
    }
    public void OnCloseNoBtnDown()
    {
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
        
        if (dataController.isready && Input.GetKeyDown(KeyCode.Escape))
        { //Quit game
            exitPanel.SetActive(true);
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
