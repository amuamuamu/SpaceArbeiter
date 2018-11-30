using UnityEngine;
using System.Collections;

public class Halohalo : MonoBehaviour
{
    public GameObject effect;
    public float effectOffsetY = 0.1f ;




    // トリガーとの接触時に呼ばれるコールバック
    void OnTriggerEnter(Collider hit)
    {
        // 接触対象はPlayerタグですか？
        if (hit.CompareTag("Player"))
        {
            hit.gameObject.GetComponent<Score>().HitHalo(gameObject.tag);

            Vector3 effectPos = gameObject.transform.position;
            effectPos.y += effectOffsetY;

            Instantiate(effect, effectPos, Quaternion.identity);

            // このコンポーネントを持つGameObjectを破棄する
            Destroy(gameObject);
        }
    }
}


