using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_control : MonoBehaviour
{
    public class Block
    {
        public enum TYPE
        {
            NONE = -1,
            FLOOR = 0,
            HOLE,
            NUM,
        };
    };

    public struct CreationInfo
    {
        public Block.TYPE block_type;
        public int max_count;
        public int height;
        public int current_count;
    };

    public CreationInfo previous_block;
    public CreationInfo current_block;
    public CreationInfo next_block;

    public int level = 0;

    private void ClearNextBlock(ref CreationInfo block)     //다음 블록에 구멍을 뚫을 것
    {
        block.block_type = Block.TYPE.FLOOR;
        block.max_count = 15;
        block.height = 0;
        block.current_count = 0;
    }

    public void Initialize()
    {
        ClearNextBlock(ref previous_block);
        ClearNextBlock(ref current_block);
        ClearNextBlock(ref next_block);
    }

    private void UpdateLevel(ref CreationInfo current, CreationInfo previous)
    {
        player_control player = GameObject.FindGameObjectWithTag("Player").GetComponent<player_control>();
        Debug.Log(player.game_timer);

        switch (previous.block_type)
        {
            case Block.TYPE.FLOOR:
                current.block_type = Block.TYPE.HOLE;

                if (player.game_timer <= 30)
                    current.max_count = Random.Range(1, 3);
                else if (player.game_timer <= 60)
                    current.max_count = Random.Range(2, 4);
                else if (player.game_timer <= 90)
                    current.max_count = Random.Range(1, 3);
                
                current.height = previous.height;
                break;

            case Block.TYPE.HOLE:
                current.block_type = Block.TYPE.FLOOR;
                if (player.game_timer <= 15)
                    current.max_count = Random.Range(9, 11);
                else if (player.game_timer <= 30)
                    current.max_count = Random.Range(8, 10);
                else if (player.game_timer <= 45)
                    current.max_count = Random.Range(7, 9);
                else if (player.game_timer <= 60)
                    current.max_count = Random.Range(5, 7);
                else if (player.game_timer <= 75)
                    current.max_count = Random.Range(6, 8);
                else if (player.game_timer <= 90)
                    current.max_count = Random.Range(9, 11);
                
                break;
        }
    }

    public void UpdateStatus()      //현재 블록 위치에 만든 것 처럼(속임수)
    {
        current_block.current_count++; //블럭에 빈 공간 만들고 마치 블록을 만든 것 처럼 해놓음
        if(current_block.current_count >= current_block.max_count)
        {
            previous_block = current_block;
            current_block = next_block;

            ClearNextBlock(ref next_block);
            UpdateLevel(ref next_block, current_block);
        }
    }
}
