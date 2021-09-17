using UnityEngine;
using System.Collections;

public class SlowTrap : MonoBehaviour {

    //참조
    private GameDAO.PlayerData player_data;
    private AudioSource audio_source;

    //속도감소
    public float slow_speed = 0.05f;

    //초기화
    void Start()
    {
        player_data = GameDAO.instance.player_data;
        audio_source = GetComponent<AudioSource>();
    }

    //지속 판정
    void OnTriggerEnter2D(Collider2D col)
    {
        //플레이어에게 닿으면 공격
        if (col.CompareTag("Player"))
        {
            audio_source.Play();
        }
    }

    //지속 판정
    void OnTriggerStay2D(Collider2D col)
    {
        //플레이어에게 닿으면 공격
        if (col.CompareTag("Player"))
        {
            Attack();
        }
    }

    //지속 판정
    void OnTriggerExit2D(Collider2D col)
    {
        //플레이어에게 닿으면 공격
        if (col.CompareTag("Player"))
        {
            audio_source.Stop();
        }
    }

    // 속도 감소 함수
    private void Attack()
    {
        //슬로우 On
        player_data.slow = true;
        //타이머 0으로 초기화
        player_data.slow_timer.timer = 0f;
        //속도 감소
        player_data.speed = slow_speed;
    }
}
