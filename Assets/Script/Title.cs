using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
    public GameObject rankingPanel;
    public GameObject settingPanel;
    public dataController datacontroller;
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
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
