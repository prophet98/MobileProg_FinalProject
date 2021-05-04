using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvEventManager : MonoBehaviour
{
    public delegate void ChangeEnv();
    public static event ChangeEnv OnEnvChange;

    public static void Change()
    {
        if(OnEnvChange != null)
            OnEnvChange();
    } 
}
