using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{

    public CapsuleCollider capcol;

    private Vector3 point1, point2;
    private float raduis;

    private void Awake()
    {
        raduis = capcol.radius;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        point1 = transform.position + transform.up * raduis;
        point2 = transform.position + transform.up * (capcol.height - raduis);
        Collider[] colliders = Physics.OverlapCapsule(point1, point2, raduis, LayerMask.GetMask("Ground"));
        if (colliders.Length != 0)
        {
            foreach (var coll in colliders)
            {
                Debug.Log("Collision : " + coll.name);
            }
        }
    }
}
