using UnityEngine;
using System.Collections;

public class GameMono : BaseMonoBehaviour {
    public static GameMono Instance;
    void Awake()
    {
        Instance = this;
    }
    public GameObject CreateByReourse(string path)
    {
        GameObject loginUI = Resources.Load(path) as GameObject;
        return Instantiate(loginUI);
    }
}
