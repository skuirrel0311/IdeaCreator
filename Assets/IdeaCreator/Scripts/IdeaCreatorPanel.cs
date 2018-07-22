using System;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class IdeaCreatorPanel : Panel
{
    [SerializeField]
    UGUIButton nextButton = null;
    [SerializeField]
    UGUIButton memoButton = null;
    [SerializeField]
    UGUIButton showVideoButton = null;
    [SerializeField]
    UGUIButton backButton = null;
    [SerializeField]
    UGUIButton showGameDataButton = null;

    [SerializeField]
    UGUITextUnity gameTitle = null;
    [SerializeField]
    UGUITextUnity changePoint = null;

    [SerializeField]
    MemoCreateDialog memoCreateDialog = null;
    [SerializeField]
    GameDataDialog gameDataDialog = null;

    IdeaCreator ideaCreator;
    Idea idea;


    void Start()
    {
        ideaCreator = new IdeaCreator();

        nextButton.OnClick.AddListener(SetIdea);

        memoButton.OnClick.AddListener(() =>
        {
            memoCreateDialog.Init(gameTitle.Text, changePoint.Text);
            memoCreateDialog.Show();
        });

        showVideoButton.OnClick.AddListener(OpenYouTube);

        backButton.OnClick.AddListener(() =>
        {
            Deactivate();
            UIManager.Instance.TitlePanel.Activate();
        });

        showGameDataButton.OnClick.AddListener(() =>
        {
            gameDataDialog.Init(idea.GameData);
            gameDataDialog.Show();
        });
    }

    public override void Activate()
    {
        base.Activate();
        SetIdea();
    }

    void SetIdea()
    {
        idea = ideaCreator.Create();

        gameTitle.Text = idea.GameData.Title;
        changePoint.Text = idea.changePointText;
    }

    void OpenYouTube()
    {
        string[] strs = gameTitle.Text.Split(' ');
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
    }
}
