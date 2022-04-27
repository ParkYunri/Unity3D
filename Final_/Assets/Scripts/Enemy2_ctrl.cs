using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_ctrl : MonoBehaviour
{
    public float speed = 2.0f;
    public float dis = 5.0f;
    int a = 1;
    void Update()
    {
        if (transform.localPosition.z < -dis)
        {
            a = -1;
        }
        else if (transform.localPosition.z > dis)
        {
            a = 1;
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime * a);
    }
}
