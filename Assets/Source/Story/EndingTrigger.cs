using UnityEngine;
using System.Collections;

public class EndingTrigger : MonoBehaviour {

    public EndingPlayer ending_player;
    public UIController ui_controller;
    public GameObject surprise;
    public GameObject[] DisablePanel = new GameObject[4];

    //충돌 판정
    void OnTriggerEnter2D(Collider2D col)
    {
        //플레이어에게 닿으면 엔딩
        if (col.CompareTag("Player"))
        {
            ui_controller.MoveStop();

            for (int i = 0; i < DisablePanel.Length; i++)
                DisablePanel[i].SetActive(true);

            Player.instance.anim.Play("Player_Ending");

            surprise.SetActive(true);
            Vector3 pos = Player.instance.transform.localPosition;
            pos.y += 3f;
            surprise.transform.localPosition = pos;

            ending_player.Move();
            gameObject.SetActive(false);
        }
    }
}
