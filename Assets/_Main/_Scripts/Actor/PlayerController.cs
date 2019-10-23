using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DS_RE
{

    public class PlayerController : CharacterController
    {
        protected override void Awake()
        {
            base.Awake();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }
        protected override void Start()
        {
            base.Start();
        }
        protected override void Update()
        {
            base.Update();
            //animator.SetBool("camera lock", camCtrl.lockState);
            //float targetRunMulti = (input.run ? 2.0f : 1.0f);
            //if (camCtrl.lockState == false)
            //{
            //    animator.SetFloat("Forward", Mathf.Lerp(animator.GetFloat("Forward"), input.Dmag * targetRunMulti, 0.2f));
            //}
            //else
            //{
            //    animator.SetFloat("Forward", Mathf.Lerp(animator.GetFloat("Forward"), input.Dup * targetRunMulti, 0.2f));
            //    animator.SetFloat("right", Mathf.Lerp(animator.GetFloat("right"), input.Dright * targetRunMulti, 0.2f));
            //}
            ////Debug.Log(CheckRolling());
            //if (camCtrl.lockState == false)
            //{
            //    if (input.inputEnable)
            //    {


            //        if (input.Dmag > 0.1f)
            //        {
            //            model.transform.forward = Vector3.Slerp(model.transform.forward, input.Dforward, 0.3f);
            //        }


            //    }
            //    if (!lockPlaner)
            //    {
            //        planerVec = input.Dforward * input.Dmag * walkSpeed * (input.run ? runSpeed : 1.0f);
            //    }

            //}
            //else if (camCtrl.lockState == true && CheckRolling())
            //{
            //    if (input.inputEnable)
            //    {
            //        if (input.Dmag > 0.1f)
            //        {
            //            // model.transform.forward = Vector3.Slerp(model.transform.forward, input.Dforward, 0.03f);
            //            model.transform.forward = input.Dforward;
            //        }
            //    }
            //    if (!lockPlaner)
            //    {
            //        planerVec = input.Dforward * input.Dmag * walkSpeed * (input.run ? runSpeed : 1.0f);
            //    }
            //}
            //else
            //{
            //    //model.transform.forward = Vector3.Slerp(model.transform.forward, transform.forward, 0.1f);
            //    model.transform.forward = transform.forward;

            //    if (!lockPlaner)
            //    {
            //        planerVec = input.Dforward * input.Dmag * walkSpeed * (input.run ? runSpeed : 1.0f);
            //    }

            //}
        }
    }
}
