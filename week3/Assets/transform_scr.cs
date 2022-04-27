using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transform_scr : MonoBehaviour
{ 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            this.transform.Translate(Vector3.forward * 3.0f * Time.deltaTime);
        if (Input.GetKey(KeyCode.DownArrow))
            this.transform.Translate(Vector3.back * 3.0f * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftArrow))
            this.transform.Translate(Vector3.left * 3.0f * Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow))
            this.transform.Translate(Vector3.right * 3.0f * Time.deltaTime);
    }
   
    
}
