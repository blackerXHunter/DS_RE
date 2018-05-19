using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UserInput : MonoBehaviour {

    [Header("===== output signals =====")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dforward;

    public float Jup;
    public float Jright;

    // 1. pressing signal
    public bool run;
    public bool defense;
    // 2. trigger once signal
    public bool jump;
    protected bool lastJump;

    public bool attack;
    protected bool lastAttack;

    [Header("===== others =====")]
    public bool inputEnable = true;

    protected float DupTarget;
    protected float DrightTarget;
    protected float DupVelocity;
    protected float DrightVelocity;

    public static UserInput GetEnabledUserInput(GameObject gameObject)
    {
        var inputs = gameObject.GetComponents<UserInput>();
        foreach (var input in inputs)
        {
            if (input.enabled)
            {
                return input;
            }
        }
        return null;
    }
}
