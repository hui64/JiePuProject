using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIMainShow : BaseMonoBehaviour {
    private GameObject ScrollShow;
    private ScrollRect ShowRect;
    private Transform Middle;
    private Transform Show;
    private Transform Top;
    private GameObject Goods;
    private Transform TopEmpty;
    private Transform MiddleEmpty;
    private Transform Bottom;
    private GameObject TuiJianShow;
    private GameObject GuangJieShow;
    private GameObject HistoryShow;
    private GameObject OwnShow;
    private GameObject TuiJianMenu;
    private GameObject GuangjieMenu;
    private GameObject HistoryMenu;
    private GameObject OwnMenu;
    private List<RectTransform> ScrollMasks;
    private GameObject StoreShow;
    private GameObject GoodShow;
    private GameObject TopGrid;
    private GameObject GuangJieGrid;
    private GameObject mapbutton;
    private GameObject DistanceButton;
    private GameObject SortButton;
    private GameObject ChooseButton;
    private GameObject OrderButton;
    private GameObject NewsButton;
    private GameObject SetButton;
    private GameObject CollectButton;
    private GameObject UserSafeButton;
    private GameObject ChangeDataButton;
    private GameObject OrderShow;
    private GameObject NewsShow;
    private GameObject CollectShow;
    private GameObject UserSafeShow;
    private GameObject SetShow;
    private GameObject CommentShow; //发表评论
    private GameObject CallSellShow;
    private GameObject MoneySafeShow;  //账户安全
    private GameObject HelpShow;
    private GameObject ShareShow;
    private GameObject AddressShow;
    private GameObject SetAddressShow;
    private GameObject ComplainShow; //投诉
    private GameObject ChangePassWordShow;
    private GameObject ChangePhoneShow;
    private GameObject ChangeUserShow;
    private GameObject GuanYuShow;
    private GameObject MapMiscShow;
    private GameObject ChangeShow;
    private GameObject ErWeiMaShow;
    private GameObject CallSellInputButton;
    private GameObject CallSellTalkButton;
    private GameObject CallSellJiaHaoButton;
    private Transform CallSellMenu0;
    private Transform CallSellPos;
    //9/16
    void Awake() {
        Init();
    }
    void Start() {
        AddScrollMasks();
        GameTools.SetPosAndMaskByScreen(ScrollMasks); //自适应
        AddListen();
    }
    private void AddListen() {
        EventTriggerListener.Get(TuiJianMenu).onClick = ClickBottomMenu;
        EventTriggerListener.Get(GuangjieMenu).onClick = ClickBottomMenu;
        EventTriggerListener.Get(HistoryMenu).onClick = ClickBottomMenu;
        EventTriggerListener.Get(OwnMenu).onClick = ClickBottomMenu;
        EventTriggerListener.Get(mapbutton).onClick = GameInit.Instance.OpenMap;
        GameTools.AddChildListen(TuiJianShow.transform.FindChild("show/goods/grid").gameObject, ClickGood);
        GameTools.AddChildListen(GuangJieGrid, ClickStore);
        GameTools.AddChildListen(HistoryShow.transform.FindChild("show/goods/grid").gameObject, ClickStore);
        GameTools.AddChildListen(TopGrid, ClickTopMenu);
        GameTools.AddClickListen(DistanceButton, ClickDistance);
        GameTools.AddClickListen(SortButton, ClickDistance);
        GameTools.AddClickListen(ChooseButton, ClickDistance);
        GameTools.AddClickListen(OrderButton, ClickOrder);
        GameTools.AddClickListen(NewsButton, ClickOpenNews);
        GameTools.AddClickListen(CollectButton, ClickCollect);
        GameTools.AddClickListen(UserSafeButton, ClickUserSafe);
        GameTools.AddClickListen(ChangeDataButton, ClickChangeDate);
        GameTools.AddClickListen(OrderShow.transform.FindChild("top/backbutton").gameObject, GameTools.ClickBack);
        GameTools.AddClickListen(NewsShow.transform.FindChild("top/backbutton").gameObject, GameTools.ClickBack);
        GameTools.AddClickListen(CollectShow.transform.FindChild("top/backbutton").gameObject, GameTools.ClickBack);
        GameTools.AddClickListen(UserSafeShow.transform.FindChild("top/backbutton").gameObject, GameTools.ClickBack);
        GameTools.AddClickListen(ChangeShow.transform.FindChild("top/backbutton").gameObject, GameTools.ClickBack);
        //打开头像设置
        GameTools.AddClickListen(ChangeShow.transform.FindChild("items/grid/new").gameObject, OpenAvater);
        GameTools.AddClickListen(ChangeShow.transform.FindChild("items/grid/new1").gameObject, OpenName);
        GameTools.AddClickListen(ChangeShow.transform.FindChild("items/grid/new2").gameObject, OpenSex);
        GameTools.AddClickListen(ChangeShow.transform.FindChild("items/grid/new3").gameObject, OpenErWeiMa);
        GameTools.AddClickListen(ChangeShow.transform.FindChild("items/grid/new4").gameObject, OpenAddress);
        GameTools.AddClickListen(ErWeiMaShow.transform.FindChild("top/backbutton").gameObject, GameTools.ClickBack);
        GameTools.AddClickListen(AddressShow.transform.FindChild("top/backbutton").gameObject, GameTools.ClickBack);

        GameTools.AddClickListen(SetButton, ClickSet);
        GameTools.AddClickListen(SetShow.transform.FindChild("top/backbutton").gameObject, GameTools.ClickBack);
        Transform setGrid = SetShow.transform.FindChild("items/grid");
        GameTools.AddClickListen(setGrid.GetChild(0).gameObject, ClickSetHelp);
        GameTools.AddClickListen(HelpShow.transform.FindChild("top/backbutton").gameObject, GameTools.ClickBack);
        //GameTools.AddClickListen(setGrid.GetChild(1).gameObject, ClickSetClear);
        //GameTools.AddClickListen(setGrid.GetChild(2).gameObject, ClickSetUpdate);
        GameTools.AddClickListen(setGrid.GetChild(3).gameObject, ClickSetGuanYu);
        GameTools.AddClickListen(GuanYuShow.transform.FindChild("top/backbutton").gameObject, GameTools.ClickBack);
        GameTools.AddChildListen(HelpShow.transform.FindChild("items/grid").gameObject, ClickHelpMap);
        GameTools.AddClickListen(MapMiscShow.transform.FindChild("top/backbutton").gameObject, GameTools.ClickBack);
        GameTools.AddClickListen(ChangeUserShow.transform.FindChild("bg").gameObject, CloseChangeUser);
        //打开聊天
        GameTools.AddChildListen(NewsShow.transform.FindChild("news/grid").gameObject, ClickNews);
        GameTools.AddClickListen(CallSellShow.transform.FindChild("top/backbutton").gameObject, GameTools.ClickBack);
        GameTools.AddClickListen(SetShow.transform.FindChild("bottom/Button").gameObject, GameTools.Quit);
        GameTools.AddClickListen(CallSellTalkButton, ClickTalk);
        GameTools.AddClickListen(CallSellInputButton, ClickInput);
        GameTools.AddClickListen(CallSellJiaHaoButton, ClickJiaHao);
    }
    private void AddScrollMasks() {
        ScrollMasks = new List<RectTransform>();
        ScrollMasks.Add(TuiJianShow.GetComponent<RectTransform>());
        ScrollMasks.Add(GuangJieShow.GetComponent<RectTransform>());
        ScrollMasks.Add(HistoryShow.GetComponent<RectTransform>());
        ScrollMasks.Add(OwnShow.GetComponent<RectTransform>());
    }
    private void Init() {
        ScrollShow = transform.FindChild("guangjie").gameObject;
        ShowRect = ScrollShow.GetComponent<ScrollRect>();
        Show = ScrollShow.transform.FindChild("guangjie");
        Top = ScrollShow.transform.FindChild("show/top");
        Middle = ScrollShow.transform.FindChild("show/middle/middle");
        TopEmpty = ScrollShow.transform.FindChild("top");
        MiddleEmpty = ScrollShow.transform.FindChild("show/middle");
        Bottom = transform.FindChild("bottom");
        GameTools.AddChangeTransform(Bottom.transform);
        TuiJianMenu = Bottom.transform.FindChild("menu0/items/tuijian").gameObject;
        GuangjieMenu = Bottom.transform.FindChild("menu0/items/guangjie").gameObject;
        ChooseDownMenu = TuiJianMenu;
        HistoryMenu = Bottom.transform.FindChild("menu0/items/history").gameObject;
        OwnMenu = Bottom.transform.FindChild("menu0/items/own").gameObject;
        ChooseShow = TuiJianShow = transform.FindChild("tuijian").gameObject;
        GuangJieShow = transform.FindChild("guangjie").gameObject;
        HistoryShow = transform.FindChild("history").gameObject;
        OwnShow = transform.FindChild("own").gameObject;
        StoreShow = transform.parent.FindChild("store/store").gameObject;
        GoodShow = transform.parent.FindChild("store/good").gameObject;
        TopGrid = Top.FindChild("grid").gameObject;
        GuangJieGrid = GuangJieShow.transform.FindChild("show/goods/grid").gameObject;
        mapbutton = transform.FindChild("top/mapbutton").gameObject;
        DistanceButton = Middle.FindChild("distance").gameObject;
        SortButton = Middle.FindChild("sort").gameObject;
        ChooseButton = Middle.FindChild("choose").gameObject;
        Transform ownItems = transform.FindChild("own/show/items");
        OrderButton = ownItems.FindChild("item").gameObject;
        NewsButton = ownItems.FindChild("item1").gameObject;
        CollectButton = ownItems.FindChild("item2").gameObject;
        UserSafeButton = ownItems.FindChild("item3").gameObject;
        ChangeDataButton = ownItems.FindChild("item4").gameObject;
        Transform own = transform.parent.FindChild("own");
        OrderShow = own.FindChild("order").gameObject;
        NewsShow = own.FindChild("news").gameObject;
        CollectShow = own.FindChild("collect").gameObject;
        UserSafeShow = own.FindChild("safe").gameObject;
        ChangeShow = own.FindChild("change").gameObject;
        SetButton = OwnShow.transform.FindChild("show/top/set").gameObject;
        SetShow = own.FindChild("set").gameObject;
        CommentShow = own.FindChild("comment").gameObject;
        CallSellShow = own.FindChild("callsell").gameObject;
        MoneySafeShow = own.FindChild("moneysafe").gameObject;
        HelpShow = own.FindChild("help").gameObject;
        AddressShow = own.FindChild("address").gameObject;
        SetAddressShow = own.FindChild("setaddress").gameObject;
        ComplainShow = own.FindChild("complain").gameObject;
        ChangePassWordShow = own.FindChild("changepassword").gameObject;
        ChangePhoneShow = own.FindChild("changephone").gameObject;
        ChangeUserShow = own.FindChild("changeuser").gameObject;
        GuanYuShow = own.FindChild("guanyu").gameObject;
        MapMiscShow = own.FindChild("mapmisc").gameObject;
        ErWeiMaShow = own.FindChild("erweima").gameObject;
        CallSellInputButton = CallSellShow.transform.FindChild("bottom/menu0/top/talk/inputbutton").gameObject;
        CallSellTalkButton = CallSellShow.transform.FindChild("bottom/menu0/top/input/talkbutton").gameObject;
        CallSellJiaHaoButton = CallSellShow.transform.FindChild("bottom/menu0/top/jiahao").gameObject;
        CallSellMenu0 = CallSellShow.transform.FindChild("bottom/menu0");
        CallSellPos = CallSellShow.transform.FindChild("bottom/pos");

    }
    private bool IsChange = false;
    //滑动时调用
    public void OnValueChange(Vector2 value) {
        if (Middle.position.y > TopEmpty.position.y) {
            if (!IsChange)
            {
                Middle.parent = TopEmpty;
                Middle.localPosition = Vector3.zero;
                IsChange = true;
            }
           
        }
        if (MiddleEmpty.position.y < TopEmpty.position.y)
        {
            if (IsChange)
            {
                Middle.parent = MiddleEmpty;
                Middle.localPosition = Vector3.zero;
                IsChange = false;
            }
        }
    }
    private GameObject ChooseDownMenu;
    private GameObject ChooseShow;
    //点击下部的菜单按钮
    public void ClickBottomMenu(GameObject button) {
        //点击选择的图片返回
        if (ChooseDownMenu.name.Equals(button.name))
            return;
        //点击推荐
        if (button.name.Equals("tuijian")) {
            ChooseDownMenu = GameTools.ClickChangeButton(ChooseDownMenu, button);
            ChooseShow = GameTools.ClickChangeShow(ChooseShow, TuiJianShow);
            return;
        }
        //点击逛街
        if (button.name.Equals("guangjie"))
        {
            ChooseDownMenu = GameTools.ClickChangeButton(ChooseDownMenu, button);
            ChooseShow = GameTools.ClickChangeShow(ChooseShow, GuangJieShow);
            return;
        }
        //点击历史
        if (button.name.Equals("history"))
        {
            ChooseDownMenu = GameTools.ClickChangeButton(ChooseDownMenu, button);
            ChooseShow = GameTools.ClickChangeShow(ChooseShow, HistoryShow);
            return;
        }
        //点击我
        if (button.name.Equals("own"))
        {
            ChooseDownMenu = GameTools.ClickChangeButton(ChooseDownMenu, button);
            ChooseShow = GameTools.ClickChangeShow(ChooseShow, OwnShow);
            return;
        }

    }
    //点击店铺进入店铺
    public void ClickStore(GameObject button) {
        GameTools.OpenUI(StoreShow);
        //UIMove.Instance.MoveUI(StoreShow.transform);
    }
    //点击店铺进入店铺
    public void ClickGood(GameObject button)
    {
        GameTools.OpenUI(GoodShow);
        //UIMove.Instance.MoveUI(StoreShow.transform);
    }
    //点击上部分类
    public void ClickTopMenu(GameObject button) {
        StartCoroutine(AddChild(GuangJieGrid));
    }
    //添加子物体
    private IEnumerator AddChild(GameObject grid) {
        int index = grid.transform.childCount;
        for (int i = 0; i < index; i++) {
            grid.transform.GetChild(i).gameObject.SetActive(false);
        }
        //GridLayoutGroup uigrid = grid.GetComponent<GridLayoutGroup>();
        //float 
        for (int i = 0; i < index; i++)
        {
            yield return new WaitForSeconds(0.1f);
            grid.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    //点击地图按钮
    public void ClickMap(GameObject button) {
        //切换到地图
        GameInit.Instance.OpenMap();
    }
    private GameObject ChooseGrid;
    //点击距离选择
    public void ClickDistance(GameObject distance) {
        GameObject grid = distance.transform.FindChild("grid").gameObject;
        if (grid.activeSelf)
        {
            grid.GetComponent<GridLayoutGroup>().spacing = new Vector2(0, -48);
            grid.SetActive(false);
            distance.transform.GetChild(0).gameObject.SetActive(true);
            distance.transform.GetChild(1).gameObject.SetActive(false);
            return;
        }
        else {
            distance.transform.GetChild(0).gameObject.SetActive(false);
            distance.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (ChooseGrid)
        {
            if (ChooseGrid != grid){
            ChooseGrid.SetActive(false);
            ChooseGrid.transform.parent.GetChild(0).gameObject.SetActive(true);
            ChooseGrid.transform.parent.GetChild(1).gameObject.SetActive(false);
            }
        }
        grid.SetActive(true);
        ChooseGrid = grid;
    }
    //点击我的订单
    private void ClickOrder(GameObject button) {
        GameTools.OpenUI(OrderShow);
    }
    //点击我的消息
    private void ClickOpenNews(GameObject button) {
        GameTools.OpenUI(NewsShow);
    }
    //点击我的收藏
    private void ClickCollect(GameObject button) {
        GameTools.OpenUI(CollectShow);
    }
    //点击账号安全
    private void ClickUserSafe(GameObject button) {
        GameTools.OpenUI(UserSafeShow);
    }
    //点击资料修改
    private void ClickChangeDate(GameObject button) {
        GameTools.OpenUI(ChangeShow);
    }
    //点击设置按钮
    private void ClickSet(GameObject button) {
        GameTools.OpenUI(SetShow);
    }
    //点击设置帮助
    private void ClickSetHelp(GameObject button) {
       GameTools.OpenUI(HelpShow);
    }
    //点击关于
    private void ClickSetGuanYu(GameObject button) {
        GameTools.OpenUI(GuanYuShow);
    }
    //点击地图帮助
    private void ClickHelpMap(GameObject button) {
        GameTools.OpenUI(MapMiscShow);
    }
    //打开头像设置
    private void OpenAvater(GameObject button) {
        GameTools.IsOpenSet = true;
        ChangeUserShow.SetActive(true);
        ChangeUserShow.transform.FindChild("name").gameObject.SetActive(false);
        ChangeUserShow.transform.FindChild("avatar").gameObject.SetActive(true);
        ChangeUserShow.transform.FindChild("sex").gameObject.SetActive(false);
    }
    //打开呢称设置
    private void OpenName(GameObject button)
    {
        GameTools.IsOpenSet = true;
        ChangeUserShow.SetActive(true);
        ChangeUserShow.transform.FindChild("name").gameObject.SetActive(true);
        ChangeUserShow.transform.FindChild("avatar").gameObject.SetActive(false);
        ChangeUserShow.transform.FindChild("sex").gameObject.SetActive(false);
    }
    //打开性别设置
    private void OpenSex(GameObject button)
    {
        GameTools.IsOpenSet = true;
        ChangeUserShow.SetActive(true);
        ChangeUserShow.transform.FindChild("name").gameObject.SetActive(false);
        ChangeUserShow.transform.FindChild("avatar").gameObject.SetActive(false);
        ChangeUserShow.transform.FindChild("sex").gameObject.SetActive(true);
    }
    private void CloseChangeUser(GameObject button) {
        GameTools.IsOpenSet = false;
        ChangeUserShow.SetActive(false);
    }
    private void OpenErWeiMa(GameObject button) {
        GameTools.OpenUI(ErWeiMaShow);
    }
    private void OpenAddress(GameObject button)
    {
        GameTools.OpenUI(AddressShow);
    }
    //点击聊天信息
    private void ClickNews(GameObject button)
    {
        GameTools.OpenUI(CallSellShow);
    }
    //点击input 
    private void ClickInput(GameObject button) {
        button.transform.parent.gameObject.SetActive(false);
        button.transform.parent.parent.FindChild("input").gameObject.SetActive(true);
    }
    //点击talk 
    private void ClickTalk(GameObject button)
    {
        button.transform.parent.gameObject.SetActive(false);
        button.transform.parent.parent.FindChild("talk").gameObject.SetActive(true);
    }
    //点击JiaHao 
    private void ClickJiaHao(GameObject button)
    {
        if (CallSellPos.childCount == 0)
            CallSellMenu0.parent = CallSellPos;
        else
            CallSellMenu0.parent = CallSellPos.parent;
        CallSellMenu0.localPosition = Vector3.zero;

    }
}
