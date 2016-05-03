using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//用户数据
public class UserData {
    //单例
    private static UserData Instance;
    private UserData() {
        
    }
    public UserData getInstance() {
        if (Instance == null) {
            Instance = new UserData();
        }
        return Instance;
    }
    public string UserName;
    public string UserPassWord;
    public string UserPhoneNumber;
    //用户商店历史记录
    public Dictionary<int, string> HistoryStore;
    //用户购物车记录
    public Dictionary<int, GoodData> WantBuyGoodData;
    //用户订单记录
    public Dictionary<int, GoodData> GoodOrder; 
}
