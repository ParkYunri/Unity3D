using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control : MonoBehaviour
{
    public static float ACCELERATION = 10.0f;
    public static float SPEED_MIN = 5.0f;
    public static float SPEED_MAX = 9.0f;
    public static float JUMP_HEIGHT_MAX = 3.0f;
    public static float JUMP_POWER_REDUCE = 0.5f;
    public static float FAIL_LIMIT = -5.0f;
    public float game_timer = 0.0f;

    public enum STEP
    {
        NONE = -1,
        RUN = 0,
        JUMP,
        MISS,
        NUM,
    };

    public STEP step = STEP.NONE;
    public STEP next_step = STEP.NONE;

    public float step_timer = 0.0f;
    public bool is_landed = false;
    public bool is_collided = false;
    public bool is_key_released = false;    //점프키가 손에서 떼어졌는지

    // Start is called before the first frame update
    void Start()
    {
        next_step = STEP.RUN;   //시작하자마자 달리게(무조건)
    }

    private void CheckLanded()
    {
        is_landed = false;  //아직 착지x

        do
        {
            Vector3 current_position = transform.position;
            Vector3 down_position = current_position + Vector3.down * 1.0f;

            RaycastHit hit;
            if (!Physics.Linecast(current_position, down_position, out hit))
                break;

            if (step == STEP.JUMP)
            {
                if (step_timer < Time.deltaTime * 3.0f)//점프한지 경과한 시간
                    break;
            }
            is_landed = true;
        } while (false);
    }

    // Update is called once per frame
    void Update()
    {
        game_timer += Time.deltaTime;
        //Debug.Log(game_timer);
        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        CheckLanded();

        switch (step)
        {
            case STEP.RUN:
            case STEP.JUMP:
                if (transform.position.y < FAIL_LIMIT)
                    next_step = STEP.MISS;
                break;
        }

        step_timer += Time.deltaTime;

        if(next_step == STEP.NONE)
        {
            switch (step)
            {
                case STEP.RUN:
                    if (!is_landed) { }
                    else if (Input.GetMouseButtonDown(0))
                        next_step = STEP.JUMP;
                    break;
                case STEP.JUMP:
                    if (is_landed)
                        next_step = STEP.RUN;
                    break;
            }
        }

        while(next_step != STEP.NONE)//RUN 또는 jump 상태일 경우
        {
            step = next_step;
            next_step = STEP.NONE;

            switch (step)
            {
                case STEP.JUMP:
                    velocity.y = Mathf.Sqrt(2.0f * 9.8f * JUMP_HEIGHT_MAX);
                    is_key_released = false;
                    break;
            }
            step_timer = 0.0f;
        }

        switch (step)
        {
            case STEP.RUN:              
                velocity.x += player_control.ACCELERATION * Time.deltaTime;

                if (game_timer <= 15)
                    player_control.SPEED_MAX = 5.0f;
                else if (game_timer <= 30)
                    player_control.SPEED_MAX = 6.0f;
                else if (game_timer <= 45)
                    player_control.SPEED_MAX = 7.0f;
                else if (game_timer <= 60)
                    player_control.SPEED_MAX = 8.0f;
                else if (game_timer <= 75)
                    player_control.SPEED_MAX = 9.0f;
                else if (game_timer <= 90)
                    player_control.SPEED_MAX = 5.0f;

                if (Mathf.Abs(velocity.x) > player_control.SPEED_MAX)
                    velocity.x = player_control.SPEED_MAX;
                break;

            case STEP.JUMP:
                do
                {
                    if (!Input.GetMouseButtonUp(0))
                        break;
                    if (is_key_released)//mousebutton 눌렀는가 안눌렀는가
                        break;
                    if (velocity.y <= 0.0f) //하강하고 있을 때
                        break;

                    velocity.y *= JUMP_POWER_REDUCE;
                    is_key_released = true;     //감속 여부 기록
                } while (false);
                break;

            case STEP.MISS: //구멍에 빠지면
                velocity.x -= player_control.ACCELERATION * Time.deltaTime;
                if(velocity.x < 0.0f)
                {
                    velocity.x = 0.0f;
                    Application.Quit();     //게임종료
                }
                break;
        }
        GetComponent<Rigidbody>().velocity = velocity;
    }
}
