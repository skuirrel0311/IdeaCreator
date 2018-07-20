using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KanekoUtilities;


/// <summary>
/// メモの内容を表示することができるダイアログ
/// </summary>
public class MemoViewDialog : Dialog
{
    [SerializeField]
    protected InputField title = null;
    [SerializeField]
    protected InputField body = null;
    [SerializeField]
    protected UGUITextUnity gameTitle = null;
    [SerializeField]
    protected UGUITextUnity changePoint = null;

    [SerializeField]
    protected UGUIButton saveButton = null;
    [SerializeField]
    protected UGUIButton cancelButton = null;
    
    protected override void Start()
    {
        base.Start();

        saveButton.OnClick.AddListener(OnSave);

        cancelButton.OnClick.AddListener(Hide);

        title.onValueChanged.AddListener((text) =>
        {
            saveButton.Interactable = !string.IsNullOrEmpty(text);
        });
        saveButton.Interactable = false;
    }
    
    protected virtual void OnSave()
    {
        Hide();
    }
}
