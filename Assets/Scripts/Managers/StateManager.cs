using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : IActorManager {

    private void Start() {
        HP = Mathf.Clamp(HP, 0, HPMAX);
    }

    private float HP = 15.0f;
    private float HPMAX = 25.0f;
    public void AddHP(float value) {
        HP += value;
        HP = Mathf.Clamp(HP, 0, HPMAX);
    }

    private void Update() {
        isDie = am.ac.CheckState("die");
        isGround = am.ac.CheckState("ground");
        isRoll = am.ac.CheckState("roll");
        isJab = am.ac.CheckState("jab");
        isAttack = am.ac.CheckStateTag("attackR") || am.ac.CheckStateTag("attackL");
        isBolcked = am.ac.CheckState("blocked");
        isJump = am.ac.CheckState("jump");
        isImpact = am.ac.CheckState("impact");
        isFall = am.ac.CheckState("fall");

        allowDefense = isGround || isBolcked;
        isDefense = allowDefense && am.ac.CheckState("defense1h", "Defense Layer");
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

    [Header("2nd order stae flags")]
    public bool allowDefense;

    public bool HPisZero
    {
        get
        {
            return HP == 0;
        }
    }
}
