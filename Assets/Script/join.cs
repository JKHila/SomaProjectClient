using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class join : MonoBehaviour {
    public dataController datacontroller;
    public InputField field;
    public GameObject joinPanel;
    public void OnEnterBtnDown()
    {
        PlayerPrefs.SetString("nickname",field.text);
        datacontroller.joinGame(field.text);
        joinPanel.SetActive(false);

    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
