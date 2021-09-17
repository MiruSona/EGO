using UnityEngine;
using System.Collections;

public class ShadowEnding : MonoBehaviour
{
    private float amount = 15f;
    private float time = 15.0f;
    
    //초기화
    void Start () {
        iTween.MoveAdd(gameObject, new Vector3(0, amount, 0), time);
    }

    //충돌 판정
    void OnTriggerEnter2D(Collider2D col)
    {
        //플레이어에게 닿으면 엔딩
        if (col.CompareTag("Player"))
        {
            Ending();
        }
    }

    private void Ending()
    {
        EndingMananger.instance.ending = true;
        gameObject.SetActive(false);
    }
}
