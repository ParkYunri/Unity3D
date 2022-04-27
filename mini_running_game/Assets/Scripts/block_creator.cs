using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block_creator : MonoBehaviour
{
    public GameObject[] block_prefabs;
    private int block_count = 0;

    public void CreateBlock(Vector3 block_position)
    {
        int next_block_type = block_count % block_prefabs.Length;   //0일땐 0번, 1일땐 1번 생성

        GameObject game_object = GameObject.Instantiate(block_prefabs[next_block_type]) as GameObject;
        game_object.transform.position = block_position;
        block_count++;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
