using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DS_RE
{

    public class CharacterController : AnimatedObjectController
    {
        #region Feilds
        [Header("====== Field ======")]
        [SerializeField]
        public UserInput input;

        [SerializeField]
        protected Rigidbody rigid;

        [SerializeField]
        protected Collider coll;

        [SerializeField]
        protected float walkSpeed = 1.4f;

        [SerializeField]
        protected float runSpeed = 2.5f;

        [SerializeField]
        protected float jumpVelocity = 3.0f;

        [SerializeField]
        protected float rollVelocity = 2.0f;

        [SerializeField]
        protected float jabVelocity = 2.0f;

        [SerializeField]
        protected bool lockPlaner = false;

        public UnityEvent OnAction;


        protected Vector3 planerVec;
        protected Vector3 thrustVec;
        protected Vector3 deltaPos;
        [Header("===== Frication Settings =====")]
        public PhysicMaterial fricationOne;
        public PhysicMaterial fricationZero;
        [Header("===== Controller ======")]
        public WeaponController weaponController;
        public StateController stateController;
        public LockController lockController;
        [Header("===== Manager =====")]
        public DirectorManager dm;
        public EventCasterManager frontStabEcManager;
        #endregion

        #region Mono

        protected override void Awake()
        {
            base.Awake();
            animator = model.GetComponent<Animator>();
            input = UserInput.GetEnabledUserInput(this.gameObject);
            rigid = GetComponent<Rigidbody>();
            coll = GetComponent<Collider>();
        }

        protected override void Start()
        {
            dm = DirectorManager.Instance;
        }

        bool rollingStart = false;
        public bool CheckRolling()
        {
            if (input.roll && input.Dmag > 0.1f)
            {
                rollingStart = true;
                return true;
            }
            else
            if (animator.CheckState("roll"))
            {
                rollingStart = false;
                return true;
            }
            else if (rollingStart)
            {
                return true;
            }
            return false;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            if (input == null)
            {
                return;
            }



            if (input.roll)
            {
                animator.SetTrigger("Roll");
            }
            if (animator.CheckState("ground") || animator.CheckState("blocked"))
            {

                animator.SetBool("defense", input.defense);
                animator.SetLayerWeight(animator.GetLayerIndex("Defense Layer"), input.defense ? 1 : 0);
            }

            if (input.jump)
            {
                animator.SetTrigger("Jump");

            }


            if (input.rb)
            {

                if (animator.CheckState("ground") || animator.CheckStateTag("attackR"))
                {
                    animator.SetTrigger("attack");
                }
            }
            // left heavy(left trigger)
            if (input.lt)
            {
                if (animator.CheckState("ground") || animator.CheckStateTag("attackR"))
                {
                    animator.SetTrigger("counterBack");
                }
            }

            if (input.action)
            {
                OnAction.Invoke();
                DoAction();
            }

            if (input.lockUnlock)
            {
                lockController.LockUnLock();
            }

            animator.SetBool("camera lock", lockController.lockState);
            float targetRunMulti = (input.run ? 2.0f : 1.0f);
            if (lockController.lockState == false)
            {
                animator.SetFloat("Forward", Mathf.Lerp(animator.GetFloat("Forward"), input.Dmag * targetRunMulti, 0.2f));
                animator.SetFloat("right", 0);
            }
            else
            {
                animator.SetFloat("Forward", Mathf.Lerp(animator.GetFloat("Forward"), input.Dup * targetRunMulti, 0.2f));
                animator.SetFloat("right", Mathf.Lerp(animator.GetFloat("right"), input.Dright * targetRunMulti, 0.2f));
            }

            var targetDir = lockController.lockTarget != null ? (lockController.lockTarget.obj.transform.position - this.transform.position).normalized : Vector3.zero;
            if (lockController.lockState == false)
            {
                if (input.inputEnable)
                {
                    if (input.Dmag > 0.1f)
                    {
                        //model.transform.Rotate()
                        //model.transform.Rotate(model.transform.up, Vector3.Angle(model.transform.forward, input.Dforward));
                        //model.transform.Rotate(model.transform.position, model.transform.up, Vector3.Angle(model.transform.forward, input.Dforward));
                        //model.transform.forward = Vector3.Slerp(model.transform.forward, input.Dforward, 0.3f);
                        model.transform.forward = input.Dforward;
                    }
                }
                if (!lockPlaner)
                {
                    planerVec = input.Dforward * input.Dmag * walkSpeed * (input.run ? runSpeed : 1.0f);
                }

            }
            else if (lockController.lockState == true && CheckRolling())
            {

                //if (Vector3.Angle(this.transform.forward, targetDir) > 90)
                //{
                //}

                //this.transform.forward = Vector3.Slerp(this.transform.forward, targetDir, 1f);
                //this.transform.LookAt(lockController.lockTarget.obj.transform);

                if (input.inputEnable)
                {
                    if (input.Dmag > 0.1f)
                    {
                        model.transform.forward = input.Dforward;
                        // model.transform.forward = Vector3.Slerp(model.transform.forward, input.Dforward, 0.03f);
                    }
                }
                if (!lockPlaner)
                {
                    planerVec = input.Dforward * input.Dmag * walkSpeed * (input.run ? runSpeed : 1.0f);
                }
            }
            else
            {
                this.transform.forward = Vector3.Slerp(this.transform.forward, targetDir, .4f);
                //this.transform.LookAt(lockController.lockTarget.obj.transform);
                model.transform.forward = Vector3.Slerp(model.transform.forward, transform.forward, 0.2f);

                //model.transform.forward = transform.forward;
                if (!lockPlaner)
                {
                    planerVec = input.Dforward * input.Dmag * walkSpeed * (input.run ? runSpeed : 1.0f);
                }

            }

        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (rigid != null)
            {
                rigid.position += deltaPos;
                deltaPos = Vector3.zero;
                rigid.velocity = new Vector3(planerVec.x, rigid.velocity.y, planerVec.z) + thrustVec;
                thrustVec = Vector3.zero;
            }
        }

        public override void SendCommand(string command, params object[] objs)
        {
            base.SendCommand(command, objs);
            switch (command)
            {
                case "Lock":
                    Lock((bool)objs[0]);
                    break;
                case "Damage":
                    Damage();
                    break;
                case "Die":
                    Die();
                    break;
                case "Blocked":
                    Blocked();
                    break;
                case "Stunned":
                    Stunned(objs[0] as WeaponController);
                    break;
                case "SetCounterBack":
                    SetCounterBack((bool)objs[0]);
                    break;
                case "Frontstab":
                    FrontStab();
                    break;
                case "CheckDieState":
                    CheckDieState();
                    break;
                case "TryDoDamage":
                    TryDoDamage(objs[0] as WeaponController, (bool)objs[1], (bool)objs[2]);
                    break;
            }
        }

        #endregion

        #region Handle Event

        public void IssueTrigger(string triggerSign)
        {
            if (this == null)
            {
                return;
            }
            animator?.SetTrigger(triggerSign);
        }
        public void IssueBool(string boolSign, bool val)
        {
            if (this == null)
            {
                return;
            }
            animator?.SetBool(boolSign, val);
        }
        public void SetCounterBack(bool val)
        {
            stateController.isCounterBackEnable = val;
        }

        public void TryDoDamage(WeaponController wcTarget, bool attackVeild, bool counterVeild)
        {
            if (stateController.counterBackSuccess && counterVeild)
            {
                //wcTarget.SendCommand("Stunned", wcTarget);
            }
            else if (stateController.counterBackFailer && attackVeild)
            {
                HitOrDie(wcTarget, 1, false);
            }
            else if (stateController.HPisZero)
            {
                //do no thing
            }
            else if (stateController.immortal)
            {
                //do no thing
            }
            else if (stateController.isDefense)
            {
                Blocked();
            }
            else
            {
                if (attackVeild)
                {
                    HitOrDie(wcTarget);
                }
            }
        }
        public void HitOrDie(float damageVal, bool doHitAnimation = true)
        {
            stateController.AddHP(-damageVal);
            if (stateController.HPisZero)
            {
                Die();
            }
            else if (doHitAnimation)
            {
                Damage();
            }
        }
        public void HitOrDie(WeaponController targetWc, float damageSample = 1f, bool doHitAnimation = true)
        {
            float damageVal = targetWc.GetAtk() * damageSample;
            HitOrDie(damageVal, doHitAnimation);
        }

        public void Stunned(WeaponController wcTarget)
        {

            IssueTrigger("stunned");
        }

        public void Blocked()
        {
            IssueTrigger("blocked");
        }

        public void Damage()
        {
            IssueTrigger("damage");
        }

        public void Lock(bool val)
        {
            IssueBool("lock", val);
        }

        private void FrontStab()
        {
            HitOrDie(10, false);
        }

        public void Die()
        {
            IssueTrigger("die");
            input.inputEnable = false;
            if (!animator.CheckState("die"))
            {
                Debug.Log("keep Die State!");
                IssueBool("keepDieState", true);
            }
        }
        public void CheckDieState()
        {
            if (stateController.HPisZero)
            {

                dm.pd.time = dm.pd.playableAsset.duration;
                //dm.pd.Evaluate ();
                dm.pd.Stop();
            }
        }
        protected override void DoAction()
        {
            foreach (var ecastController in interactionController.ecastControllerList)
            {
                if (!ecastController.active || dm.IsPlaying())
                {
                    continue;
                }
                if (ecastController.eventName == "frontStab")
                {
                    ecastController.active = false;
                    transform.position = ecastController.transform.position + ecastController.transform.forward * ecastController.offset.z;
                    model.transform.LookAt(ecastController.transform, Vector3.up);

                    dm.Play("frontStab", this, ecastController.ac as AnimatedObjectController);
                }
                else if (ecastController.eventName == "treasureBox")
                {

                    bool canOpenBox = BattleManager.CheckAnglePlayer(this.model, ecastController.gameObject, 45);
                    if (canOpenBox)
                    {
                        //this.transform.LookAt(ecastController.transform, Vector3.up);
                        transform.position = ecastController.transform.position + ecastController.transform.forward * ecastController.offset.z;
                        model.transform.LookAt(ecastController.transform, Vector3.up);
                        dm.Play("treasureBox", this, ecastController.ac as AnimatedObjectController);
                        ecastController.active = false;
                    }
                }
                else if (ecastController.eventName == "leverUp")
                {
                    bool canLeverUp = BattleController.CheckAnglePlayer(model, ecastController.gameObject, 45);
                    if (canLeverUp)
                    {
                        //this.transform.LookAt(ecastController.transform, Vector3.up);
                        transform.position = ecastController.transform.position + ecastController.transform.forward * ecastController.offset.z;
                        
                        model.transform.LookAt(ecastController.transform, Vector3.up);
                        dm.Play("leverUp", this, ecastController.ac as AnimatedObjectController);
                        ecastController.active = false;
                    }
                }
                else if (ecastController.eventName == "item")
                {
                    //Destroy(ecastController.gameObject);
                    //var ac = ecastController.ac as ItemAC;
                    //Debug.Log(ac);
                    //InventoryManager.Instance.Add(item);
                    //model.GetComponent<Renderer>().enabled = false;
                    //FindObjectOfType<HUDManager>().takingPanel.gameObject.SetActive(true);
                    //ecastController.active = false;
                }
            }
        }
        #endregion

        #region Message processing


        private void OnJumpEnter()
        {
            input.inputEnable = false;
            Debug.Log("Jump!");
            lockPlaner = true;
            thrustVec = new Vector3(0, jumpVelocity, 0);
        }

        private void OnJumpStay()
        {
            //input.inputEnable = false;

        }

        private void InGround()
        {
            animator.SetBool("InGround", true);
        }

        private void NotInGround()
        {
            animator.SetBool("InGround", false);
        }

        private void OnGroundEnter()
        {
            animator.SetLayerWeight(animator.GetLayerIndex("Weapon Up"), 1f);
            input.inputEnable = true;

            lockPlaner = false;
            coll.material = fricationOne;
        }

        private void OnGroundExit()
        {
            animator.SetLayerWeight(animator.GetLayerIndex("Weapon Up"), 0f);
            lockPlaner = true;
            coll.material = fricationZero;
        }

        private void OnFallEnter()
        {
            input.inputEnable = false;
            lockPlaner = true;
        }

        private Vector3 tempForward;
        private void OnRollEnter()
        {
            tempForward = model.transform.forward;
            thrustVec = rollVelocity * model.transform.forward /*+ new Vector3(0, 2, 0)*/;
            input.inputEnable = false;
            lockPlaner = true;
        }

        private void OnRollStay()
        {
            thrustVec = rollVelocity * tempForward;
        }

        private void OnJabEnter()
        {
            input.inputEnable = false;
            lockPlaner = true;
        }

        private void OnJabStay()
        {
            thrustVec = animator.GetFloat("jabVelocity") * (model.transform.forward);
        }

        private void OnAttackEnter()
        {

            lockPlaner = true;
            input.inputEnable = false;
            planerVec = Vector3.zero;
        }

        private void OnAttackExit()
        {
            //animator.gameObject.SendMessage("WeaponDisable");
            weaponController.WeaponDisable();
        }

        private void OnAttack1hAUpdate()
        {
            thrustVec = animator.GetFloat("attackVelocity") * model.transform.forward;
        }

        private void OnUpdateAnimatorMove(object _deltaPos)
        {
            if (animator.CheckState("attack1hC"))
            {
                this.deltaPos = this.deltaPos * .7f + (Vector3)_deltaPos * .3f;
            }
        }

        private void OnImpactEnter()
        {
            input.inputEnable = false;
            lockPlaner = true;
            planerVec = Vector3.zero;
        }

        private void OnDieEnter()
        {
            input.inputEnable = false;
            planerVec = Vector3.zero;
        }

        private void OnBlockedEnter()
        {
            input.inputEnable = false;
            planerVec = Vector3.zero;
        }

        private void OnCounterBackEnter()
        {
            input.inputEnable = false;
            planerVec = Vector3.zero;
        }


        private void OnStunnedEnter()
        {
            input.inputEnable = false;
            planerVec = Vector3.zero;
            frontStabEcManager.active = true;
        }

        private void OnStunnedExit()
        {
            frontStabEcManager.active = false;
        }

        private void OnDieStateEnter()
        {
            input.inputEnable = false;
            planerVec = Vector3.zero;
            //animator.SetBool("keepDieState", false);
        }

        private void OnLockEnter()
        {
            input.inputEnable = false;
            planerVec = Vector3.zero;
        }
        #endregion
    }
}
