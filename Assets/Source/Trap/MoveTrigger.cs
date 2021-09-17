using UnityEngine;
using System.Collections;

public class MoveTrigger : MonoBehaviour {

    //참조
    private MoveTrap move_trap;

    //초기화
    void Start () {
        move_trap = gameObject.GetComponentInParent<MoveTrap>();
    }

    //충돌 판정
    void OnTriggerEnter2D(Collider2D col)
    {
        //플레이어에게 닿으면 공격
        if (col.CompareTag("Player"))
        {
            move_trap.Move();
            gameObject.SetActive(false);
        }
    }
}
