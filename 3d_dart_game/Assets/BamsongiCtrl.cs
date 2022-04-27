using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BamsongiCtrl : MonoBehaviour
{
    float timer = 0.0f;
    int score = 0;
    bool is_changed = false;
    private BamsongiGenerator bamsongi;

    // Start is called before the first frame update
    void Start()
    {
        bamsongi = GameObject.Find("bamsongi_spawn").GetComponent<BamsongiGenerator>();
        Debug.Log(bamsongi.wind);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        GetComponent<Rigidbody>().AddForce(bamsongi.wind);
        if (timer >= 2 && !is_changed)
        {
            bamsongi.wind = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(-1.0f, 4.0f), 0.0f);
            is_changed = true;
            
            //Debug.Log(bamsongi.wind);
            //Debug.Log(is_changed);
        }
        //Debug.Log(wind);
    }

    public void Shoot(Vector3 dir)
    {
        GetComponent<Rigidbody>().AddForce(dir);
    }

    void OnCollisionEnter(Collision other)
    {
        bamsongi.wind = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(-1.0f, 4.0f), 0.0f);
        is_changed = true;
        Debug.Log(bamsongi.wind);
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<ParticleSystem>().Play();
        Vector3 collided_position = transform.position;
        float distance = collided_position.x * collided_position.x +
            (collided_position.y - 6.5f) * (collided_position.y - 6.5f);
        distance = Mathf.Sqrt(distance);


        if (distance <= 0.4f)
            score = 100;
        else if (distance <= 0.8f)
            score = 90;
        else if (distance <= 1.2f)
            score = 70;
        else if (distance <= 1.6f)
            score = 50;
        else if (distance <= 2.0f)
            score = 30;
        else
            score = 0;

        bamsongi.score += score;
    }
}
