using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class UIManager : SingletonMonobehaviour<UIManager>
{
    [SerializeField]
    TitlePanel titlePanel = null;
    public TitlePanel TitlePanel { get { return titlePanel; } }
    [SerializeField]
    IdeaCreatorPanel ideaCreatorPanel = null;
    public IdeaCreatorPanel IdeaCreatorPanel { get { return ideaCreatorPanel; } }
    [SerializeField]
    MemoPanel memoPanel = null;
    public MemoPanel MemoPanel { get { return memoPanel; } }
}
