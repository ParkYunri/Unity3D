using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map_creator : MonoBehaviour
{
    public static float BLOCK_WIDTH = 1.0f;
    public static float BLOCK_HEIGHT = 0.2f;
    public static int BLOCK_NUM_IN_SCREEN = 24;     //화면에 블럭 총 24개

    private level_control lev_ctrl = null;

    private struct FloorBlock       //특정 위치를 지정->그 위치에 블럭 생성 유무 확인
    {
        public bool is_created;
        public Vector3 position;
    }

    private FloorBlock last_block;      //가장 마지막에 생성된 블록의 정보를 기록할 변수
    private player_control player = null;       //player_control 스크립트에 연결
    private block_creator block;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player_control>();
        last_block.is_created = false;  //생성된 블록이 없으니까
        block = gameObject.GetComponent<block_creator>();   

        lev_ctrl = new level_control();
        lev_ctrl.Initialize();
    }

    private void CreateFloorBlock()
    {
        Vector3 block_position;
        if (!last_block.is_created)     //마지막에 블록이 생성된게 없다면(맨 처음에만 false)
        {
            block_position = player.transform.position;
            block_position.x -= BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);  //플레이어의 왼쪽에 블록 생성
            block_position.y = 0.0f;
        }
        else
        {
            block_position = last_block.position;
        }

        block_position.x += BLOCK_WIDTH;
        //block.CreateBlock(block_position);
        lev_ctrl.UpdateStatus();

        float X = 0.0f;
        if (player.game_timer <= 15)
        {
            X = 0.0f;
        }
        else if(player.game_timer <= 30)
        {
            X = Random.Range(-1.0f, 2.0f);
        }
        else if(player.game_timer <= 60)
        {
            X = Random.Range(-2.0f, 3.0f);
        }
        else if(player.game_timer <= 75)
        {
            X = Random.Range(-1.0f, 2.0f);
        }
        else if(player.game_timer <= 90)
        {
            X = 0.0f;
        }

        block_position.y = lev_ctrl.current_block.height * BLOCK_HEIGHT * X;
        level_control.CreationInfo current = lev_ctrl.current_block;

        if (current.block_type == level_control.Block.TYPE.FLOOR)
            block.CreateBlock(block_position);

        last_block.position = block_position;
        last_block.is_created = true;
    }

    // Update is called once per frame
    void Update()
    {
        float block_generate_x = player.transform.position.x;   //플레이어의 위치
        block_generate_x += BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN + 1) / 2.0f;
        while (last_block.position.x < block_generate_x)        //마지막에 생성된 블록의 위치가 block_generate_x 위치보다 작을 경우
            CreateFloorBlock();         //새로운 블록 만듬
    }

    public bool IsGone(GameObject block_object)
    {
        bool result = false;
        float left_limit = player.transform.position.x - BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
        if (block_object.transform.position.x < left_limit)
            result = true;
        return result;
    }
}
