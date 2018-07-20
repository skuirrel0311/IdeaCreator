using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

/// <summary>
/// 質問に対する答えを出力する
/// </summary>
public class MemoWriter
{
    List<Memo> memoList = new List<Memo>();
    public List<Memo> MemoList { get { return memoList; } }
    Dictionary<string, Memo> memoDictionary = new Dictionary<string, Memo>();
    RegisterStringParameter memoDataJson;
    
    public MemoWriter()
    {
        memoDataJson = new RegisterStringParameter("MemoData", string.Empty);
        LoadMemo();
    }
    
    public void AddMemo(Memo memo)
    {
        if (memoDictionary.ContainsKey(memo.ID)) return;

        memoDictionary.Add(memo.ID, memo);
        memoList.Add(memo);
    }

    public void RemoveMemo(Memo memo)
    {
        memoDictionary.Remove(memo.ID);
        memoList.Remove(memo);
        SaveMemo();
    }
    
    public Memo GetMemo(string id)
    {
        Memo memo;
        memoDictionary.TryGetValue(id, out memo);
        return memo;
    }
    
    //外部ファイルに書き込む
    public void SaveMemo()
    {
        MemoDataFile file = new MemoDataFile();
        file.dataArray = new MemoData[memoList.Count];
        for(int i = 0;i< memoList.Count;i++)
        {
            Memo memo = memoList[i];
            MemoData data = new MemoData();

            data.id = memo.ID;
            data.title = memo.Title;
            data.body = memo.Body;
            data.original_game_name = memo.OriginalGameName;
            data.change_point = memo.ChangePoint;
            data.date = memo.Date;

            file.dataArray[i] = data;
        }

        memoDataJson.SetValue(JsonUtility.ToJson(file));
    }

    void LoadMemo()
    {
        if (string.IsNullOrEmpty(memoDataJson.GetValue())) return;

        MemoDataFile file =  JsonUtility.FromJson<MemoDataFile>(memoDataJson.GetValue());

        for(int i = 0;i < file.dataArray.Length;i++)
        {
            AddMemo(new Memo(file.dataArray[i]));
        }
    }
}
