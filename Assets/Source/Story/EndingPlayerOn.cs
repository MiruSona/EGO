using UnityEngine;
using System.Collections;

public class EndingPlayerOn : MonoBehaviour {

    public GameObject ending_player;

    //충돌 판정
    void OnTriggerEnter2D(Collider2D col)
    {
        //플레이어에게 닿으면 엔딩
        if (col.CompareTag("Player"))
        {
            ending_player.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
