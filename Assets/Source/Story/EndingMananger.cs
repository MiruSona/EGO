using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndingMananger : SingleTon<EndingMananger> {

    //참조
    private Image white_panel;
    private Image ending_panel;
    public GameObject shadow_ending;
    private Sprite[] ending_sprite = new Sprite[13];

    //bool
    public bool ending = false;
    private bool alpha_add = true;
    private bool show_ending_panel = false;

    //타이머
    private GameDAO.Timer ending_timer = new GameDAO.Timer(0, 0.2f);

    //인덱스
    private int ending_index = 0;

    // 초기화
    void Start () {
        white_panel = transform.FindChild("EDCanvas").FindChild("WhitePanel").GetComponent<Image>();
        ending_panel = transform.FindChild("EDCanvas").FindChild("EndingPanel").GetComponent<Image>();
        shadow_ending = transform.FindChild("ShadowEnding").gameObject;

        for (int i = 0; i < ending_sprite.Length; i++)
        {
            ending_sprite[i] = Resources.Load<Sprite>("Sprite/Ending/end_01_" + (i + 1));
        }
    }
	
	void Update () {

        if (!ending)
        {
            return;
        }

        if (!show_ending_panel)
        {
            white_panel.gameObject.SetActive(true);

            Color color = white_panel.color;
            if (alpha_add)
            {
                if (color.a < 1.0f)
                {
                    color.a += 0.005f;
                    white_panel.color = color;
                }
                else
                    alpha_add = false;
            }
            else
            {
                if (color.a > 0.3f)
                {
                    color.a -= 0.005f;
                    white_panel.color = color;
                }
                else
                {
                    show_ending_panel = true;
                    ending_panel.gameObject.SetActive(true);
                    white_panel.gameObject.SetActive(false);
                }
            }            
        }
        else
        {
            if (ending_timer.timer < ending_timer.term)
                ending_timer.timer += Time.deltaTime;
            else
            {
                if (ending_index < ending_sprite.Length)
                {
                    ending_panel.sprite = ending_sprite[ending_index];
                    ending_index++;
                }

                ending_timer.timer = 0f;
            }
        }
	}

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }
}
