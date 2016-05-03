﻿using UnityEngine;
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
        GameTools.SetScreenModify();// 设置分辨率参数
    }
	void Start () {
        //Init();
	}
    void Update() {
        //if (Input.anyKeyDown)
        //    GameTools.QuitWarn.SetActive(false);
        //if (Input.GetKeyDown(KeyCode.Escape))
        //    if(!GameTools.IsOpenSet)
        //        GameTools.ClickBack(gameObject);
    }
    private void Init() {
        Scale = transform.FindChild("scale").gameObject;
        Scale.transform.localScale = new Vector3(GameTools.Scale, GameTools.Scale, GameTools.Scale);
        GameTools.WarnText = transform.FindChild("scale/warn").GetComponent<Text>();
        GameTools.QuitWarn = transform.FindChild("scale/warnquit").gameObject;
        StartShow = transform.FindChild("scale/start").gameObject;
        GameTools.InitNavigations(StartShow);
        GameTools.Wait = transform.FindChild("scale/wait").gameObject;
        GameTools.Map = transform.FindChild("Map/map").gameObject;
        MainShow = transform.FindChild("scale/main").gameObject;
        EventTriggerListener.Get(transform.FindChild("Map/top/mapbutton").gameObject).onClick= CloseMap;
        EventTriggerListener.Get(transform.FindChild("Map/bottom/menu0/5km").gameObject).onClick = Set5km;
        EventTriggerListener.Get(transform.FindChild("Map/bottom/menu0/10km").gameObject).onClick = Set10km;
        EventTriggerListener.Get(transform.FindChild("Map/bottom/menu0/30km").gameObject).onClick = Set30km;
        EventTriggerListener.Get(transform.FindChild("Map/bottom/menu0/city").gameObject).onClick = SetCity;
        EventTriggerListener.Get(transform.FindChild("Map/bottom/menu0/country").gameObject).onClick = Setcountry;
        EventTriggerListener.Get(transform.FindChild("Map/bottom/oldpos").gameObject).onClick = Setoldpos;
        
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