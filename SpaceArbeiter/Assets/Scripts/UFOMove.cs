using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOMove : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSmooth = 10f;
    float startY;

    private Vector3 targetPosition;

    public float changeTargetSqrDistance = 1f;

    private void Start()
    {

        startY = gameObject.transform.position.y;

        targetPosition = GetRandomPositionOnLevel();
    }

    private void Update()
    {
        // 目標地点との距離が小さければ、次のランダムな目標地点を設定する
        //float sqrDistanceToTarget = Vector3.SqrMagnitude(transform.position - targetPosition);
        //if (sqrDistanceToTarget < changeTargetSqrDistance)
        //{
            //targetPosition = GetRandomPositionOnLevel();
        //}

        // 目標地点の方向を向く
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmooth);

        // 前方に進む
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public Vector3 GetRandomPositionOnLevel()
    {
        float levelSize = 10f;
        return new Vector3(Random.Range(-levelSize, levelSize), startY, Random.Range(-levelSize, levelSize));
    }
}