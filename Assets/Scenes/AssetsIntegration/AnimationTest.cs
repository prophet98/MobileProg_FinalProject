using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    private Animator[] playerAC;

    private const string DEBUG_ATTACK = "DebugAttack";
    private const string DEBUG_RUN = "DebugRun";
    private const string DEBUG_DASH = "DebugDash";

    private void Awake()
    {
        playerAC = GetComponentsInChildren<Animator>();
    }

    public void DebugRun()
    {
        for (int i = 0; i < playerAC.Length; i++)
        {
            bool run = playerAC[i].GetBool(DEBUG_RUN);
            playerAC[i].SetBool(DEBUG_RUN, !run);
        }
    }

    public void DebugDash()
    {
        for (int i = 0; i < playerAC.Length; i++)
        {
            bool dash = playerAC[i].GetBool(DEBUG_DASH);
            playerAC[i].SetBool(DEBUG_DASH, !dash);
        }
    }

    public void DebugAttack()
    {
        for (int i = 0; i < playerAC.Length; i++)
        {
            bool attack = playerAC[i].GetBool(DEBUG_ATTACK);
            playerAC[i].SetBool(DEBUG_ATTACK, !attack);
        }
    }
}
