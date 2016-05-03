using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMain : MonoBehaviour {

    void Awake()
    {
        PlayMovie();
        Debug.Log("初始化-----");
    }
    //播放开场动画
    private void PlayMovie() {
        Handheld.PlayFullScreenMovie("movie.mp4", Color.clear, FullScreenMovieControlMode.Hidden);
    }
    void Start()
    {
        Init();
    }
    private void Init()
    {
        LoadLoginUI();
    }
    //加载登录界面
    private void LoadLoginUI()
    {
       
    }
   
}
