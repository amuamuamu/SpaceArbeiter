using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSmooth = 10f;

    private Vector3 targetPosition;

    public float changeTargetSqrDistance = 1f;

    private void Start()
    {
        targetPosition = GetRandomPositionOnLevel();
    }

    private void Update()
    {
        // 目標地点との距離が小さければ、次のランダムな目標地点を設定する
        float sqrDistanceToTarget = Vector3.SqrMagnitude(transform.position - targetPosition);
        if (sqrDistanceToTarget < changeTargetSqrDistance)
        {
            targetPosition = GetRandomPositionOnLevel();
        }

        // 目標地点の方向を向く
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmooth);

        // 前方に進む
         transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // gameObject.GetComponent<Rigidbody>().velocity = Vector3.forward * speed;
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * speed);
    }

    public Vector3 GetRandomPositionOnLevel()
    {
        float levelSize = 100f;
        return new Vector3(Random.Range(-levelSize, levelSize), 0, Random.Range(-levelSize, levelSize));
    }
}