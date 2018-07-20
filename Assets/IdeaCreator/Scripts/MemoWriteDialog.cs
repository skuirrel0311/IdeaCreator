using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KanekoUtilities;

public class MemoWriteDialog : OkCancelDialog
{
    [SerializeField]
    InputField titleInputField = null;
    [SerializeField]
    InputField memoInputField = null;

    public InputField TitleInputField { get { return titleInputField; } }
    public InputField MemoInputField { get { return memoInputField; } }

    protected override void Start()
    {
        base.Start();

        titleInputField.onValueChanged.AddListener((title) =>
        {
            OkButton.Interactable = !string.IsNullOrEmpty(title);
        });

        OkButton.Interactable = false;
    }

}
