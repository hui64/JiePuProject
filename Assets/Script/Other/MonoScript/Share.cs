using UnityEngine;
using System.Collections;
using cn.sharesdk.unity3d;

public class Share : MonoBehaviour {
    public ShareSDK share;

    // Use this for initialization
    void Awake()
    {

    }
    void Start()
    {
        //定义回调
        share.shareHandler = ShareResultHandler;
    }
    void ShareResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        print("==============");
        if (state == ResponseState.Success)
        {
            print("分享成功");
            print(MiniJSON.jsonEncode(result));
        }
        else if (state == ResponseState.Fail)
        {
            print("分享失败" + result["error_code"] + "; error msg = " + result["error_msg"]);
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel!");
        }

    }
    public void ClickButton()
    {
        print("分享");
        ShareContent content = new ShareContent();
        content.SetText("this is a test string.");
        content.SetImageUrl("https://f1.webshare.mob.com/code/demo/img/1.jpg");
        content.SetTitle("test title");
        content.SetTitleUrl("http://www.mob.com");
        content.SetSite("Mob-ShareSDK");
        content.SetSiteUrl("http://www.mob.com");
        content.SetUrl("http://www.mob.com");
        content.SetComment("test description");
        content.SetMusicUrl("http://mp3.mwap8.com/destdir/Music/2009/20090601/ZuiXuanMinZuFeng20090601119.mp3");
        content.SetShareType(ContentType.App);

        int reqID = share.ShareContent(PlatformType.QQ, content);
    }
}
