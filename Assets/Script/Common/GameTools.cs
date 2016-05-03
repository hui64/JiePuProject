using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using cn.sharesdk.unity3d;
/// <summary>
/// 公用类 
/// </summary>
public class GameTools{

    ///===============================================================================================
    //声明委托
    ///===============================================================================================
    public delegate void Finish();
    public delegate void Finish<T>(T t);
    ///===============================================================================================
    //配置数据
    ///===============================================================================================
    public const float ScreenX = 9;
    public const float ScreenY = 16;
    public const float TextureX = 720f;
    public const float TextureY = 1280f;
    ///===============================================================================================
    //运行数据
    ///===============================================================================================
    public static Canvas UICanvas;
    public static int ModifyY = 0;                                        //自适应修正值
    public static float Scale = 1f;                                       //自适应倍数
    public static Vector2 ScaleVector2 = Vector2.one;
    public static bool IsBigger = false;                                   //是否是变大
    public static List<Transform> ChangeTransforms = new List<Transform>();//需要修改的位置
    public static bool IsHaveUser = false;                                 //是否输入了账号,密码
    public static Text WarnText;                                            //错误提示 
    public static GameObject Wait;
    public static GameObject Map;
    public static bool IsOpenSet = false;
    public static GameObject QuitWarn;
    ///===============================================================================================
    //默认数据
    ///===============================================================================================
    public static GameObject DefaultPerfab;
    public static Sprite DefaultSprite;
    //添加点击监听
    public static void AddClickListen(GameObject button, EventTriggerListener.VoidDelegate OnClick)
    {
        Button info = button.GetComponent<Button>();
        if (info == null)
            info = button.AddComponent<Button>();
        info.onClick.AddListener(delegate() {
            OnClick(button); 
        });
    }
    //子物体添加监听
    public static void AddChildListen(GameObject grid, EventTriggerListener.VoidDelegate OnClick)
    {
        int index = grid.transform.childCount;
        for (int i = 0; i < index; i++)
        {
            AddClickListen(grid.transform.GetChild(i).gameObject, OnClick);
        }
    }
    //设置自适应修正值
    public static void SetScreenModify() {
        float diff = ScreenX / ScreenY;
        float screenDiff = Screen.width / (float)(Screen.height);
        float modify = 1.0f;
        if (screenDiff >= diff)
        {
            IsBigger = false;
            modify = Screen.height / TextureY;
            Scale =  screenDiff / diff;
            ScaleVector2 = new Vector2(Scale, Scale);
            ModifyY = (int)(Screen.height * (Scale - 1f) / modify / Scale);
        }
        else {
            IsBigger = true;
            modify = Screen.width / TextureX;
            float height = Screen.width / diff;
            ModifyY = -(int)((Screen.height - height) / modify);
        }
    }
    //自适应设置mask区域跟物品的位置
    public static void SetPosAndMaskByScreen(List<RectTransform> scrolls)
    {
        if (IsBigger)
        {
            foreach(RectTransform info in scrolls)
                info.sizeDelta -= new Vector2(0, ModifyY);
        }
        SetPosByScreen();
    }
    //自适应设置物品的位置
    public static void SetPosByScreen() {
        foreach (Transform info in ChangeTransforms) {
            info.localPosition += new Vector3(0, ModifyY, 0); 
        }
        ChangeTransforms.Clear();
    }
    //显示提示信息
    public static void ShowWarnText(string myString, Transform myTransform) {
        WarnText.gameObject.SetActive(true);
        WarnText.transform.position = myTransform.position;
        WarnText.text = myString;
    }
    //关闭提示信息
    public static void CloseWarnText() {
        WarnText.gameObject.SetActive(false);
    }
    //scrollview 循环列表
    public static void AddChild(){
        
    }
    //添加需要修改的位置
    public static void AddChangeTransform(Transform info) {
        ChangeTransforms.Add(info);
    }
    //验证用户名
    public static bool CheckUserName(InputField userNameText)
    {
        if (userNameText.text == "" || userNameText.text == "请输入账号")
        {
            GameTools.ShowWarnText("请输入账号", userNameText.transform);
            return true;
        }
        WarnText.gameObject.SetActive(false);
        return false;
    }
    private static string Password = "";
    //验证密码
    public static bool CheckPassWord(InputField passWordText)
    {
        Password = passWordText.text;
        if (Password.Equals(""))
        {
            GameTools.ShowWarnText("密码不能为空", passWordText.transform);
            return true;
        }
        WarnText.gameObject.SetActive(false);
        return false;
    }
    //验证密码是否跟前面输入的一致
    public static bool CheckPassWordAgain(InputField passWordTextAgain)
    {
        if (passWordTextAgain.text != Password)
        {
            GameTools.ShowWarnText("密码不一致", passWordTextAgain.transform);
            return true;
        }
        WarnText.gameObject.SetActive(false);
        return false;
    }
    //验证手机号
    public static bool CheckPhoneNumber(InputField phoneNumberText)
    {
        if (phoneNumberText.text == "")
        {
            GameTools.ShowWarnText("请输入手机号", phoneNumberText.transform);
            return true;
        }
        if (phoneNumberText.text.Length != 11)
        {
            GameTools.ShowWarnText("手机号应该为11位", phoneNumberText.transform);
            return true;
        }
        WarnText.gameObject.SetActive(false);
        return false;
    }
    //验证验证码
    public static bool CheckPin(InputField pinText)
    {
        if (pinText.text == "")
        {
            GameTools.ShowWarnText("验证码不能为空", pinText.transform);
            return true;
        }
        WarnText.gameObject.SetActive(false);
        return false;
    }
    //点击button时表现
    public static GameObject ClickChangeButton(GameObject choose, GameObject item) {
        choose.transform.GetChild(0).gameObject.SetActive(true);
        choose.transform.GetChild(1).gameObject.SetActive(false);
        item.transform.GetChild(0).gameObject.SetActive(false);
        item.transform.GetChild(1).gameObject.SetActive(true);
        return item;
    }
    //点击菜单时界面切换
    public static GameObject ClickChangeShow(GameObject oldShow, GameObject newShow) {
        oldShow.SetActive(false);
        newShow.SetActive(true);
        return newShow;
    }
   
