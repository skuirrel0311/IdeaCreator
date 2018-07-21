using System;

[Serializable]
public class MemoData
{
    public string id;
    public string title;
    public string body;
    public string original_game_name;
    public string change_point;
    public string date;
    public string author;
}

public class MemoDataFile
{
    public MemoData[] dataArray;
}

public class Memo
{
    const string TimeFormat = "yyyyMMddHHmmss";
    const string DateFormat = "MMdd";

    public string ID { get; private set; }
    public string Title { get; private set; }
    public string Body { get; private set; }
    public string OriginalGameName { get; private set; }
    public string ChangePoint { get; private set; }
    public string Date { get; private set; }
    //作者
    public string Author { get; private set; }

    // 何に対してのメモなのか
    public Memo(string originalGameName, string changePoint)
    {
        OriginalGameName = originalGameName;
        ChangePoint = changePoint;
        Author = UserDataManager.Instance.UserName.GetValue();

        ID = DateTime.Now.ToString(TimeFormat);
        Date = DateTime.Now.ToString(DateFormat);
    }

    public Memo(MemoData data)
    {
        ID = data.id;
        Title = data.title;
        Body = data.body;
        OriginalGameName = data.original_game_name;
        ChangePoint = data.change_point;
        Date = data.date;
        Author = data.author;
    }

    public void Apply(string title, string body)
    {
        Title = title;
        Body = body;
        UserDataManager.Instance.ApplyMemo(this);
    }
}