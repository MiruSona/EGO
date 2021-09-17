using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour {

    //참조
    private GameDAO.PlayerData player_data;
    private Transform player;
    private Transform player_light_transform;
    private Light player_light;
    private Light bg_light;

    //초기화
    void Start () {
        player_data = GameDAO.instance.player_data;
        player = Player.instance.transform;

        player_light_transform = transform.FindChild("PlayerLight");
        player_light = player_light_transform.GetComponent<Light>();
        //bg_light = transform.FindChild("BGLight").GetComponent<Light>();
    }

    // 빛 관리
    void Update()
    {
        //플레이어 빛 위치 변경
        Vector3 player_pos = player.localPosition;
        player_pos.z = -20f;
        player_light_transform.localPosition = player_pos;
    }
}
