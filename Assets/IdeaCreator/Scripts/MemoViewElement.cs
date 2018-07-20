using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using KanekoUtilities;

public class MemoViewElement : UGUIButton
{
    [SerializeField]
    UGUITextUnity title = null;
    [SerializeField]
    UGUITextUnity date = null;
    [SerializeField]
    float thresholdLongPush = 1.0f;
    
    public Memo Memo { get; private set; }

    EventTrigger eventTrigger;

    public UnityEvent OnLongPush = new UnityEvent();
    bool isPushing = false;
    float pushingTime = 0.0f;


    void Awake()
    {
        eventTrigger = GetComponent<EventTrigger>();
        eventTrigger.triggers = new List<EventTrigger.Entry>();

        AddEvent((data) => OnPointerDown(), EventTriggerType.PointerDown);
        AddEvent((data) => OnPointerUp(), EventTriggerType.PointerUp);
    }
    void AddEvent(UnityAction<BaseEventData> action, EventTriggerType type)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(action);

        eventTrigger.triggers.Add(entry);
    }
    
    public void Init(Memo memo)
    {
        Memo = memo;
        title.Text = memo.Title;
        
        string month;
        month = memo.Date[0].ToString() + memo.Date[1].ToString();

        string date = memo.Date[2].ToString() + memo.Date[3].ToString();

        this.date.Text = month + "/" +  date;
    }

    void OnDisable()
    {
        OnPointerUp();
    }

    public void Refresh()
    {
        Init(Memo);
    }

    void Update()
    {
        if(isPushing)
        {
            OnPushingButton();
        }
    }

    void OnPointerDown()
    {
        isPushing = true;
    }

    void OnPushingButton()
    {
        pushingTime += Time.deltaTime;

        if(pushingTime > thresholdLongPush)
        {
            OnLongPush.Invoke();
            OnPointerUp();
        }
    }

    void OnPointerUp()
    {
        pushingTime = 0.0f;
        isPushing = false;
    }
}
