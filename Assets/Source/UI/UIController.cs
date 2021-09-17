using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour {

    private GameDAO.PlayerData player_data;
    private Image dead_panel;
    private Image start_panel;
    private GameDAO.Timer dead_timer = new GameDAO.Timer(0, 0.2f);
    private bool alpha_add = true;
    private GameDAO.Timer start_timer = new GameDAO.Timer(0, 0.2f);

    //초기화
    void Start () {
        player_data = GameDAO.instance.player_data;
        start_panel = transform.FindChild("UICanvas").FindChild("StartPanel").GetComponent<Image>();
        dead_panel = transform.FindChild("UICanvas").FindChild("DeadPanel").GetComponent<Image>();

        start_panel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (start_panel.color.a > 0f)
        {
            Color color = start_panel.color;
            color.a -= 0.005f;
            start_panel.color = color;
        }
        else
        {
            start_panel.gameObject.SetActive(false);
            start_timer.timer = start_timer.term * 2;
            Time.timeScale = 1;
        }

        if(player_data.state == GameDAO.PlayerData.State.isDead)
        {
            dead_panel.gameObject.SetActive(true);

            Color color = dead_panel.color;
            if (alpha_add)
            {
                if (color.a < 1.0f)
                {
                    color.a += 0.005f;
                    dead_panel.color = color;
                }
                else
                    SceneManager.LoadScene("InGame");
            }
        }
    }

    public void MoveLeft()
    {
        player_data.facing = -1;
        player_data.move_direction = GameDAO.PlayerData.MoveDirection.Left;
        player_data.movement = GameDAO.PlayerData.Movement.isMove;
    }

    public void MoveRight()
    {
        player_data.facing = 1;
        player_data.move_direction = GameDAO.PlayerData.MoveDirection.Right;
        player_data.movement = GameDAO.PlayerData.Movement.isMove;
    }

    public void MoveUp()
    {
        player_data.facing = 1;
        player_data.move_direction = GameDAO.PlayerData.MoveDirection.Up;
        player_data.movement = GameDAO.PlayerData.Movement.isMove;
    }

    public void MoveDown()
    {
        player_data.facing = -1;
        player_data.move_direction = GameDAO.PlayerData.MoveDirection.Down;
        player_data.movement = GameDAO.PlayerData.Movement.isMove;
    }

    public void MoveStop()
    {
        player_data.movement = GameDAO.PlayerData.Movement.isReady;
    }

}
