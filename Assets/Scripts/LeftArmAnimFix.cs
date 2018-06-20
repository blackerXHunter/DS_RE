using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAnimFix : MonoBehaviour
{

    private Animator anim;

    public Vector3 angles;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!anim.GetBool("defense"))
        {
            Transform leftLowerArmTransform = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);

            leftLowerArmTransform.localEulerAngles += angles;

            anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftLowerArmTransform.localEulerAngles));
        }

    }
}
