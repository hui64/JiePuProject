using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIStore : MonoBehaviour {
    private GameObject StoreBackButton;
    private GameObject GoodBackButton;
    private GameObject GoodShowBackButton;
    private Transform StoreBottom;
    private Transform GoodBottom;
    private Transform GoodShowBottom;
    private GameObject StoreButton;
    private GameObject MenuGrid;
    private GameObject MainShow;
    private GameObject StoreShow;
    private GameObject GoodShowShow;
    private GameObject GoodShow;
    private GameObject GoodsGrid;
    private GameObject Other;
    private GameObject Share;
    private List<RectTransform> ScrollMasks = new List<RectTransform>();
    private void Init() {
        StoreBackButton = transform.FindChild("store/top/back").gameObject;
        GoodBackButton = transform.FindChild("good/top/back").gameObject;
        GoodShowBackButton = transform.FindChild("goodshow/top/back").gameObject;
        StoreButton = transform.FindChild("good/top/store").gameObject;
        MenuGrid = transform.FindChild("goodshow/storeshow/show/menus").gameObject;
        MainShow = transform.parent.FindChild("main").gameObject;
        StoreShow = transform.FindChild("store").gameObject;
        GoodShowShow = transform.FindChild("goodshow").gameObject;
        GoodsGrid = transform.FindChild("store/store/goodshow/show/goods/grid").gameObject;
        GoodShow = transform.FindChild("good").gameObject;
        StoreBottom = StoreShow.transform.FindChild("bottom");
        GameTools.AddChangeTransform(StoreBottom);
        GoodBottom = GoodShow.transform.FindChild("bottom");
        GameTools.AddChangeTransform(GoodBottom);
        GoodShowBottom = GoodShowShow.transform.FindChild("bottom");
        GameTools.AddChangeTransform(GoodShowBottom);
        Other = transform.FindChild("good/top/other").gameObject;
        Share = transform.FindChild("good/share").gameObject;
    }
    private void AddScrollMasks() {
        ScrollMasks.Add(transform.FindChild("store/store/goodshow").GetComponent<RectTransform>());
        ScrollMasks.Add(transform.FindChild("good/goodshow").GetComponent<RectTransform>());
        ScrollMasks.Add(transform.FindChild("goodshow/storeshow").GetComponent<RectTransform>());
    }
    private void AddListen() {
        GameTools.AddClickListen(StoreBackButton, GameTools.ClickBack);
        GameTools.AddClickListen(GoodBackButton, GameTools.ClickBack);
        GameTools.AddClickListen(GoodShowBackButton, GameTools.ClickBack);
        GameTools.AddChildListen(GoodsGrid, ClickGood);
        GameTools.AddClickListen(StoreButton, ClickStore);
        GameTools.AddClickListen(Other, ClickOther);
    }
    void Awake() {
        Init();
    }
	void Start () {
        AddScrollMasks();
        GameTools.SetPosAndMaskByScreen(ScrollMasks); //自适应
        AddListen();
        //StartCoroutine(UIMove.Instance.MoveToward(transform, Vector3.zero, 50f));
	}
    //点击店铺按钮
    public void ClickStore(GameObject button) {
        GameTools.OpenUI(StoreShow);
    }
    //点击商品分类
    public void ClickMenu(GameObject button) {
        GameTools.OpenUI(GoodShowShow);
    }
    //点击商品
    public void ClickGood(GameObject good) {
        GameTools.OpenUI(GoodShow);
    }  
    //点击其他按钮（分享）
    private void ClickOther(GameObject button) {
        Share.SetActive(!Share.activeSelf);
    }
}
