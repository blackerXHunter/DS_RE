using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAC : IActorController
{
    #region Feilds

    private Rigidbody rigid;

    private Collider coll;

    public CameraController camCtrl;


    [SerializeField]
    private float walkSpeed = 1.4f;
    [SerializeField]
    private float runSpeed = 2.5f;

    public UserInput playerInput;

    private Vector3 planerVec;

    private Vector3 thrustVec;

    public float jumpVelocity = 3.0f;

    public float rollVelocity = 3.0f;

    public float jabVelocity = 2.0f;

    private bool lockPlaner = false;

    //private bool canAttack;

    //private bool leftIsShild = true;

    private Vector3 deltaPos;

    [Header("===== Frication Settings =====")]
    public PhysicMaterial fricationOne;
    public PhysicMaterial fricationZero;

    #endregion

    // Use this for initialization
    private void Awake()
    {
        actor = model.GetComponent<Animator>();
        playerInput = UserInput.GetEnabledUserInput(gameObject);
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerInput == null)
        {
            return;
        }
        float targetRunMulti = (playerInput.run ? 2.0f : 1.0f);

        if (camCtrl.lockState == false)
        {
            actor.SetFloat("Forward", Mathf.Lerp(actor.GetFloat("Forward"), playerInput.Dmag * targetRunMulti, 0.2f));
        }
        else
        {
            actor.SetFloat("Forward", Mathf.Lerp(actor.GetFloat("Forward"), playerInput.Dup * targetRunMulti, 0.2f));
            actor.SetFloat("right", Mathf.Lerp(actor.GetFloat("right"), playerInput.Dright * targetRunMulti, 0.2f));
        }

        if (playerInput.roll)
        {
            actor.SetTrigger("Roll");
        }
        if (CheckState("ground") || CheckState("blocked"))
        {
            actor.SetBool("defense", playerInput.defense);
            actor.SetLayerWeight(actor.GetLayerIndex("Defense Layer"), playerInput.defense ? 1 : 0);
        }

        if (playerInput.jump)
        {
            actor.SetTrigger("Jump");

        }


        if (playerInput.rb)
        {

            if (CheckState("ground") || CheckStateTag("attackR"))
            {
                actor.SetTrigger("attack");
            }
        }
        // left heavy(left trigger)
        if (playerInput.lt)
        {
            if (CheckState("ground") || CheckStateTag("attackR"))
            {
                actor.SetTrigger("counterBack");
            }
        }
        // TODO:翻滚时不应该锁定
        if (camCtrl.lockState == false || playerInput.roll)
        {
            if (playerInput.inputEnable)
            {

                if (playerInput.Dmag > 0.1f)
                {
                    model.transform.forward = Vector3.Slerp(model.transform.forward, playerInput.Dforward, 0.3f);
                }

            }
            if (!lockPlaner)
            {
                planerVec = playerInput.Dforward * playerInput.Dmag * walkSpeed * (playerInput.run ? runSpeed : 1.0f);
            }

        }
        else
        {
            model.transform.forward = transform.forward;

            if (!lockPlaner)
            {
                planerVec = playerInput.Dforward * playerInput.Dmag * walkSpeed * (playerInput.run ? runSpeed : 1.0f);
            }

        }

        if (playerInput.action)
        {
            OnAction.Invoke();
        }
    }

    private void FixedUpdate()
    {
        if (rigid != null)
        {
            rigid.position += deltaPos;
            deltaPos = Vector3.zero;
            rigid.velocity = new Vector3(planerVec.x, rigid.velocity.y, planerVec.z) + thrustVec;
            thrustVec = Vector3.zero;
        }
    }

    public bool CheckState(string stateName, string layerName = "Base Layer")
    {
        return actor.GetCurrentAnimatorStateInfo(actor.GetLayerIndex(layerName)).IsName(stateName);
    }

    public bool CheckStateTag(string stateTag, string layerName = "Base Layer")
    {
        return actor.GetCurrentAnimatorStateInfo(actor.GetLayerIndex(layerName)).IsTag(stateTag);
    }

    #region Message processing


    private void OnJumpEnter()
    {
        playerInput.inputEnable = false;
        lockPlaner = true;
        thrustVec = new Vector3(0, jumpVelocity, 0);
    }

    private void InGround()
    {
        actor.SetBool("InGround", true);
    }

    private void NotInGround()
    {
        actor.SetBool("InGround", false);
    }

    private void OnGroundEnter()
    {
        playerInput.inputEnable = true;
        lockPlaner = false;
        //canAttack = true;
        coll.material = fricationOne;
    }

    private void OnGroundExit()
    {
        playerInput.inputEnable = true;
        lockPlaner = true;
        //canAttack = true;
        coll.material = fricationZero;
    }

    private void OnFallEnter()
    {
        playerInput.inputEnable = false;
        lockPlaner = true;
    }

    private void OnRollEnter()
    {
        thrustVec = rollVelocity * model.transform.forward + new Vector3(0, 2, 0);
        playerInput.inputEnable = false;
        lockPlaner = true;
    }

    private void OnJabEnter()
    {
        playerInput.inputEnable = false;
        lockPlaner = true;
    }

    private void OnJabStay()
    {
        thrustVec = actor.GetFloat("jabVelocity") * (model.transform.forward);
    }

    private void OnAttackEnter()
    {

        lockPlaner = true;
        playerInput.inputEnable = false;
        planerVec = Vector3.zero;
    }

    private void OnAttackExit()
    {
        actor.gameObject.SendMessage("WeaponDisable");
    }

    private void OnAttack1hAUpdate()
    {
        thrustVec = actor.GetFloat("attackVelocity") * model.transform.forward;
    }

    private void OnUpdateAnimatorMove(object _deltaPos)
    {
        if (CheckState("attack1hC"))
        {
            this.deltaPos = this.deltaPos * .7f + (Vector3)_deltaPos * .3f;
        }
    }

    private void OnImpactEnter()
    {
        playerInput.inputEnable = false;
        lockPlaner = true;
        planerVec = Vector3.zero;
    }

    private void OnDieEnter()
    {
        playerInput.inputEnable = false;
        planerVec = Vector3.zero;
    }

    private void OnBlockedEnter()
    {
        playerInput.inputEnable = false;
        planerVec = Vector3.zero;
    }

    private void OnCounterBackEnter()
    {
        playerInput.inputEnable = false;
        planerVec = Vector3.zero;
    }


    public EventCasterManager frontStabEcManager;
    private void OnStunnedEnter()
    {
        playerInput.inputEnable = false;
        planerVec = Vector3.zero;
        frontStabEcManager.active = true;
    }

    private void OnStunnedExit()
    {
        frontStabEcManager.active = false;
    }

    private void OnDieStateEnter()
    {
        playerInput.inputEnable = false;
        planerVec = Vector3.zero;
        //actor.SetBool("keepDieState", false);
    }

    private void OnLockEnter()
    {
        playerInput.inputEnable = false;
        planerVec = Vector3.zero;
    }
    #endregion
}
