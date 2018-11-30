
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public GameObject cameraObject;
    //Rigidbodyを変数に入れる
    Rigidbody rb;
    //移動スピード
    public float speed = 3f;
    //ジャンプ力
    public float thrust = 200;
    //Animatorを入れる変数
    private Animator animator;
    //プレイヤーの位置を入れる
    Vector3 playerPos;
    //地面に接触しているか否か
    bool ground;
    void Start()
    {


        //Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
        //プレイヤーのAnimatorにアクセスする
        animator = GetComponent<Animator>();
        //プレイヤーの現在より少し前の位置を保存
        playerPos = transform.position;
    }
    void Update()
    {
        //地面に接触していると作動する
        if (ground)
        {
            //A・Dキー、←→キーで横移動
            float h = Input.GetAxisRaw("Horizontal");
            float x = h * Time.deltaTime * speed;

            //W・Sキー、↑↓キーで前後移動
            float v = Input.GetAxisRaw("Vertical");
            float z = v * Time.deltaTime * speed;
            //現在の位置＋入力した数値の場所に移動する

            Vector3 camDir = cameraObject.transform.right * x + cameraObject.transform.forward * z;

            // rb.MovePosition(transform.position + new Vector3(x, 0, z));
            rb.MovePosition(transform.position + camDir);
            //プレイヤーの最新の位置から少し前の位置を引いて方向を割り出す
            Vector3 direction = transform.position - playerPos;
            //移動距離が少しでもあった場合に方向転換
            if ( !(( -0.001f < h && h < 0.001f )  && (-0.001f < v && v < 0.001f )))
            // if (direction.magnitude > 0.01f)
            {
                //directionのX軸とZ軸の方向を向かせる
                transform.rotation = Quaternion.LookRotation(new Vector3
                                    (direction.x, 0, direction.z));
                //走るアニメーションを再生
                animator.SetBool("Running", true);
            }
            else
            {
                //ベクトルの長さがない＝移動していない時は走るアニメーションはオフ
                animator.SetBool("Running", false);
            }
            //プレイヤーの位置を更新する
            playerPos = transform.position;
            //スペースキーやゲームパッドの3ボタンでジャンプ
            if (Input.GetButton("Jump"))
            {
                //thrustの分だけ上方に力がかかる
                rb.AddForce(transform.up * thrust);
                //速度が出ていたら前方と上方に力がかかる
                if (rb.velocity.magnitude > 0)
                    rb.AddForce(transform.forward * thrust + transform.up * thrust);
            }
        }
    }
    //Planに触れている間作動
    void OnCollisionStay(Collision col)
    {
        ground = true;
        //ジャンプのアニメーションをオフにする
        animator.SetBool("Jumping", false);
    }
    //Planから離れると作動
    void OnCollisionExit(Collision col)
    {
        ground = false;
        //ジャンプのアニメーションをオンにする
        animator.SetBool("Jumping", true);
    }
}