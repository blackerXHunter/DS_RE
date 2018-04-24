using System.Collections;
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

    private PlayerInput playerInput;

    private Vector3 movingVec;

    // Use this for initialization
    private void Awake()
    {
        actor = model.GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        actor.SetFloat("Forward", playerInput.Dmag);
        if (playerInput.Dmag > 0.1f)
        {
            model.transform.forward = playerInput.Dforward;
        }
        movingVec = playerInput.Dforward * playerInput.Dmag * walkSpeed;
    }

    private void FixedUpdate()
    {
        rigid.position += movingVec * Time.fixedDeltaTime;
    }
}
