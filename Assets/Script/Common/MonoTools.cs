using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonoTools : MonoBehaviour {
    public static MonoTools Instance;
    void Awake() {
        Instance = this;
    }
    
}
