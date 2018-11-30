using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public GameObject[] BGM;
    public Text timerText;

    public float totalTime;
    int seconds;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (totalTime < 0.0f)
            return;

        totalTime -= Time.deltaTime;

        //  タイムアウト！ 引き分け
        if (totalTime <= 0.0f)
        {
            BGM[0].SetActive(false);
            BGM[1].SetActive(true);
        }

        seconds = (int)Mathf.Ceil(totalTime);
        timerText.text = string.Format("Time:{0}", seconds);
    }
}