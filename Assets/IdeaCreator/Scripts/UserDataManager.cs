using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class UserDataManager : Singleton<UserDataManager>
{
    public MemoWriter MemoWriter { get; private set; }

    public UserDataManager()
    {
        MemoWriter = new MemoWriter();
    }
}
