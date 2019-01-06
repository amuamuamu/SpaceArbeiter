using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemy : MonoBehaviour
{
    //生まれてくる敵プレハブ
    public GameObject[] enemyPrefabs;
    //敵を格納
    GameObject[] existEnemys;
    //アクティブ最大数
    public int maxEnemy = 30;

    // Use this for initialization
    void Start()
    {
        //配列確保
        existEnemys = new GameObject[maxEnemy];
        //周期的に実行したい場合はコルーチン
        StartCoroutine(Exec());

    }

    //敵を作成する
    IEnumerator Exec()
    {
        while (true)
        {
            Generate();
            yield return new WaitForSeconds(1.0f);
        }
    }

    void Generate()
    {
        for (int enemyCount = 0; enemyCount < existEnemys.Length; ++enemyCount)
        {
            if (existEnemys[enemyCount] == null)
            {
                //敵を作成する
                existEnemys[enemyCount] = Instantiate(enemyPrefabs[Random.Range(0,enemyPrefabs.Length)], transform.position, transform.rotation) as GameObject;
                return;
            }
        }
    }
}