using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText; //Text用変数
    public int score = 0; //スコア計算用変数

    void Start()
    {
        score = 0;
        SetScore();   //初期スコアを代入して表示
    }

    void SetScore()
    {
        scoreText.text = string.Format("Score:{0} Yen", score) ;
    }

    public void HitHalo( string tag ){
        string yourTag = tag;

        if (yourTag == "Halo")
        {
            score += 50;
            // ここで音をならす
        }
        else if (yourTag == "HaloG")
        {
            score += 500;
            // ここで音をならす
        }
        else if (yourTag == "")
        {
            score += 100;
            // ここで音をならす
        }

        SetScore();
        // 上のができたらここはけす
        gameObject.GetComponent<AudioSource>().Play();

    }
}