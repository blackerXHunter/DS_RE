using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAnimFix : MonoBehaviour {

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        Transform leftLowerArmTransform = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);

        leftLowerArmTransform.localEulerAngles = Vector3.zero;
    }
}
