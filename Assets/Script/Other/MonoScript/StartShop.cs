﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class StartShop : MonoBehaviour {
    private GameObject RegisterButton;
    private GameObject StartButton;
    private GameObject MainShow;
    private GameObject RegisterShow;
    private GameObject InputUserName;
    private InputField UserNameText;
    private InputField PassWordText;
    private GameObject SureButton;
    private GameObject LosePassword;
    private GameObject Log;
    void Awake() {
        //Init();
        GameTools.SetPosByScreen();
        //AddListen();
    }
    void Start() {
       
    }
    private void AddListen() {
        EventTriggerListener.Get(StartButton).onClick = ClickStart;
        EventTriggerListener.Get(RegisterButton).onClick = ClickRegister;
        EventTriggerListener.Get(SureButton).onClick = ClickSure;
        EventTriggerListener.Get(LosePassword).onClick = ClickLosePassword;
    }
    private void Init() {
        RegisterButton = transform.FindChild("inputusername/registerbutton").gameObject;
        GameTools.AddChangeTransform(RegisterButton.transform);
        StartButton = transform.FindChild("loginbutton").gameObject;
        GameTools.AddChangeTransform(StartButton.transform);
        MainShow = transform.parent.FindChild("main").gameObject;
        RegisterShow = transform.parent.FindChild("register").gameObject;
        InputUserName = transform.FindChild("inputusername").gameObject;
        GameTools.AddChangeTransform(InputUserName.transform);
        UserNameText = transform.FindChild("inputusername/username/InputField").GetComponent<InputField>();
        PassWordText = transform.FindChild("inputusername/password/InputField").GetComponent<InputField>();
        SureButton = transform.FindChild("inputusername/surebutton").gameObject;
        LosePassword = transform.FindChild("inputusername/losepassword").gameObject;
        Log = transform.FindChild("log").gameObject;
    }
    //点击登录界面
    public void ClickStart(GameObject button) {
        RegisterButton.SetActive(false);
        StartButton.SetActive(false);
        InputUserName.SetActive(true);
        Log.transform.localPosition += new Vector3(0, 50f, 0);
    }
    //点击注册界面
    public void ClickRegister(GameObject button) {
        GameTools.OpenUIDelegate function = RegisterShow.GetComponent<Register>().Open;
        GameTools.OpenUI(RegisterShow, function);
    }
    //点击忘记密码
    public void ClickLosePassword(GameObject button)
    {
        GameTools.OpenUIDelegate function = RegisterShow.GetComponent<Register>().OpenAnother;
        GameTools.OpenUI(RegisterShow, function);
    }
    //点击账号确认按钮
    public void ClickSure(GameObject button) {
        ValiDate();
        //切换到地图
        GameInit.Instance.OpenMap();
    }
    //验证账号密码
    public void ValiDate() {
        if (GameTools.CheckUserName(UserNameText))
            return;
        if (GameTools.CheckPassWord(PassWordText))
            return;
    }
}
