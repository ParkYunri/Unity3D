using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public int jump_power;
    private float gameTime;

    // Start is called before the first frame update
    void Start()
    {
        jump_power = Random.Range(5, 8);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, jump_power, 0);
        }
        gameTime += Time.deltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        SceneManager.LoadScene("main_scene");
    }

    void OnGUI()
    {
        GUI.Label(new Rect(81, Screen.height - 40, 128, 32), jump_power.ToString());
        GUI.Label(new Rect(Screen.width / 2 - 30, 80, 100, 20), gameTime.ToString());
    }
}
