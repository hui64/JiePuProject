using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoopGrid : MonoBehaviour {
    public Transform Top;
    public ScrollRect myScrollRect; 
    private GameObject Grid;
    private float DistanceY = 0;
    private float TopY = 0;
    private float BottomY = 0;
    private Transform[] ChildTrans;
    private int First;
    private int Last;
    private int ChildNum;
    private Vector2 FirstPos;
    private Vector2 LastPos;
    private Vector3 ChangePos;
    public float Modify = 0;
    public int Index = 0;   //拖拽到第几个了  默认的等于子物体个数
    void Awake()
    {

    }
    void Start()
    {
        Init();
    }
    private void Init()
    {
        SetDistance(gameObject.GetComponent<RectTransform>());
        GetChildTransforms(gameObject);
    }
    //设置上边界跟下边界 间距
    private void SetDistance(RectTransform RTran)
    {
        if (RTran.pivot == new Vector2(0.5f, 0.5f))
        {
            TopY = 0.5f * DistanceY;
            BottomY = -TopY;
        }
        if (RTran.pivot == new Vector2(0.5f, 1f))
        {
            BottomY = -DistanceY;
        }
        if (Top)
            Modify = Top.position.y - transform.position.y;
    }
    //获取子物体的Transform
    private void GetChildTransforms(GameObject grid)
    {
        ChildNum = grid.transform.childCount;
        Index = ChildNum - 1;
        ChildTrans = new Transform[ChildNum];
        for (int i = 0; i < ChildNum; i++)
        {
            ChildTrans[i] = grid.transform.GetChild(i);
        }
        First = 0;
        Last = ChildNum - 1;
        LastPos = ChildTrans[ChildNum - 1].position + new Vector3(0, Modify, 0);
        DistanceY = ChildTrans[0].position.y - ChildTrans[1].position.y;
        FirstPos = ChildTrans[0].position + new Vector3(0, DistanceY + Modify, 0);
        DistanceY = DistanceY * ChildNum;
        ChangePos = new Vector3(0, DistanceY, 0);
    }
    private float ChangY = 0;
    private float OldY = 1f;
    private bool IsLastToFirst = false;
    //拖拽时调用
    public void OnValueChanged(Vector2 move)
    {
        if (ChildTrans[First].position.y >= FirstPos.y)
        {
            ChangeFirstToLast();
            IsLastToFirst = true;
        }
        if (!IsLastToFirst)
            return;
        if (ChildTrans[Last].position.y < LastPos.y)
        {
            ChangeLastToFirst();
        }
    }
    //第一个移动到最后一个
    public void ChangeFirstToLast()
    {
        myScrollRect.movementType = ScrollRect.MovementType.Unrestricted;
        Index++;
        ChildTrans[First].position -= ChangePos;
        Last = First;
        if (First == ChildNum - 1)
            First = 0;
        else
            First = First + 1;
    }
    //最后一个移动到第一个
    public void ChangeLastToFirst()
    {
        if (Index == ChildNum - 1)
        {
            myScrollRect.movementType = ScrollRect.MovementType.Elastic;
            return;
        }
        Index--;
        ChildTrans[Last].position += ChangePos;
        First = Last;
        if (Last == 0)
            Last = ChildNum - 1;
        else
            Last = Last - 1;
    }
}
