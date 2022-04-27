using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision_scr : MonoBehaviour
{
    public float power = 500.0f;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Cube")
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 1) * power);
    }
}
