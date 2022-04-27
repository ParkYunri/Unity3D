using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_ctrl : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    Rigidbody rigid;
    float hMove;
    float vMove;
    Vector3 movement;
   
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        hMove = Input.GetAxisRaw("Horizontal");
        vMove = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Run(hMove, vMove);
    }

    void Run(float h, float v)
    {
        movement.Set(h, 0, v);
        movement = movement.normalized * moveSpeed * Time.deltaTime;

        rigid.MovePosition(transform.position + movement);
    }

    void Turn()
    {
        Quaternion pRotate = Quaternion.LookRotation(movement);

        rigid.rotation = Quaternion.Slerp(rigid.rotation, pRotate, moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Coin")
        {
            coinValue coin = col.gameObject.GetComponent<coinValue>();
            Score_result.setScore((int)coin.value);

            Destroy(col.gameObject, 0.0f);

            audioSource.Play();
        }

        if (col.gameObject.tag == "End")
        {          
            col.enabled = false;
            GameCtrl.EndGame();
        }

        if(col.gameObject.tag == "Enemy")
        {
            RestartStage();
        }
    }

    void RestartStage()
    {
        GameCtrl.RestartStage();
    }
}
