using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class dataController : MonoBehaviour {
    public static bool isready;
    public RankingController rankingcontroller;
    public void getHighScore()
    {
        Dictionary<string, string> data = new Dictionary<string, string>
        {
            {"nick_name",PlayerPrefs.GetString("nickname")}
        };
        POST(2, "http://52.41.1.215:3000/highscore", data);
    }
    public void requestRankingList()
    {
        GET(1, "http://52.41.1.215:3000/rankinglist");
    }
    public void updatePlaytime()
    {
        Dictionary<string, string> data = new Dictionary<string, string>
        {
            {"nick_name",PlayerPrefs.GetString("nickname")},
            {"play_time",PlayerPrefs.GetInt("playTime").ToString()}
        };
        POST(0, "http://52.41.1.215:3000/playtime", data);
    }
    public void updateEndtime()
    {
        Dictionary<string, string> data = new Dictionary<string, string>
        {
            {"nick_name",PlayerPrefs.GetString("nickname")},
        };
        POST(0, "http://52.41.1.215:3000/endtime", data);
    }
    public void updateStarttime()
    {
        Dictionary<string, string> data = new Dictionary<string, string>
        {
            {"nick_name",PlayerPrefs.GetString("nickname")},
        };
        POST(0, "http://52.41.1.215:3000/starttime", data);
    }
    public void updateHighScore(int score)
    {
        Dictionary<string, string> data = new Dictionary<string, string>
        {
            {"nick_name",PlayerPrefs.GetString("nickname")},
            {"score",score.ToString()}
        };
        POST(0, "http://52.41.1.215:3000/ranking", data);
    }
    public void joinGame(string name)
    {
        Dictionary<string, string> data = new Dictionary<string, string>
        {
            {"nick_name",name}
        };
        POST(0, "http://52.41.1.215:3000/join", data);
    }
    void Start()
    {
        /*Dictionary<string, string> data = new Dictionary<string, string>
        {
            {"user_id","42" },
            {"score","300" }
        };
        requestRankingList();*/
      
       
    }
    void GET(int id,string url)
    {
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(id,www));
    }
    void POST(int id,string url, Dictionary<string, string> post)
    {
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<string, string> post_arg in post)
        {
            form.AddField(post_arg.Key, post_arg.Value);
        }
        var www = new WWW(url, form);

        StartCoroutine(WaitForRequest(id,www));
    }
    IEnumerator WaitForRequest(int id,WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            switch (id)
            {
                case 1: rankingcontroller.showRankingList(www.text); break;
                case 2: rankingcontroller.setHighScore(www.text); isready = true; break;
            }
           // Debug.Log("WWW ok! : " + www.text);
           // PostCallBack(www.text);
        }
        else
        {
            Debug.Log("WWW Error : " + www.error);
        }
    }
}
