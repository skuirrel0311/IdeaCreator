using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class MemoCreateDialog : MemoViewDialog
{
    protected override void Start()
    {
        base.Start();
        title.onValueChanged.AddListener((text) =>
        {
            saveButton.Interactable = !string.IsNullOrEmpty(text);
        });
    }

    public void Init(string originalGameTitle, string changePoint)
    {
        gameTitle.Text = originalGameTitle;
        this.changePoint.Text = changePoint;
    }

    protected override void OnSave()
    {
        Memo memo = new Memo(gameTitle.Text, changePoint.Text);
        memo.Apply(title.text, body.text);
        UserDataManager.Instance.MemoWriter.AddMemo(memo);
        UserDataManager.Instance.MemoWriter.SaveMemo();

        title.text = "";
        body.text = "";
        base.OnSave();
    }
}
