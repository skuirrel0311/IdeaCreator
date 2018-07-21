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
    
    void Start()
    {
        gotoIdeaCreatorButton.OnClick.AddListener(() =>
        {
            Deactivate();
            UIManager.Instance.IdeaCreatorPanel.Activate();
            UserDataManager.Instance.UserName.SetValue(userNameInputField.text);
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
        
        userNameInputField.text = UserDataManager.Instance.UserName.GetValue();
        gotoIdeaCreatorButton.Interactable = !string.IsNullOrEmpty(userNameInputField.text);
    }
}
