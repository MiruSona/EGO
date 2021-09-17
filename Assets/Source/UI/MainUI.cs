using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainUI : MonoBehaviour {

    private Image title;
    private Sprite[] title_sprite = new Sprite[13];
    private int index = 0;
    private GameDAO.Timer timer = new GameDAO.Timer(0,1f);

	// Use this for initialization
	void Start () {
        title = transform.FindChild("Image").GetComponent<Image>();
        for (int i = 0; i < title_sprite.Length; i++)
        {
            title_sprite[i] = Resources.Load<Sprite>("Sprite/Opening/Ego_" + i);
        }
    }
	
	// Update is called once per frame
	void Update () {

        if(timer.timer < timer.term)
        {
            timer.timer += Time.deltaTime;
        }
        else
        {
            if (index < title_sprite.Length)
            {
                title.sprite = title_sprite[index];
                index++;
            }
        }
	}

    public void StartGame()
    {
        SceneManager.LoadScene("InGame");
    }
}
