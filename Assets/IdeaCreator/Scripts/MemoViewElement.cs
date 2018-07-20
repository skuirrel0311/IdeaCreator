using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class MemoViewElement : UGUIButton
{
    [SerializeField]
    UGUITextUnity title = null;
    [SerializeField]
    UGUITextUnity date = null;
    
    public Memo Memo { get; private set; }
    
    public void Init(Memo memo)
    {
        Memo = memo;
        title.Text = memo.Title;
        
        string month;
        month = memo.Date[0].ToString() + memo.Date[1].ToString();

        string date = memo.Date[2].ToString() + memo.Date[3].ToString();

        this.date.Text = month + "/" +  date;
    }

    public void Refresh()
    {
        Init(Memo);
    }
}
