using UnityEngine;
using System.Collections.Generic;


public class GameDAO : SingleTon<GameDAO> {
    
    #region 플레이어

    //플레이어 데이터
    public class PlayerData
    {
        #region 변수

        //스탯
        public float hp = 10.0f;        //체력
        public float hp_max = 10.0f;    //최대 체력
        public float speed = 0.1f;       //속도
        public int facing = 1;          //방향(1, -1)
        public bool slow = false;

        //타이머
        public Timer damaged_timer = new Timer(0f, 1.0f);
        public Timer slow_timer = new Timer(0f, 0.5f);

        //움직임
        public enum Movement
        {
            isReady,            //대기
            isMove,             //움직임 초기화
            isMoveSchedule,     //움직임
        }
        public Movement movement = Movement.isReady;

        //움직이는 방향
        public enum MoveDirection
        {
            Up,
            Down,
            Right,
            Left
        }
        public MoveDirection move_direction = MoveDirection.Right;

        //상태
        public enum State
        {
            isAlive,
            isDamaged,
            isDead
        }
        public State state = State.isAlive;
        #endregion

        #region 함수

        //체력 처리 함수
        public void SubHP(float _amount)
        {
            //살아있는 상태가 아니면 실행 X
            if (state != State.isAlive)
                return;

            //HP가 0보다 작거나 같으면
            if (hp - _amount <= 0)
            {
                //HP를 0으로 만들고 죽음상태 표시
                hp = 0;
                state = State.isDead;
            }
            else //HP가 0보다 많으면
            {
                //값만큼 빼준다
                hp -= _amount;
                state = State.isDamaged;
            }
        }
        #endregion
    }
    public PlayerData player_data = new PlayerData();
    
    #endregion
    
    //----------------------- 공용 -----------------------
    //시간 관리 구조체
    public struct Timer
    {
        public float term;      //간격
        public float timer;     //타이머

        public Timer(float _timer, float _term)
        {
            term = _term;
            timer = _timer;
        }
    }

    //초기화
    void Awake()
    {

    }
}
