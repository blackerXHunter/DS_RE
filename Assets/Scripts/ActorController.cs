﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{

    public GameObject model;

    [SerializeField]
    private Animator actor;

    private Rigidbody rigid;

    [SerializeField]
    private float walkSpeed = 1.4f;
    [SerializeField]
    private float runSpeed = 2.5f;

    private PlayerInput playerInput;

    private Vector3 planerVec;

    private Vector3 thrustVec;

    public float jumpVelocity = 3.0f;

    private bool lockPlaner = false;

    // Use this for initialization
    private void Awake()
    {
        actor = model.GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        float targetRunMulti = (playerInput.run ? 2.0f : 1.0f);
        actor.SetFloat("Forward", playerInput.Dmag * Mathf.Lerp(actor.GetFloat("Forward"), targetRunMulti, 0.2f));
        if (playerInput.Dmag > 0.1f)
        {
            model.transform.forward = Vector3.Slerp(model.transform.forward, playerInput.Dforward, 0.3f);
        }

        if (!lockPlaner)
        {
            planerVec = playerInput.Dforward * playerInput.Dmag * walkSpeed * (playerInput.run ? runSpeed : 1.0f);
        }

        if (playerInput.jump)
        {
            actor.SetTrigger("Jump");
        }
    }

    private void FixedUpdate()
    {
        //rigid.position += planerVec * Time.fixedDeltaTime;
        rigid.velocity = new Vector3(planerVec.x, rigid.velocity.y, planerVec.z) + thrustVec;
        thrustVec = Vector3.zero;
    }

    private void OnJumpEnter()
    {
        playerInput.inputEnable = false;
        lockPlaner = true;
        thrustVec = new Vector3(0, jumpVelocity, 0);
    }

    private void OnJumpExit()
    {
        playerInput.inputEnable = true;
        lockPlaner = false;
    }
}
