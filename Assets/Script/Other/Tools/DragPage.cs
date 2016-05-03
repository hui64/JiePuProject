using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class DragPage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int Speed = 6;
    public int PageSpeed = 10;
    public float Change = 0.2f;
    public int AdNum = 5;   //广告图数量
    private Vector3 OldPos;
    private Vector3 HeadPos;
    private Vector3 NextPos;
    private float DistanceX = 0;
    private Vector3 Distance;
    private RectTransform myRT;
    private Vector3 TargetPos;
    private float Diff;
    private Vector3 FirstPos;
    private Vector3 LastPos;
    private Vector3 ChangPos;
    private Transform[] ChildTrans;
    private int ChildNum;
    private int First = 0;
    private int Last = 1;
    private GameObject[] Dians;
    private int AdIndex = 0;
    public ScrollRect MySR;
    void Start() {
        Init();
        SetDians(transform.parent.FindChild("dians"));
    }
    void OnEnable()
    {
        StartCoroutine("LoopAd");
    }
    private void Init() {
        myRT = transform.GetComponent<RectTransform>();
        DistanceX = transform.GetChild(1).position.x - transform.GetChild(0).position.x;
        Distance = new Vector3(DistanceX, 0, 0);
        FirstPos = transform.position - Distance;
        LastPos = transform.position + Distance;
        ChildNum = transform.childCount;
        ChildTrans = new Transform[ChildNum];
        for (int i = 0; i < ChildNum; i++)
        {
            ChildTrans[i] = transform.GetChild(i);
        }
        SetTargetPos();
    }
    private void SetDians(Transform grid) {
        Dians = new GameObject[AdNum];
        GameObject info = grid.GetChild(0).gameObject;
        Dians[0] = info.transform.GetChild(0).gameObject;
        for (int i = 0; i < AdNum - 1; i++) {
            GameObject go = Instantiate(info);
            GameTools.AddChild(grid.gameObject, go);
            Dians[i + 1] = go.transform.GetChild(0).gameObject; 
        }
        Dians[0].SetActive(true);
        OldDian = Dians[0];
    }
    private bool IsDraging = false;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (MySR)
            MySR.OnBeginDrag(eventData);
        StopCoroutine("LoopAd");
        if (!IsDraging)
        {
            //SetTargetPos();
            IsDraging = true;
        }
    }
    private void SetTargetPos() {
        OldPos = transform.position;
        HeadPos = transform.position - Distance;
        NextPos = transform.position + Distance;
    }
    private bool IsDragUpDown = false;
    public void OnDrag(PointerEventData data)
    {
        //StartCoroutine("Moveing");
        if (MySR)
            if (data.delta.x == 0)
                MySR.OnDrag(data);
        if (data.delta.y == 0)
            SetDraggedPosition(data);
        SetPos(data.delta.x);
    }
    private void SetPos(float change) {
        if (change < 0)
            if (ChildTrans[First].position.x <= FirstPos.x + 1)
            {
                ChangeFirstToLast();
                //return;
            }
        if (change > 0)
            if (ChildTrans[Last].position.x >= LastPos.x - 1)
            {
                ChangeLastToFirst();
            }
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        transform.position += new Vector3(data.delta.x, 0, 0);
        //MoveSpeed = data.delta.x*0.5f;
    }
    private float MoveSpeed = 0;
    private IEnumerator Moveing() {
        while (true) {
            MoveSpeed -= 0.1f;
            transform.Translate(MoveSpeed, 0, 0);
            yield return null;
        }
    }
    private bool IsDragLeft = true;
    public void OnEndDrag(PointerEventData eventData)
    {
        //StopCoroutine("Moveing");
        if (MySR)
            MySR.OnEndDrag(eventData);
        if (IsDragUpDown)
        {
            return;
        }
        Diff = transform.position.x - OldPos.x;
        if (Diff <= -Change * DistanceX)
        {
            AdIndex++;
            if (AdIndex >= AdNum)
                AdIndex = 0;
            TargetPos = HeadPos;
        }
        else if (Diff >= Change * DistanceX)
        {
            AdIndex--;
            if (AdIndex < 0)
                AdIndex = AdNum - 1;
            TargetPos = NextPos;
        }
        else
            TargetPos = OldPos;
        StopCoroutine("MoveToTarget");
        StartCoroutine("MoveToTarget");
    }
    private IEnumerator MoveToTarget() {
        SetDianHigh(AdIndex);
        while (true) {
            transform.position = new Vector2(Vector3.MoveTowards(transform.position, TargetPos, Speed).x,transform.position.y);
            SetPos(Diff);
            if (Mathf.Abs(TargetPos.x - transform.position.x) < 0.1)
            {
                SetTargetPos();
                transform.position = new Vector2(TargetPos.x, transform.position.y);
                IsDraging = false;
                StopCoroutine("LoopAd");
                StartCoroutine("LoopAd");
                yield break;
            }
            yield return null;
        }
       
    }
    //第一个移动到最后一个
    public void ChangeFirstToLast()
    {
        ChildTrans[First].position += 2*Distance;
        Last = First;
        if (First == ChildNum - 1)
            First = 0;
        else
            First = First + 1;
    }
    //最后一个移动到第一个
    public void ChangeLastToFirst()
    {
        ChildTrans[Last].position -= 2*Distance;
        First = Last;
        if (Last == 0)
            Last = ChildNum - 1;
        else
            Last = Last - 1;
    }
    private GameObject OldDian;
    //设置滑动到第i点高亮
    private void SetDianHigh(int i)
    {
        OldDian.SetActive(false);
        Dians[i].SetActive(true);
        OldDian = Dians[i];
    }
    //private int ChangeX = 0;
    //广告轮播
    private IEnumerator LoopAd()
    {
        int changeX = 0;
        while (true)
        {
            changeX++;
            if (changeX > 100)
            {
                transform.Translate(-10, 0, 0);
                if (ChildTrans[First].position.x <= FirstPos.x)
                {
                    ChangeFirstToLast();
                    AdIndex++;
                    if (AdIndex >= AdNum)
                        AdIndex = 0;
                    SetDianHigh(AdIndex);
                    changeX = 0;
                    SetTargetPos();
                }
            }
            yield return null;
        }
    }
    //移动到目标X方向
    private bool MoveToTargetX(Transform go, Vector3 target, int speed) {
        float move = target.x - go.transform.position.x;
        if (Mathf.Abs(move) < 0.4) {
            go.transform.position = new Vector2(target.x, go.transform.position.y);
            return true;
        }
        go.transform.position += new Vector3(move/speed, 0, 0);
        return false;
    }

}

