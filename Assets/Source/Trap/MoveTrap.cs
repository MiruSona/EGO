using UnityEngine;
using System.Collections;

public class MoveTrap : MonoBehaviour {

    //참조
    private GameDAO.PlayerData player_data;
    private Vector3 scale;
    private AudioSource audio_source;
    private AudioClip audio_clip;

    //공격력
    public float atk = 1.0f;

    //이동관련
    //방향
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
    public Direction direction = Direction.Right;
    //이동 정도
    public float move_amount = 15f;
    public float move_time = 1.5f;

    //초기화
    void Start()
    {
        player_data = GameDAO.instance.player_data;
        scale = transform.localScale;
        audio_source = GetComponent<AudioSource>();
        audio_clip = Resources.Load<AudioClip>("Sound/Effect/Eagle");
    }

    public void Move()
    {
        audio_source.PlayOneShot(audio_clip);
        Vector3 amount = Vector3.zero;
        switch (direction)
        {
            case Direction.Right:
                amount.x = move_amount;
                iTween.MoveAdd(gameObject, amount, move_time);
                break;
            case Direction.Left:
                scale.x *= -1f;
                transform.localScale = scale;
                amount.x = -move_amount;
                iTween.MoveAdd(gameObject, amount, move_time);
                break;
            case Direction.Up:
                amount.y = move_amount;
                iTween.MoveAdd(gameObject, amount, move_time);
                break;
            case Direction.Down:
                amount.y = -move_amount;
                iTween.MoveAdd(gameObject, amount, move_time);
                break;
        }
    }

    //충돌 판정
    void OnTriggerEnter2D(Collider2D col)
    {
        //플레이어에게 닿으면 공격
        if (col.CompareTag("Player"))
        {
            Attack();
        }
    }

    // 데미지 주는 함수
    private void Attack()
    {
        if (player_data.state == GameDAO.PlayerData.State.isAlive)
            player_data.SubHP(atk);
    }
}
