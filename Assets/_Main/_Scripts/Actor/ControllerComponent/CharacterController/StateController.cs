using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DS_RE
{

    public class StateController : MonoBehaviour
    {
        public CharacterController ac;
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

            isDie = ac.animator.CheckState("die") || ac.animator.CheckState("dieState");
            isGround = ac.animator.CheckState("ground");
            isRoll = ac.animator.CheckState("roll");
            isJab = ac.animator.CheckState("jab");
            isAttack = ac.animator.CheckStateTag("attackR") || ac.animator.CheckStateTag("attackL");
            isBolcked = ac.animator.CheckState("blocked");
            isJump = ac.animator.CheckState("jump");
            isImpact = ac.animator.CheckState("impact");
            isFall = ac.animator.CheckState("fall");

            counterBackSuccess = isCounterBackEnable;
            counterBackFailer = isCounterBack && !isCounterBackEnable;

            allowDefense = isGround || isBolcked;
            isDefense = allowDefense && ac.animator.CheckState("defense1h", "Defense Layer");
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
}
