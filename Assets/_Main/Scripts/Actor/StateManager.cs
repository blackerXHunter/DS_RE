using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : IActorManager
{

    private void Start()
    {
        HP = Mathf.Clamp(HP, 0, HPMAX);
    }

    public float HP = 150.0f;
    public float HPMAX = 250.0f;
    public float ATK = 10f;
    public void AddHP(float value)
    {
        HP += value;
        HP = Mathf.Clamp(HP, 0, HPMAX);
    }

    private void Update()
    {
        var ac = am.ac as IActorController;
        isDie = ac.GetAnimator().CheckState("die");
        isGround = ac.GetAnimator().CheckState("ground");
        isRoll = ac.GetAnimator().CheckState("roll");
        isJab = ac.GetAnimator().CheckState("jab");
        isAttack = ac.GetAnimator().CheckStateTag("attackR") || ac.GetAnimator().CheckStateTag("attackL");
        isBolcked = ac.GetAnimator().CheckState("blocked");
        isJump = ac.GetAnimator().CheckState("jump");
        isImpact = ac.GetAnimator().CheckState("impact");
        isFall = ac.GetAnimator().CheckState("fall");

        counterBackSuccess = isCounterBackEnable;
        counterBackFailer = isCounterBack && !isCounterBackEnable;

        allowDefense = isGround || isBolcked;
        isDefense = allowDefense && ac.GetAnimator().CheckState("defense1h", "Defense Layer");
        immortal = isRoll || isJab;
    }

    [Header("1st order state flags")]
    public bool isDie;
    public bool isGround;
    public bool isRoll;
    public bool isJab;
    public bool isAttack;
    public bool isDefense;
    public bool isBolcked;
    public bool isJump;
    public bool isImpact;
    public bool isFall;
    public bool isCounterBack;
    public bool isCounterBackEnable;

    [Header("2nd order state flags")]
    public bool allowDefense;
    public bool immortal;
    public bool counterBackSuccess;
    public bool counterBackFailer;

    public bool HPisZero
    {
        get
        {
            return HP <= 0;
        }
    }
}
