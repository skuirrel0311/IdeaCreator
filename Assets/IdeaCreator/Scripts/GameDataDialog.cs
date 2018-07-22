using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class GameDataDialog : Dialog
{
    [SerializeField]
    UGUITextUnity publisher = null;
    [SerializeField]
    UGUIImage icon = null;
    [SerializeField]
    UGUITextUnity title = null;
    [SerializeField]
    UGUITextUnity potential = null;
    [SerializeField]
    NumberText usRanking = null;
    [SerializeField]
    NumberText topCount = null;
    [SerializeField]
    UGUIButton youtubeButton = null;
    [SerializeField]
    UGUIButton closeButton = null;

    GameData gameData;

    protected override void Start()
    {
        base.Start();
        closeButton.OnClick.AddListener(() =>
        {
            Hide();
        });

        youtubeButton.OnClick.AddListener(() =>
        {
            string[] strs = gameData.Title.Split(' ');
            string url = "https://www.youtube.com/results?search_query=ketchapp+";
            for (int i = 0; i < strs.Length; i++)
            {
                url += strs[i];
                if (i < strs.Length - 1)
                {
                    url += "+";
                }
            }

            Application.OpenURL(url);
        });
    }

    public void Init(GameData gameData)
    {
        this.gameData = gameData;
        publisher.Text = gameData.Publisher;
        title.Text = gameData.Title;
        usRanking.SetValue(gameData.USRanking);
        topCount.SetValue(gameData.TopCount);

        //icon.Sprite = MyAssetStore.Instance.GetAsset<Sprite>(gameData.Title + "Icon", "Icons/");

        string potentialStar = "";
        for(int i = 0;i < 5;i++)
        {
            if(gameData.Potential >= i + 1) potentialStar += "★";
            else potentialStar += "☆";
        }
        potential.Text = potentialStar;
    }
}
