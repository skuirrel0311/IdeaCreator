using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MemoEditDialog : MemoViewDialog
{
    Memo memo;

    protected override void Start()
    {
        base.Start();
        title.onValueChanged.AddListener((text) =>
        {
            bool isInteractable = !string.IsNullOrEmpty(text) && text != memo.Title;
            saveButton.Interactable = isInteractable;
        });
    }

    public void Init(Memo memo)
    {
        this.memo = memo;

        title.text = memo.Title;
        body.text = memo.Body;
        gameTitle.Text = memo.OriginalGameName;
        changePoint.Text = memo.ChangePoint;
    }

    protected override void OnSave()
    {
        memo.Apply(title.text, body.text);
        base.OnSave();
    }
}
