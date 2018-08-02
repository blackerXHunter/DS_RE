using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour {

    public GameObject model;

    private Animator actor;

    private Rigidbody rigid;

    private Collider coll;

    public CameraController camCtrl;

    [SerializeField]
    private float walkSpeed = 1.4f;
    [SerializeField]
    private float runSpeed = 2.5f;

    private UserInput playerInput;

    private Vector3 planerVec;

    private Vector3 thrustVec;

    public float jumpVelocity = 3.0f;

    public float rollVelocity = 3.0f;

    public float jabVelocity = 2.0f;

    private bool lockPlaner = false;

    private bool canAttack;

    private float lerpTarget;

    private Vector3 deltaPos;

    [SerializeField]
    private float lerpSpeed = 1.0f;

    [Header("===== Frication Settings =====")]
    public PhysicMaterial fricationOne;
    public PhysicMaterial fricationZero;

    // Use this for initialization
    private void Awake() {
        actor = model.GetComponent<Animator>();
        playerInput = UserInput.GetEnabledUserInput(gameObject);
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        camCtrl = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    private void LateUpdate() {
        float targetRunMulti = (playerInput.run ? 2.0f : 1.0f);
        if (camCtrl.lockState == false) {
            actor.SetFloat("Forward",  Mathf.Lerp(actor.GetFloat("Forward"), playerInput.Dmag * targetRunMulti, 0.2f));
        }
        else {
            actor.SetFloat("Forward",  Mathf.Lerp(actor.GetFloat("Forward"), playerInput.Dup * targetRunMulti, 0.2f));
            actor.SetFloat("right",  Mathf.Lerp(actor.GetFloat("right"), playerInput.Dright * targetRunMulti, 0.2f));
        }

        if (playerInput.roll) {
            actor.SetTrigger("Roll");
        }

        actor.SetBool("defense", playerInput.defense);

        if (playerInput.jump) {
            actor.SetTrigger("Jump");
            canAttack = false;
        }

        if (playerInput.attack && CheckState("ground") && canAttack) {
            actor.SetTrigger("attack");
        }

        if (camCtrl.lockState == false) {

            if (playerInput.Dmag > 0.1f) {
                model.transform.forward = Vector3.Slerp(model.transform.forward, playerInput.Dforward, 0.3f);
            }

            if (!lockPlaner) {
                planerVec = playerInput.Dforward * playerInput.Dmag * walkSpeed * (playerInput.run ? runSpeed : 1.0f);
            }
        }
        else {
            model.transform.forward = transform.forward;

            if (!lockPlaner) {
                planerVec = playerInput.Dforward * playerInput.Dmag * walkSpeed * (playerInput.run ? runSpeed : 1.0f);
            }
        }

    }

    private void FixedUpdate() {
        rigid.position += deltaPos;
        deltaPos = Vector3.zero;
        rigid.velocity = new Vector3(planerVec.x, rigid.velocity.y, planerVec.z) + thrustVec;
        thrustVec = Vector3.zero;
    }

    private bool CheckState(string stateName, string layerName = "Base Layer") {
        return actor.GetCurrentAnimatorStateInfo(actor.GetLayerIndex(layerName)).IsName(stateName);
    }

    #region Message processing


    private void OnJumpEnter() {
        playerInput.inputEnable = false;
        lockPlaner = true;
        thrustVec = new Vector3(0, jumpVelocity, 0);
    }

    //private void OnJumpExit()
    //{
    //    playerInput.inputEnable = true;
    //    lockPlaner = false;
    //}

    private void InGround() {
        actor.SetBool("InGround", true);
    }

    private void NotInGround() {
        actor.SetBool("InGround", false);
    }

    private void OnGroundEnter() {
        playerInput.inputEnable = true;
        lockPlaner = false;
        canAttack = true;
        coll.material = fricationOne;
    }

    private void OnGroundExit() {
        playerInput.inputEnable = true;
        lockPlaner = false;
        canAttack = true;
        coll.material = fricationZero;
    }

    private void OnFallEnter() {
        playerInput.inputEnable = false;
        lockPlaner = true;
        //thrustVec = new Vector3(0, jumpVelocity, 0);
    }

    private void OnRollEnter() {
        thrustVec = rollVelocity * model.transform.forward + new Vector3(0, 2, 0);
        playerInput.inputEnable = false;
        lockPlaner = true;
    }

    private void OnJabEnter() {
        playerInput.inputEnable = false;
        lockPlaner = true;
    }

    private void OnJabStay() {
        thrustVec = actor.GetFloat("jabVelocity") * (model.transform.forward);
    }

    private void OnAttack1hAEnter() {
        playerInput.inputEnable = false;
        lerpTarget = 1.0f;
    }

    private void OnAttack1hAUpdate() {
        thrustVec = actor.GetFloat("attackVelocity") * model.transform.forward;
        var currentLayerWeight = actor.GetLayerWeight(actor.GetLayerIndex("Attack Layer"));
        currentLayerWeight = Mathf.Lerp(currentLayerWeight, lerpTarget, Time.deltaTime * lerpSpeed);
        actor.SetLayerWeight(actor.GetLayerIndex("Attack Layer"), currentLayerWeight);
    }

    private void OnAttackIdelEnter() {
        playerInput.inputEnable = true;
        lerpTarget = 0.0f;
    }

    private void OnAttackIdelUpdate() {
        var currentLayerWeight = actor.GetLayerWeight(actor.GetLayerIndex("Attack Layer"));
        currentLayerWeight = Mathf.Lerp(currentLayerWeight, lerpTarget, Time.deltaTime * lerpSpeed);
        actor.SetLayerWeight(actor.GetLayerIndex("Attack Layer"), currentLayerWeight);
    }


    private void OnUpdateAnimatorMove(object _deltaPos) {
        if (CheckState("attack1hC", "Attack Layer")) {
            this.deltaPos = this.deltaPos * .7f + (Vector3)_deltaPos * .3f;
        }
    }
    #endregion
}
