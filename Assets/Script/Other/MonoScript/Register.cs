﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Register : BaseMonoBehaviour {
    private Text NameText;
    private GameObject SureButton;
    private GameObject Middle;
    private GameObject MainShow;
    private List<Transform> MyTransforms = new List<Transform>();
    private InputField UserNameText;
    private InputField PassWordText;
    private InputField PassWordTextAgain;
    private InputField PhoneNumberText;
    private InputField PinText;
    private List<Vector3> Positions = new List<Vector3>();
    private List<Transform> Transforms = new List<Transform>();
    private GameObject BackButton;
    private void Init() {
        NameText = transform.FindChild("bgtop/Text").GetComponent<Text>();
        BackButton = transform.FindChild("bgtop/backbutton").gameObject;
        SureButton = transform.FindChild("surebutton").gameObject;
        GameTools.AddChangeTransform(SureButton.transform);
        Middle = transform.FindChild("middle").gameObject;
        UserNameText = transform.FindChild("middle/username/InputField").GetComponent<InputField>();
        PassWordText = transform.FindChild("middle/password/InputField").GetComponent<InputField>();
        PassWordTextAgain = transform.FindChild("middle/passwordagain/InputField").GetComponent<InputField>();
        PhoneNumberText = transform.FindChild("middle/phonenumber/InputField").GetComponent<InputField>();
        PinText = transform.FindChild("middle/pin/InputField").GetComponent<InputField>();
        MainShow = transform.parent.FindChild("main").gameObject;
    }
    //记录原始位置
    private void AddPos() {
        for (int i = 0; i < 5; i++) {
            Transform info = Middle.transform.GetChild(i).transform;
            Transforms.Add(info);
            Positions.Add(info.localPosition);
        }
    }
    void Awake() {
        Init();
        AddPos();
        SetScreen();
        AddListen();
    }
    private void AddListen() {
        EventTriggerListener.Get(BackButton).onClick = GameTools.ClickBack;
    }
    //自适应
    private void SetScreen() {
        GameTools.SetPosByScreen();
        int index = Middle.transform.childCount;
        for (int i = 1; i < index; i++) {
            Middle.transform.GetChild(i).localPosition += new Vector3(0, i*GameTools.ModifyY*0.5f/index, 0);
        }
    }
    //点击注册界面确定按钮
    public void ClickSureRegister(GameObject button)
    {
        Validate();
    }
    //点击找回密码确定按钮
    public void ClickSurePassword(GameObject button)
    {
        ValidatePassword();
    }
    //验证注册账号数据
    private void Validate()
    {
        if (GameTools.CheckUserName(UserNameText))
           return;
       if (GameTools.CheckPassWord(PassWordText))
           return;
       if (GameTools.CheckPassWordAgain(PassWordTextAgain))
           return;
       if (GameTools.CheckPhoneNumber(PhoneNumberText))
           return;
       if (GameTools.CheckPin(PinText))
           return;
       //请求服务器数据
       CallSeverBack(new SeverData());
    }
    //验证找回密码数据
    private void ValidatePassword() {
        if (GameTools.CheckUserName(UserNameText))
            return;
        if (GameTools.CheckPhoneNumber(PhoneNumberText))
            return;
        if (GameTools.CheckPin(PinText))
            return;
        if (GameTools.CheckPassWord(PassWordText))
            return;
        if (GameTools.CheckPassWordAgain(PassWordTextAgain))
            return;
        //请求服务器数据
        CallSeverBack(new SeverData());
    }
    
    //向服务器发送数据注册账号
    private void CallSever(SeverData info) {
        OpenMain();
    }
    //服务器回调函数
    public void CallSeverBack(SeverData info) {
        //登录到主界面
        //登录到主界面
        GameInit.Instance.OpenMap();
    }
    //打开注册界面
    public override void Open()
    {
        //gameObject.SetActive(true);
        Recover();
        NameText.text = "注册账号";
        for (int i = 0; i < 5; i++) {
            Transforms[i].localPosition = Positions[i];
        }
        EventTriggerListener.Get(SureButton).onClick = ClickSureRegister;
    }
    //打开找回密码界面
    public override void OpenAnother()
    {
        //gameObject.SetActive(true);
        Recover();
        NameText.text = "找回密码";
        Transforms[3].localPosition = Positions[1];
        Transforms[4].localPosition = Positions[2];
        Transforms[1].localPosition = Positions[3];
        Transforms[2].localPosition = Positions[4];
        EventTriggerListener.Get(SureButton).onClick = ClickSurePassword;
    }
    //还原界面
    private void Recover()
    {
        for (int i = 0; i < 5; i++)
        {
            Transforms[i].GetChild(0).GetComponent<InputField>().text = "";
        }
    }
    //打开主界面
    public void OpenMain() {
        GameTools.OpenUI(MainShow);    
    }
}
