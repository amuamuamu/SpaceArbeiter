using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoBacktoStartScene : MonoBehaviour {
    public float Timetoback = 30.0f;
    public string StartSceneName;

	// Use this for initialization
	void Start () {
        StartCoroutine("WaitForFinish");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private IEnumerator WaitForFinish()
    {
        // 1秒待つ  
        yield return new WaitForSeconds(Timetoback);

       
        SceneManager.LoadScene(StartSceneName);
    }
}
