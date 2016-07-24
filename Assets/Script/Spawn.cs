using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames;
public class Spawn : MonoBehaviour { 
    public static bool isfirst = true;

    private GameObject curObj;
    private GameObject preObj;
    private GameObject topObj;
    private GameObject tpObj;
    private bool isPut = false;
    private bool isCameraMove = false;
    private int numOfCat = 0;
    private float center = 0;
    private int totalScore = 0;
    private Vector3 preObjPos;
    private float windPower;
    public bool isStart = false;
    public bool isGameover = false;
    
    private float direction;
    public float Dvalue;
    public GameObject[] cat = new GameObject[4];
    public GameObject[] background = new GameObject[4];
    public AudioClip[] catMeow = new AudioClip[4];
    public GameObject newBackground;
    public GameObject firstCat;
    public GameObject gradiant;
    public GameObject grass;
    public GameObject windIcon;
    public GameObject gameoverPanel;
    public GameObject joinPanel;
    public GameObject logoPanel;
   // public GameObject settingPanel;
    
    public GPGSMng gpgsmng;
    public Text scoreText;
    public Text titleScoreText;
    public Text highScore;
    public Text floorText;
    public Text windText;
    public Image myImg;
    public Text nameText;
    public Text logining;
   
    public Toggle SEToggle;
    public Sprite windsp;

    public dataController datacontroller;

    IEnumerator waitLogin()
    {
        yield return new WaitUntil(()=>GPGSMng.logined);
        //yield return new WaitForSeconds(2.0f);
        setUser();
        if (PlayerPrefs.GetString("nickname") != "noname")
        {
            logining.text = "유저 정보 받는중...";
            yield return new WaitUntil(() => dataController.isready);
        }else
        {
            logining.text = "로그인에 실패해서\n랭킹에 등록할 수 없습니다.";
        }
        yield return new WaitForSeconds(1.0f);
        logoPanel.SetActive(false);
    }
    IEnumerator moveBack()
    {
        isCameraMove = true;
        preObjPos = Camera.main.WorldToViewportPoint(preObj.transform.position);
        yield return new WaitUntil(() => preObjPos.y < 0.0f);
        if(newBackground.transform.position.y -Camera.main.transform.position.y < 5)
        {
            int n = Random.Range(0, 4);
            newBackground = Instantiate(background[n], new Vector2(0, newBackground.transform.position.y+10), transform.rotation) as GameObject;
        }

        isCameraMove = false;
        readyCat();
    }
    public void setUser()
    {
        if (gpgsmng.GetNameGPGS() == null)
        {
            PlayerPrefs.SetString("nickname", "noname");
        }
        else
        {
            PlayerPrefs.SetString("nickname", gpgsmng.GetNameGPGS());
            datacontroller.updateStarttime();
            datacontroller.getHighScore();
        }
        nameText.text = "Name : " + PlayerPrefs.GetString("nickname");
        /*Texture2D tmpTexture = gpgsmng.GetImageGPGS();
        if (tmpTexture)
            myImg.sprite = Sprite.Create(tmpTexture, new Rect(0, 0, tmpTexture.width, tmpTexture.height), new Vector2(0, 0));
        */
    }
    

    public void Replay()
    {
        isfirst = false;
        SceneManager.LoadScene("main");
    }

    public void Gameover()
    {
        isGameover = true;
        if (isGameover)
        {
            scoreText.gameObject.SetActive(false);
            gameoverPanel.SetActive(true);
            //scoreText.transform.position = new Vector2(scoreText.transform.position.x, scoreText.transform.position.y - 160);
            if(PlayerPrefs.GetString("nickname") != "noname" && PlayerPrefs.GetInt("highScore") < totalScore)
            {
                PlayerPrefs.SetInt("highScore", totalScore);
                datacontroller.updateHighScore(PlayerPrefs.GetInt("highScore"));
            }
            highScore.text += "  " + PlayerPrefs.GetInt("highScore").ToString();
            titleScoreText.gameObject.SetActive(true);
            titleScoreText.text = totalScore.ToString();
        }
    }

    public void collCheck(GameObject obj,float sub,int score)
    {
        center += sub;
        gradiant.transform.rotation = Quaternion.Euler(0, 0, center * -25);

        tpObj = obj;
        
        StartCoroutine(moveBack());

        if (gradiant.transform.localEulerAngles.z > 90 && gradiant.transform.localEulerAngles.z < 270 && !isGameover)
            Gameover();

        totalScore += score;
        scoreText.text = totalScore.ToString();
    }
           
