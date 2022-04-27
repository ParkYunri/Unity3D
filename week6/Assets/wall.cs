using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Random.Range(-6.0f, -4.0f);

        transform.Translate(speed * Time.deltaTime, 0, 0);
    }
}
