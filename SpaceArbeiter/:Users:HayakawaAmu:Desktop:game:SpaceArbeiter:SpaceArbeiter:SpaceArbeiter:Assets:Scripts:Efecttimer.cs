using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTimer : MonoBehaviour
{
    public float lifeTime;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("WaitForDestoy");

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator WaitForDestoy()
    {
        // 1秒待つ  
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}