    public void readyCat()//cat생성
    {
        if (!isGameover)
        {
            numOfCat++;
            floorText.text = numOfCat + "F";
            Dvalue += 0.1f;
            preObj = topObj;
            topObj = tpObj;
            isPut = false;
            
            //고양이생성
            int n = Random.Range(0, 4);
            float m = Random.Range(-2.3f, 2.3f);
            transform.position = new Vector2(m, transform.position.y);
            curObj = Instantiate(cat[n], new Vector2(transform.position.x, transform.position.y - 1f), transform.rotation) as GameObject;
            if(direction > 0)
                curObj.GetComponent<SpriteRenderer>().flipX = true;
            //바람표시
            n = Random.Range(0, 5);
            if(n < 2)
            {
                int p = Random.Range(-3, 4);
                windPower = p * 10;
                windText.text = Mathf.Abs(windPower) + "\nWind";

                if (p > 0)
                {
                    windIcon.GetComponent<SpriteRenderer>().sprite = windsp;
                    windIcon.GetComponent<SpriteRenderer>().flipX = false;
                }
                else if (p < 0)
                {
                    windIcon.GetComponent<SpriteRenderer>().sprite = windsp;
                    windIcon.GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                    windIcon.GetComponent<SpriteRenderer>().sprite = null;
            }
            else
            {
                windPower = 0;
                windText.text = 0 + "\nWind";
                windIcon.GetComponent<SpriteRenderer>().sprite = null;
            }
            //curObj.transform.SetParent(background.transform);
        }
    }
    // Use this for initialization
    void Start () {
        //앱처음실행시만
        if(PlayerPrefs.GetInt("isFirst")== 0)
        {
            
            PlayerPrefs.SetInt("isFirst",1);
        }
        if (isfirst)
        {
            Debug.Log(Social.localUser.authenticated);
            logoPanel.SetActive(true);
            gpgsmng.InitializeGPGS();
            gpgsmng.LoginGPGS();
            StartCoroutine(waitLogin());

            string[] senderIds = { "225717214172" };
            GCM.Register(senderIds);
            gameoverPanel.SetActive(true);
            titleScoreText.gameObject.SetActive(false);
        }
        else
        {
            nameText.text += PlayerPrefs.GetString("nickname");
            scoreText.gameObject.SetActive(true);
            direction = Dvalue;
            readyCat();
            topObj = firstCat;
            preObj = grass;
        }
    }
	
	// Update is called once per frame
	void Update () {
        
        if (!isfirst)
        {
            if (Time.timeScale == 1&&!isGameover&&Input.GetMouseButtonDown(0))
            {
                int n = Random.Range(0, 4);
                
                AudioSource.PlayClipAtPoint(catMeow[n], transform.position);
                curObj.GetComponent<Rigidbody2D>().isKinematic = false;
                curObj.GetComponent<Rigidbody2D>().AddForce(Vector2.right * windPower * Time.deltaTime*1.1f, ForceMode2D.Impulse);
                isPut = true;
            }

            if (curObj && !isPut) //cat이 spawn 따라다님
                curObj.transform.position = new Vector2(transform.position.x, transform.position.y - 1f);
            
            if (transform.position.x < -2.3)
            { //spawn움직임
                direction = Dvalue;
                curObj.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (transform.position.x > 2.3)
            {
                direction = Dvalue * -1;
                curObj.GetComponent<SpriteRenderer>().flipX = false;
            }
            if (!isPut)
            {
                transform.Translate(Vector2.right * direction * Time.deltaTime);
            }
            if (preObj && topObj.transform.position.y <= preObj.transform.position.y) //전것보다 밑에쌓으면 게임오버
            {
                if (!isGameover)
                    Gameover();
            }

            if (isCameraMove) //카메라 위로 움직임
            {
                preObjPos = Camera.main.WorldToViewportPoint(preObj.transform.position);
                Camera.main.transform.Translate(Vector2.up * 1.0f * Time.deltaTime);
            }

            if(isGameover && Camera.main.transform.position.y >0)
                Camera.main.transform.Translate(Vector2.up * -1.0f * Time.deltaTime);

        }
    }
}
