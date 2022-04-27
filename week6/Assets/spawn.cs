using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject pf_wall;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            float interval = Random.Range(1.0f, 2.0f);
            float randomY = Random.Range(-3.0f, 4.0f);
            transform.position = new Vector3(0.0f, randomY, 0.0f);

            Instantiate(pf_wall, transform.position, transform.rotation);
            yield return new WaitForSeconds(interval);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
