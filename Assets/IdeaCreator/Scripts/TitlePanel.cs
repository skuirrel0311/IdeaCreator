using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KanekoUtilities;

public class TitlePanel : Panel
{
    [SerializeField]
    UGUIButton gotoIdeaCreatorButton = null;
    [SerializeField]
    UGUIButton gotoMemoButton = null;

    [SerializeField]
    InputField userNameInputField = null;

    RegisterStringParameter userName;
    
    void Start()
    {
        gotoIdeaCreatorButton.OnClick.AddListener(() =>
        {
            Deactivate();
            UIManager.Instance.IdeaCreatorPanel.Activate();
            userName.SetValue(userNameInputField.text);
        });

        gotoMemoButton.OnClick.AddListener(() =>
        {
            Deactivate();
            UIManager.Instance.MemoPanel.Activate();
        });

        userNameInputField.onValueChanged.AddListener((name) =>
        {
            gotoIdeaCreatorButton.Interactable = !string.IsNullOrEmpty(name);
            
        });

        userName = new RegisterStringParameter("UserName", "");
        userNameInputField.text = userName.GetValue();
        gotoIdeaCreatorButton.Interactable = !string.IsNullOrEmpty(userName.GetValue());
    }
}
