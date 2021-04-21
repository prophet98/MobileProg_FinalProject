using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    private Animator playerAC;

    private const string DEBUG_ATTACK = "DebugAttack";
    private const string DEBUG_RUN = "DebugRun";
    private const string DEBUG_DASH = "DebugDash";

    private void Awake()
    {
        playerAC = GetComponent<Animator>();
    }

    public void DebugRun()
    {
        bool run = playerAC.GetBool(DEBUG_RUN);
        playerAC.SetBool(DEBUG_RUN, !run);
    }

    public void DebugDash()
    {
        bool dash = playerAC.GetBool(DEBUG_DASH);
        playerAC.SetBool(DEBUG_DASH, !dash);
    }

    public void DebugAttack()
    {
        bool attack = playerAC.GetBool(DEBUG_ATTACK);
        playerAC.SetBool(DEBUG_ATTACK, !attack);
    }
}
