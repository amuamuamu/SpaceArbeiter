using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    // プレイヤー１か２か
    public enum Kind
    {
        P1,
        P2
    }


    [SerializeField]
    Kind kind_;


    public Kind MyKind { get { return kind_; } }


    [SerializeField]
    GameObject cameraObject;
    public GameObject timmer;


    //Rigidbodyを変数に入れる
    Rigidbody rb;


    //移動スピード
    private float speed = 20f;
    //ジャンプ力
    private float thrust = 500;
    //Animatorを入れる変数
    Animator animator;
    //プレイヤーの位置を入れる
    Vector3 playerPos;
    //地面に接触しているか否か
    bool ground = true;
    bool isJump = false;
    private object 上に移動;

    void Start()
    {
        //Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
        //プレイヤーのAnimatorにアクセスする
        animator = GetComponent<Animator>();
        //プレイヤーの現在より少し前の位置を保存
        playerPos = transform.position;


        ground = true;
    }


    void Update()
    {

        // これはOK
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 1000);
        //}

        // これもオッケー
        //if ( Input.GetButtonDown("Jump") ){
        //    gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 1000);
        //}






        if (timmer.GetComponent<TimerController>().isWaitingReady ||
            timmer.GetComponent<TimerController>().isTimeOver)
        {
            return;
        }


        float h = 0, v = 0;
        isJump = false;

        switch (kind_)
        {
            case Kind.P1:
                //A・Dキー、←→キーで横移動
                h = Input.GetAxisRaw("Horizontal");
                //W・Sキー、↑↓キーで前後移動
                v = Input.GetAxisRaw("Vertical");
                //スペースキーやゲームパッドの3ボタンでジャンプ
                isJump = Input.GetButtonDown("Jump");
                break;


            case Kind.P2:

                //A・Dキー、←→キーで横移動
                h = Input.GetAxisRaw("Horizontal2");
                //W・Sキー、↑↓キーで前後移動
                v = Input.GetAxisRaw("Vertical2");
                //スペースキーやゲームパッドの3ボタンでジャンプ
                isJump = Input.GetButtonDown("Jump2");

                break;
        }


        float x = h * Time.deltaTime * speed;
        float z = v * Time.deltaTime * speed;
        //現在の位置＋入力した数値の場所に移動する


        Vector3 moveDir =
            cameraObject.transform.right * x +
            cameraObject.transform.forward * z;

        rb.MovePosition(transform.position + moveDir);


        // 動いていたらtrue
        bool isMoving = moveDir.magnitude > 0.01f;


        //移動距離が少しでもあった場合に方向転換
        if (isMoving)
        {
            //directionのX軸とZ軸の方向を向かせる
            //    transform.rotation = Quaternion.LookRotation(new Vector3
            //                        (direction.x, 0, direction.z));
            var rotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(rotation, transform.rotation, 0.5f);


            //プレイヤーの位置を更新する
            playerPos = transform.position;
        }


        //地面に接触していると作動する
        if (ground)
        {
            //if ( Input.GetButtonDown("Jump") ){
            //    gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * thrust);
            //    animator.SetBool("Jumping", true);
            //    ground = false;
            //}

            if (isJump)
            {
                //thrustの分だけ上方に力がかかる
                rb.AddForce(transform.up * thrust);


                //ジャンプのアニメーションをオンにする
                animator.SetBool("Jumping", true);
                ground = false;
            }
        }


        //走るアニメーションを再生
        animator.SetBool("Running", isMoving);
        //ジャンプのアニメーションをオフにする
        animator.SetBool("Jumping", !ground);


        //プレイヤーの位置を更新する
        playerPos = transform.position;
    }


    //Planに触れている間作動
    void OnCollisionStay(Collision col)
    {
        //　地上物なら
        if (col.gameObject.tag == "Ground")
        {
            ground = true;
        }
    }


    //Planから離れると作動
    void OnCollisionExit(Collision col)
    {
        //　地上物なら
        if (col.gameObject.tag == "Ground")
        {
            ground = false;
        }
    }


}
