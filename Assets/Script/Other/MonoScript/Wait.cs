using UnityEngine;
using System.Collections;

public class Wait : MonoBehaviour {
    private Transform WaitTran;
	// Use this for initialization
	void Awake () {
        WaitTran = transform.FindChild("bg/Image");
	}
    void Start() {
        //StartCoroutine(XuanZhuan(WaitTran));
    }
    void OnEnable() {
        StartCoroutine(XuanZhuan(WaitTran));
    }
    int Time = 0;
    private IEnumerator XuanZhuan(Transform myTransform) {
        while (true) {
            Time++;
            myTransform.Rotate(0, 0, -10);
            if (Time > 10)
            {
                Time = 0;
                gameObject.SetActive(false);
                yield break;
            }
            yield return null;
        }
    }
	
}
