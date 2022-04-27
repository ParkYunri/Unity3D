using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCtrl : MonoBehaviour
{
    public GameObject player;

    Vector3 startPos;
    Quaternion startRotate;

    bool isStarted = false;
    static bool isEnd = false;

    static int stageLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        startPos = GameObject.FindGameObjectWithTag("Start").transform.position;
        startRotate = GameObject.FindGameObjectWithTag("Start").transform.rotation;

        if (stageLevel > 0)
            StartGame();
    }

    void Awake()
    {
        Time.timeScale = 0.0f;
    }

    void Turn()
    {
        startRotate = Quaternion.LookRotation(startPos);
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();

        if (stageLevel < 10)
            GUILayout.Label("Stage " + (stageLevel + 1));
        else
            GUILayout.Label("Stage End");

        GUILayout.Space(5);
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        if (!isStarted && stageLevel == 0) {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            GUILayout.Label("Press the START Button!");

            if (GUILayout.Button("START!!"))
            {
                isStarted = true;
                StartGame();
            }

            GUILayout.Space(100);
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        else if (isEnd && stageLevel == 10)
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            GUILayout.Label("Clear All Stage! Thank you :)");

            if (GUILayout.Button("RESTART??"))
            {
                isEnd = false;
                stageLevel = 0;
                Score_result.score = 0;
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            }

            GUILayout.Space(100);
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }

    void StartGame()
    {
        Time.timeScale = 1.0f;
        startPos = new Vector3(startPos.x, startPos.y + 2.0f, startPos.z);

        Instantiate(player, startPos, Quaternion.Euler(0.0f, 0.0f, 0.0f));
    }

    public static void EndGame()
    {
        Time.timeScale = 0.0f;
        stageLevel++;

        if (stageLevel == 10)
            isEnd = true;
        else
            SceneManager.LoadScene(stageLevel, LoadSceneMode.Single);
    }

    public static void RestartStage()
    {
        Time.timeScale = 0.0f;
        SceneManager.LoadScene(stageLevel, LoadSceneMode.Single);
    }
}
