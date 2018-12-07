using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public GameObject[] BGM;
    public GameObject[] FreeLookCamera; //プレイヤーの通常のカメラ
    public GameObject[] FixedCamera; //勝利演出のカメラ
    public GameObject[] UI;
    public GameObject[] Players;
    public GameObject[] WinImages;

    public Text timerText;
    public bool isWaitingReady = true;
    public bool isTimeOver = false;
    public float readyTime = 1.0f;
   

    public float totalTime;
    int seconds;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("waitForReady");
        StartCoroutine("waitForResult");
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaitingReady)
        {
            return;
        }

        if (totalTime < 0.0f)
            return;

        totalTime -= Time.deltaTime;

        //  タイムアウト！
        if (totalTime <= 0.0f)
        {
            BGM[0].SetActive(false);
            //  効果音
            BGM[1].SetActive(true);

            // FINISHの文字を出す
            UI[3].SetActive(true);




            //このタイミングで効果音分とめたい

            //カメラ切り替え
            FreeLookCamera[0].SetActive(false);
            FreeLookCamera[1].SetActive(false);
            FixedCamera[0].SetActive(true);
            FixedCamera[1].SetActive(true);

            //playerのアニメーション切り替え　できたら
           

            isTimeOver = true;


            // FINISHの文字を消す
            UI[3].SetActive(false);
            // リザルト表示を出す
            UI[3].SetActive(true);

            if ( Players[0].GetComponent<Score>().score > Players[1].GetComponent<Score>().score){
                WinImages[0].SetActive(true);
            }else if(Players[0].GetComponent<Score>().score < Players[1].GetComponent<Score>().score)
            {
                WinImages[1].SetActive(true);

            }
            else if(Players[0].GetComponent<Score>().score == Players[1].GetComponent<Score>().score){
                WinImages[2].SetActive(true);
            }
            


        }

        seconds = (int)Mathf.Ceil(totalTime);
        timerText.text = string.Format("Time:{0}", seconds);
    }

    private IEnumerator waitForReady(){

        yield return new WaitForSeconds(readyTime * 2f);
        // Readyの文字を消す
        UI[0].SetActive(false);
        // Goのもじを出す
        UI[1].SetActive(true);
              
        yield return new WaitForSeconds(readyTime * 1f);
        // Goのもじも消す
        UI[1].SetActive(false);

        isWaitingReady = false;
    }

   



    }