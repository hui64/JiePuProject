using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AnotherScrollRect : ScrollRect
{
    public Transform GridRT;
    public float ItemX;
    public Transform[] Childs;
    private Vector3 OldFirstPos;
    private Vector3 OldLastPos;
    public ScrollRect Info;
    private GameObject ChooseDian;
    private GameObject[] Dians;
    private bool IsDrag = false;
    private bool IsChangePage = false; 
    void Start() {
        Info = transform.parent.parent.GetComponent<ScrollRect>();
        GridRT = transform.FindChild("grid");
        Childs = new Transform[2];
        Childs[0] = GridRT.transform.GetChild(0);
        Childs[1] = GridRT.transform.GetChild(1);
        OldFirstPos = Childs[0].position;
        OldLastPos = Childs[1].position;
        SetDians();
        ChangePage();
        IsChangePage = true;
    }
    void OnEnable() {
        if(IsChangePage)
            ChangePage();
    }
    private void SetDians() {
        Transform info = transform.FindChild("dian");
        int index = info.childCount;
        Dians = new GameObject[index];
        for (int i = 0; i < index; i++) {
            Dians[i] = info.transform.GetChild(i).GetChild(0).gameObject;
        }
        ChooseDian = Dians[0];
    }
    public override void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (Info)
            Info.OnBeginDrag(eventData);
        base.OnBeginDrag(eventData);
    }

    public override void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (eventData.delta.x == 0)
            if(Info)
                Info.OnDrag(eventData);
        base.OnDrag(eventData);
        if (IsDrag)
            return;
        if (eventData.delta.x > 0)
        {
            StartCoroutine(MoveLeft());
            IsDrag = true;
        }
       
    }
    public override void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (Info)
            Info.OnEndDrag(eventData);
        base.OnEndDrag(eventData);
        //StartCoroutine(MoveLeft());
    }
    //翻页效果
    public void ChangePage() {
        StartCoroutine("MoveToTarget");
    }
    private Transform ChangeChild;
    private int ChangeX = 0;
    private int Speed = 6;
    private int DianNum = 0;
    private int Index = 0;
	//翻页
    private IEnumerator MoveToTarget() {
        ChangeX = 0;
        while (true) {
            ChangeX ++;
            if (ChangeX > 100)
                GridRT.localPosition -= new Vector3(Speed, 0, 0);
            if (Childs[GetAnother(Index)].position.x <= OldFirstPos.x)
            {
                DianNum++;
                if (DianNum == 5)
                    DianNum = 0;
                SetDian();
                Childs[Index].localPosition += new Vector3(1440, 0, 0);
                Index++;
                if (Index > 1)
                    Index = 0;
                ChangeX = 0;
            }else
                if (Childs[GetAnother(Index)].position.x > OldLastPos.x + 1)
            {
                DianNum--;
                if (DianNum == -1)
                    DianNum = 4;
                SetDian();
                Childs[GetAnother(Index)].localPosition -= new Vector3(1440, 0, 0);
                Index--;
                if (Index < 0)
                    Index = 1;
                ChangeX = 0;
            }
            yield return null;
        }
    }
    private void SetDian() {
        ChooseDian.SetActive(false);
        Dians[DianNum].SetActive(true);
        ChooseDian = Dians[DianNum];
    }
    private int GetAnother(int x) {
        if (x == 0)
            return 1;
        return 0;
    }
    private IEnumerator MoveLeft()
    {
        int changX = 0;
        while (true)
        {
            ChangeX = 0;
            changX++;
            if (changX > 100)
            {
                GridRT.localPosition += new Vector3(Speed, 0, 0);
                if (Childs[GetAnother(Index)].position.x >= OldLastPos.x)
                {
                    DianNum--;
                    if (DianNum == -1)
                        DianNum = 4;
                    SetDian();
                    Childs[GetAnother(Index)].localPosition -= new Vector3(1440, 0, 0);
                    Index--;
                    if (Index < 0)
                        Index = 1;
                    IsDrag = false;
                    changX = 0;
                    yield break;
                }
            }
            yield return null;
        }
    }

}
