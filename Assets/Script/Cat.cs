using UnityEngine;
using System.Collections;


public class Cat : MonoBehaviour {

    private bool isOn = false;
   
    private float halfSize;
    private float sub;

    public int score;
    public Spawn spawn;
    
	// Use this for initialization
	void Start () {
        spawn = GameObject.Find("spawn").GetComponent<Spawn>();
        GetComponent<Rigidbody2D>().isKinematic = true;
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.name == "grass" && !spawn.isGameover)
        {
            spawn.Gameover();
        }
        if (!isOn)
        {
            if (coll.gameObject.tag == "cat")
            {
                GetComponent<Rigidbody2D>().isKinematic = true;
                sub = Mathf.Abs(coll.gameObject.transform.position.x - transform.position.x) ; //x축 차이 계산
                halfSize = coll.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2 - 0.4f;

                Debug.Log("sub:"+sub + " " + "halfsize:"+halfSize);
                if (sub > halfSize && !spawn.isGameover) //x축 차이가 halfsize보다 크면 게임오버
                {
                    GetComponent<Rigidbody2D>().isKinematic = false;
                    if (coll.gameObject.transform.position.x > transform.position.x)
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.right * -3, ForceMode2D.Impulse);
                    }
                    else
                        GetComponent<Rigidbody2D>().AddForce(Vector2.right * 3, ForceMode2D.Impulse);
                    spawn.Gameover();
                }
                else
                {
                    score -= (int)(sub * 300);
                    spawn.collCheck(this.gameObject, transform.position.x, score);
                }
            }
            isOn = true;
        }
    }
}
