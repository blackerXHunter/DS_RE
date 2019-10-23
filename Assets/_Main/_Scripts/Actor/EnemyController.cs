using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DS_RE
{
    public class EnemyController : CharacterController
    {
        protected override void Update()
        {
            base.Update();
            //float targetRunMulti = (input.run ? 2.0f : 1.0f);
            //animator.SetFloat("Forward", Mathf.Lerp(animator.GetFloat("Forward"), input.Dmag * targetRunMulti, 0.2f));
            //if (input.inputEnable)
            //{


            //    if (input.Dmag > 0.1f)
            //    {
            //        model.transform.forward = Vector3.Slerp(model.transform.forward, input.Dforward, 0.3f);
            //    }


            //}
            //if (!lockPlaner)
            //{
            //    planerVec = input.Dforward * input.Dmag * walkSpeed * (input.run ? runSpeed : 1.0f);
            //}
        }
    }
}
