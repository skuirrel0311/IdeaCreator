using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class WebViewWindow : MonoBehaviour
{
    [SerializeField]
    UGUIButton button = null;

    [SerializeField]
    string url = "";

    WebViewObject webViewObject;

    void Start()
    {
        webViewObject = gameObject.AddComponent<WebViewObject>();

        webViewObject.Init(null);

        button.OnClick.AddListener(() =>
        {
            webViewObject.LoadURL(url);

            webViewObject.SetVisibility(true);

            KKUtilities.Delay(5.0f, () =>
            {
                webViewObject.SetVisibility(false);
            }, this);
        });
    }
}
