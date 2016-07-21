using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RankingController : MonoBehaviour {
    public Text nameText;
    public Text scoreText;
    public Text highScore;
    public void setHighScore(string text)
    {
        JSONObject tempJson = new JSONObject(text);
        Debug.Log(text+","+tempJson);
        string score="";
        
        if (text == "[]")
            score = "0";
        else
            score = tempJson[0].GetField("score").ToString();
        PlayerPrefs.SetInt("highScore",int.Parse(score));
        highScore.text += "  " + PlayerPrefs.GetInt("highScore").ToString();
    }
    public void showRankingList(string text)
    {
        JSONObject tempJson = new JSONObject(text);
        nameText.text = "";
        scoreText.text = "";
        int count;
        if (tempJson.Count > 10) count = 10;
        else count = tempJson.Count;
        for (int i = 0; i < count; i++)
        {
            string tmpName = tempJson[i].GetField("nick_name").str + '\n';
            if (tmpName.Length > 9)
                tmpName = tmpName.Substring(0,7)+ "...\n";
            nameText.text += tmpName;
            scoreText.text += tempJson[i].GetField("score").ToString() +'\n';
           // nameText.text.Replace
            //Debug.Log(tempJson[i].Print(false));
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