    //界面切换信息
    public class ViewData {
        public GameObject go;
        public OpenUIDelegate OpenCall;
    }
    // 操作缓存
    private static Stack<ViewData> Cache;
    //设置开始缓存的界面
    public static void InitNavigations(GameObject go){
        Cache = new Stack<ViewData>();
        ViewData info = new ViewData();
        info.go = go;
        Cache.Push(info);
    }
    //跳转界面要执行的方法
    public delegate void OpenUIDelegate();
    public static void OpenUI(GameObject go, OpenUIDelegate Open = null) {
        WarnText.gameObject.SetActive(false);
        WaitTime();
        ViewData info = new ViewData();
        Cache.Peek().go.SetActive(false);
        go.SetActive(true);
        info.go = go;
        if (Open != null)
        {
            Open();
            info.OpenCall = Open;
        }
        Cache.Push(info);
    }
    public static void WaitTime() {
        Wait.SetActive(true);
    }
    //界面后退
    public static void ClickBack(GameObject button){
        //最后一个界面退出 
        if (Cache.Count <= 1)
        {
            Quit();
            return;
        }
        ViewData info = Cache.Pop();
        info.go.SetActive(false);
        if (info.OpenCall != null)
            info.OpenCall();
        if (Cache.Count > 0) {
            Cache.Peek().go.SetActive(true);
            if (Cache.Peek().OpenCall != null)
                Cache.Peek().OpenCall();
        }
    }
    //添加子物体
    public static void AddChild(GameObject father, GameObject child) {
        child.transform.parent = father.transform;
        child.transform.localScale = Vector3.one;
    }
    //公共分享
    public static void ShareToThird(PlatformType platform, ShareContent content)
    {
        ShareSDK.Instance.ShareContent(platform, content);
    }
    //设置分享回调方法
    public static void SetShareCallBack(ShareSDK.EventHandler callback)
    {
        ShareSDK.Instance.shareHandler = callback;
    }
    //获取sprite
    public static Sprite GetSprite(Texture2D texture)
    {
        Rect rect = new Rect(0f, 0f, texture.width, texture.height);
        return Sprite.Create(texture, rect, Vector2.one);
    }
    private static bool IsCanQuit = false;
    //退出游戏
    public static void Quit(GameObject button = null) {
        if (IsCanQuit == true){
            Application.Quit();
            return;
        }
        QuitWarn.SetActive(true);
        IsCanQuit = true;
    }
    //清除assetbundle
    public static void ClearAssetBundle(AssetBundle assetbundle) {
        assetbundle.Unload(false);
    }
    //获取assetbunle里的资源
    public static T GetAsset<T>(AssetBundle assetbundle, Finish<T> finish = null) {
        T[] info = assetbundle.LoadAllAssets<Object>() as T[];
        ClearAssetBundle(assetbundle);
        if (finish != null) {
            finish(info[0]);
        }
        return info[0];
    }
    //=====================================================================================
    //加载assetbunle里的所有资源(没有跟资源添加引用, 如果垃圾回收不能回收, 还需重新设计)
    //======================================================================================
    public static void LoadAllAssets(AssetBundle assetbundle) {
        Debug.LogWarning("检测内存, 可能会有内存泄漏");
        assetbundle.LoadAllAssets();
    }
}
