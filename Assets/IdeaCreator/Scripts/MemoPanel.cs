using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class MemoPanel : Panel
{
    [SerializeField]
    MemoEditDialog memoEditDialog = null;
    [SerializeField]
    MemoViewElementPool memoViewElementPool = null;
    [SerializeField]
    RectTransform elementContainer = null;

    [SerializeField]
    UGUIButton backButton = null;

    MemoViewElement[] elements;

    void Start()
    {
        backButton.OnClick.AddListener(() =>
        {
            Deactivate();
            UIManager.Instance.TitlePanel.Activate();
        });
    }

    public override void Activate()
    {
        base.Activate();

        List<Memo> memoList = UserDataManager.Instance.MemoWriter.MemoList;
        float ElementSize = memoViewElementPool.GetOriginal.RectTransform.sizeDelta.y + 20.0f;
        Vector2 containerSize = elementContainer.sizeDelta;
        containerSize.y = ElementSize * memoList.Count;
        elementContainer.sizeDelta = containerSize;

        elements = new MemoViewElement[memoList.Count];
        for (int i = 0; i < memoList.Count; i++)
        {
            MemoViewElement element;
            element = memoViewElementPool.GetInstance();
            element.Init(memoList[i]);
            elements[i] = element;
            element.OnClick.RemoveAllListeners();
            element.OnClick.AddListener(() => ShowMemo(element.Memo));
        }
    }

    public override void Deactivate()
    {
        memoViewElementPool.ReturnAllInstance();
        base.Deactivate();
    }

    void ShowMemo(Memo memo)
    {
        memoEditDialog.Init(memo);
        memoEditDialog.Show();
    }

    public void Refresh()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].Refresh();
        }
    }
}
