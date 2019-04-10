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
        var ac = am.ac as PlayerAC;
        isDie = ac.CheckState("die");
        isGround = ac.CheckState("ground");
        isRoll = ac.CheckState("roll");
        isJab = ac.CheckState("jab");
        isAttack = ac.CheckStateTag("attackR") || ac.CheckStateTag("attackL");
        isBolcked = ac.CheckState("blocked");
        isJump = ac.CheckState("jump");
        isImpact = ac.CheckState("impact");
        isFall = ac.CheckState("fall");

        counterBackSuccess = isCounterBackEnable;
        counterBackFailer = isCounterBack && !isCounterBackEnable;

        allowDefense = isGround || isBolcked;
        isDefense = allowDefense && ac.CheckState("defense1h", "Defense Layer");
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

    [Header("2nd order stae flags")]
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
