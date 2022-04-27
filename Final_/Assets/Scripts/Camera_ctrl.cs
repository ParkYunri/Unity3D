using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_ctrl : MonoBehaviour
{
    public Vector3 offset;
    public float followSpeed = 0.15f;

    private GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        Vector3 camera_pos = player.transform.position + offset;
        Vector3 lerp_pos = Vector3.Lerp(transform.position, camera_pos, followSpeed);
        transform.position = lerp_pos;
        transform.LookAt(player.transform);
    }
}
