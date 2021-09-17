using UnityEngine;
using System.Collections.Generic;

public class Player : SingleTon<Player> {
    
    //참조
    private Rigidbody2D rb2d;
    public Animator anim;
    private SpriteRenderer sprite_renderer;
    private Vector3 scale;
    private GameDAO.PlayerData player_data;
    private AudioSource audio_source;
    private AudioClip audio_clip;
    private ParticleSystem blood_particle;
    private ParticleSystem water_particle;

    //실행 여부
    private bool do_dead_anim = false;

    //마찰력
    private float friction = 0.8f;

    //초기화
    void Start() {
        //레퍼런스 초기화
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        scale = transform.localScale;
        audio_source = GetComponent<AudioSource>();
        audio_clip = Resources.Load<AudioClip>("Sound/Effect/Hit");
        blood_particle = transform.FindChild("Blood").GetComponent<ParticleSystem>();
        water_particle = transform.FindChild("Water").GetComponent<ParticleSystem>();

        //DAO 초기화
        player_data = GameDAO.instance.player_data;
    }

    //움직임 처리
    void FixedUpdate() {

        //상태 처리
        switch (player_data.state)
        {
            case GameDAO.PlayerData.State.isDamaged:
                Damaged();
                break;

            case GameDAO.PlayerData.State.isDead:
                Dead();
                break;
        }

        //슬로우 디버프 걸리면
        if (player_data.slow)
        {
            Slow();
        }
        
        switch (player_data.movement)
        {
            //대기
            case GameDAO.PlayerData.Movement.isReady:
                anim.SetBool("MoveRight", false);
                anim.SetBool("MoveUp", false);
                anim.SetBool("MoveDown", false);
                break;

            //움직임 초기화
            case GameDAO.PlayerData.Movement.isMove:
                
                //이미지 방향 전환
                transform.localScale = new Vector3(scale.x * player_data.facing, scale.y, scale.z);

                //움직임
                switch (player_data.move_direction)
                {
                    case GameDAO.PlayerData.MoveDirection.Left:
                    case GameDAO.PlayerData.MoveDirection.Right:
                        anim.SetBool("MoveRight", true);
                        anim.SetBool("MoveUp", false);
                        anim.SetBool("MoveDown", false);
                        break;

                    case GameDAO.PlayerData.MoveDirection.Up:
                        anim.SetBool("MoveRight", false);
                        anim.SetBool("MoveUp", true);
                        anim.SetBool("MoveDown", false);
                        break;

                    case GameDAO.PlayerData.MoveDirection.Down:
                        anim.SetBool("MoveRight", false);
                        anim.SetBool("MoveUp", false);
                        anim.SetBool("MoveDown", true);
                        break;
                }

                //다음단계로
                player_data.movement = GameDAO.PlayerData.Movement.isMoveSchedule;

                break;

            //움직임 업데이트
            case GameDAO.PlayerData.Movement.isMoveSchedule:
                
                //움직임
                switch (player_data.move_direction)
                {
                    case GameDAO.PlayerData.MoveDirection.Left:
                    case GameDAO.PlayerData.MoveDirection.Right:
                        transform.Translate(player_data.speed * player_data.facing, 0f, 0f);
                        break;

                    case GameDAO.PlayerData.MoveDirection.Up:
                    case GameDAO.PlayerData.MoveDirection.Down:
                        transform.Translate(0f, player_data.speed * player_data.facing, 0f);
                        break;
                }

                break;
        }
    }

    private void Damaged()
    {
        //색변화
        Color color = sprite_renderer.color;
        color.r = 1.0f;
        color.g = 0.5f;
        color.b = 0.5f;
        color.a = 0.3f;
        sprite_renderer.color = color;

        //사운드
        if(player_data.damaged_timer.timer == 0)
        {
            audio_source.PlayOneShot(audio_clip);
            blood_particle.Emit(12);
        }

        //타이머
        if (player_data.damaged_timer.timer < player_data.damaged_timer.term)
            player_data.damaged_timer.timer += Time.deltaTime;
        else
        {
            //색 되돌림
            sprite_renderer.color = Color.white;
            //상태 되돌림
            player_data.state = GameDAO.PlayerData.State.isAlive;
            //타이머 초기화
            player_data.damaged_timer.timer = 0f;
        }
    }

    private void Dead()
    {
        if (!do_dead_anim)
        {
            anim.Play("Die");
            anim.SetBool("Dead", true);
            do_dead_anim = true;
        }
    }

    private void Slow()
    {
        //색변화
        Color color = sprite_renderer.color;
        color.r = 0.0f;
        color.g = 0.7f;
        color.b = 0.0f;
        sprite_renderer.color = color;

        if (player_data.slow_timer.timer == 0)
            water_particle.Emit(25);

        //타이머
        if (player_data.slow_timer.timer < player_data.slow_timer.term)
            player_data.slow_timer.timer += Time.deltaTime;
        else
        {
            //색 되돌림
            sprite_renderer.color = Color.white;
            //속도 되돌림
            player_data.speed = 0.1f;
            //상태 되돌림
            player_data.slow = false;
            //타이머 초기화
            player_data.slow_timer.timer = 0f;
        }
    }
}
