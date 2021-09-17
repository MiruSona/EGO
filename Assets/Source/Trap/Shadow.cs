using UnityEngine;
using System.Collections;

public class Shadow : SingleTon<Shadow> {

    //참조
    private GameDAO.PlayerData player_data;
    private iTweenPath path;
    private Animator anim;
    private Vector3 scale;

    //움직이기
    public enum Movement
    {
        Left,
        Right,
        Up,
        Down
    }
    public Movement movement = Movement.Left;

	//초기화(움직임 포함)
	void Start () {
        player_data = GameDAO.instance.player_data;
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("ShadowPath"), "easetype", iTween.EaseType.linear, "speed", 2.5f));
        path = GetComponent<iTweenPath>();
        anim = GetComponent<Animator>();

        anim.SetBool("MoveRight", true);
        anim.SetBool("MoveUp", false);
        anim.SetBool("MoveDown", false);

        scale = transform.localScale;
    }

    void Update()
    {
        if(player_data.state == GameDAO.PlayerData.State.isDead)
        {
            iTween.Stop();
        }

        CheckNode(1, Movement.Down);
        CheckNode(2, Movement.Left);
        CheckNode(3, Movement.Up);
        CheckNode(5, Movement.Right);
        CheckNode(8, Movement.Down);
        CheckNode(9, Movement.Up);
        CheckNode(10, Movement.Right);
        CheckNode(11, Movement.Down);
        CheckNode(13, Movement.Left);
        CheckNode(14, Movement.Down);
        CheckNode(15, Movement.Left);
        CheckNode(16, Movement.Up);

        switch (movement)
        {
            case Movement.Left:

                Vector3 scale_temp = scale;
                scale_temp.x *= -1;
                transform.localScale = scale_temp;

                anim.SetBool("MoveRight", true);
                anim.SetBool("MoveUp", false);
                anim.SetBool("MoveDown", false);
                break;
                
            case Movement.Right:

                transform.localScale = scale;

                anim.SetBool("MoveRight", true);
                anim.SetBool("MoveUp", false);
                anim.SetBool("MoveDown", false);
                break;

            case Movement.Up:

                anim.SetBool("MoveRight", false);
                anim.SetBool("MoveUp", true);
                anim.SetBool("MoveDown", false);
                break;

            case Movement.Down:
                
                anim.SetBool("MoveRight", false);
                anim.SetBool("MoveUp", false);
                anim.SetBool("MoveDown", true);
                break;
        }
    }

    //충돌 판정
    void OnCollisionEnter2D(Collision2D col)
    {
        //플레이어에게 닿으면 공격
        if (col.collider.CompareTag("Player"))
        {
            Attack();
        }
    }

    //노드 체크 함수
    private void CheckNode(int _index, Movement _movement)
    {
        Vector3 my_position = transform.localPosition;
        Vector3 target_position = path.nodes[_index];

        float x_min = target_position.x - 3f, x_max = target_position.x + 3f;
        float y_min = target_position .y - 3f, y_max = target_position.y + 3f;

        if (x_min <= my_position.x && my_position.x <= x_max)
        {
            if(y_min <= my_position.y && my_position.y <= y_max)
            {
                movement = _movement;
            }
        }
    }

    // 데미지 주는 함수
    private void Attack()
    {
        if (player_data.state == GameDAO.PlayerData.State.isAlive)
            player_data.SubHP(player_data.hp_max);
    }
}
