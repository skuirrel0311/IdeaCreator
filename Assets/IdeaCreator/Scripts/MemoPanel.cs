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

    Dictionary<string, MemoViewElement> elements = new Dictionary<string, MemoViewElement>();

    bool isLongPushed;

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
        
        for (int i = 0; i < memoList.Count; i++)
        {
            MemoViewElement element;
            element = memoViewElementPool.GetInstance();
            element.Init(memoList[i]);
            elements.Add(memoList[i].ID, element);
            element.OnClick.RemoveAllListeners();
            element.OnClick.AddListener(() => ShowMemo(element.Memo));
            element.OnLongPush.AddListener(() => ShowMemoDeleteDialog(element.Memo));
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

    void ShowMemoDeleteDialog(Memo memo)
    {
        OkCancelDialog dialog = DialogDisplayer.Instance.ShowDialog<OkCancelDialog>("MemoDeleteDialog");

        dialog.Init(() =>
        {
            MemoViewElement element;
            if (!elements.TryGetValue(memo.ID, out element)) return;

            UserDataManager.Instance.MemoWriter.RemoveMemo(memo);
            memoViewElementPool.ReturnInstance(element); 
        },()=> { });
    }

    public void Refresh()
    {
        foreach(string key in elements.Keys)
        {
            elements[key].Refresh();
        }
    }
}
