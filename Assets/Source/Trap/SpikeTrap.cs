using UnityEngine;
using System.Collections;

public class SpikeTrap : MonoBehaviour
{
    //참조
    private GameDAO.PlayerData player_data;
    private Animator tree_anim;
    private GameObject child;
    private AudioSource audio_source;
    private AudioClip audio_clip;

    //공격력
    public float atk = 1.0f;

    //초기화
    void Start()
    {
        player_data = GameDAO.instance.player_data;
        child = transform.FindChild("Tree").gameObject;
        tree_anim = child.GetComponent<Animator>();
        audio_source = GetComponent<AudioSource>();
        audio_clip = Resources.Load<AudioClip>("Sound/Effect/steel2");
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
        audio_source.PlayOneShot(audio_clip);
        if (player_data.state == GameDAO.PlayerData.State.isAlive)
            player_data.SubHP(atk);
        child.SetActive(true);
        tree_anim.Play("Spike_Up");
    }
}
