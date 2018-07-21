using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KanekoUtilities;

public class InputFieldEditButton : UGUIButton
{
    [SerializeField]
    InputField inputField = null;

    void Start()
    {
        inputField.onEndEdit.AddListener((text) =>
        {
            KKUtilities.Delay(1, ()=> inputField.interactable = false, this);
        });
        OnClick.AddListener(() =>
        {
            inputField.interactable = true;

            inputField.Select();
        });
    }
}
