using UnityEngine;
using System.Collections;

public class EndingPlayer : MonoBehaviour {

    public GameObject shadow_ending;
    public GameObject surprise;
    private iTweenPath path;

    // Use this for initialization
    void Start () {
        path = GetComponent<iTweenPath>();
    }

    public void Move()
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("EndingPath"), "easetype", iTween.EaseType.linear, "speed", 12f));
    }

    void Update()
    {
        CheckNode(6);
    }

    //노드 체크 함수
    private void CheckNode(int _index)
    {
        Vector3 my_position = transform.localPosition;
        Vector3 target_position = path.nodes[_index];

        float x_min = target_position.x - 3f, x_max = target_position.x + 3f;
        
        if (x_min <= my_position.x && my_position.x <= x_max)
        {
            shadow_ending.SetActive(true);
            surprise.SetActive(false);
            Player.instance.anim.Play("Idle");
            gameObject.SetActive(false);
        }
    }
}
