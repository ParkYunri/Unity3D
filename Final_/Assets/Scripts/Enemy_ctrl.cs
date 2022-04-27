using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_ctrl : MonoBehaviour
{
    public float speed = 2.0f;
    public float dis = 5.0f;
    int a = 1;
    void Update()
    {
        if (transform.localPosition.x < -dis)
        {
            a = -1;
        }
        else if (transform.localPosition.x > dis)
        {
            a = 1;
        }

        transform.Translate(Vector3.left * speed * Time.deltaTime * a);
    }
}
