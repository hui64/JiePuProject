using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameInit : MonoBehaviour {
    public static GameInit Instance; 
    private float MaxTime = 15.0f;
    private GameObject StartShow;
    private GameObject MainShow;
    private GameObject Scale;
    void Awake() {
        Instance = this;
        Init();
    }
	void Start () {
        
	}
    void Update() {
        
    }
    private void Init() {
        #if UNITY_ANDROID
        GameTools.IsIphone = false;
        #elif UNITY_IPHONE 
        GameTools.IsIphone = true;
        #endif
    }
    public void OpenMap()
    {
        OpenMain();
    }
    //倒计时 有事件输入的时候从新倒计时
    public IEnumerator CountDownOpenMain()
    {
        float time = 0;
        while (time < MaxTime)
        {
            time += Time.deltaTime;
            if (Input.anyKey)
            {
                time = 0;
            }
            yield return null;
        }
        CloseMap(gameObject);
        OpenMain();
    }
    //登录到主界面
    public void OpenMain()
    {
        GameTools.OpenUI(MainShow);
        UIMove.Instance.MoveUI(MainShow.transform);
    }
    // ================================================================================
    // Button事件
    // ================================================================================
    public void Set5km(GameObject button)
    {
        //DituUI.Instance.SetZ(18);
        Debug.Log(button.name);
    }
    public void Set10km(GameObject button)
    {
        //DituUI.Instance.SetZ(14);
        Debug.Log(button.name);
    }
    public void Set30km(GameObject button)
    {
        //DituUI.Instance.SetZ(11);
        Debug.Log(button.name);
    }
    public void SetCity(GameObject button)
    {
        //DituUI.Instance.SetZ(9);
        Debug.Log(button.name);
    }
    public void Setcountry(GameObject button)
    {
        //DituUI.Instance.SetZ(6);
        Debug.Log(button.name);
    }
    public void Setoldpos(GameObject button)
    {
        //DituUI.Instance.Reset();
    }
    public void OpenMap(GameObject button) {
        OpenMap();
    }
    //关闭地图
    public void CloseMap(GameObject button) {
        GameTools.Map.transform.parent.gameObject.SetActive(false);
        StopCoroutine("CountDownOpenMain");
       //DituUI.Instance.Close();
    }
   
}
