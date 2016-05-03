using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuGroup : MonoBehaviour {
    private GridLayoutGroup MyLayout;
    public int speed = 2;
	// Use this for initialization
	void Awake () {
        MyLayout = transform.GetComponent<GridLayoutGroup>();
	}
    void OnEnable() {
        StartCoroutine("GetDown");
    }
    //下拉
    private IEnumerator GetDown() {
        while (true) {
            MyLayout.spacing += new Vector2(0, speed);
            if (MyLayout.spacing.y >= 0)
            {
                MyLayout.spacing = new Vector2(0, -4);
                yield break;
            }
            yield return null;
        }
    }
	
	
}
