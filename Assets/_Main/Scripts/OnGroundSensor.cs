using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{

    public CapsuleCollider capcol;
    public float offset = .1f;

    private Vector3 point1, point2;
    private float raduis;

    private void Awake()
    {
        raduis = capcol.radius - 0.05f;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        point1 = transform.position + transform.up * (raduis - offset);
        point2 = transform.position + transform.up * (capcol.height - raduis - offset);
        Collider[] colliders = Physics.OverlapCapsule(point1, point2, raduis, LayerMask.GetMask("Ground"));
        if (colliders.Length != 0)
        {
            SendMessageUpwards("InGround");
        }
        else
        {
            SendMessageUpwards("NotInGround");
        }
    }
}
