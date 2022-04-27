using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BamsongiGenerator : MonoBehaviour
{
    public GameObject bamsongi_prefab;
    public Vector3 wind;
    public int score = 0;
    public int bam_count = 0;
    public GameObject pre_bam;
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        wind = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(-1.0f, 4.0f), 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && bam_count < 5)
        {
            bam_count++;
            Destroy(pre_bam);

            GameObject bamsongi = Instantiate(bamsongi_prefab) as GameObject;
            Ray screen_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 shooting_ray = screen_ray.direction;
            bamsongi.GetComponent<BamsongiCtrl>().Shoot(shooting_ray * 1000);
            if (bam_count >= 5)
            {
                text.SetActive(true);
            }
            
            pre_bam = bamsongi;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            score = 0;
            bam_count = 0;
            text.SetActive(false);
            Destroy(pre_bam);
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(81, Screen.height - 20, 128, 32), wind.ToString());
        GUI.Label(new Rect(81, Screen.height - 40, 128, 32), bam_count +" : "+score.ToString());
    }
}
