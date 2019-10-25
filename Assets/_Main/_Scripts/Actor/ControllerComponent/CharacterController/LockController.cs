using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DS_RE
{
    public class LockController : MonoBehaviour
    {
        public CharacterController ac;
        public bool isAI = false;
        public float unLockDistance = 10.0f;
        public class LockTarget
        {
            public float halfHeight
            {
                get
                {
                    return obj.GetComponent<Collider>().bounds.size.y / 2;
                }
            }
            public GameObject obj;
            public CharacterController ac;

            public LockTarget(GameObject obj)
            {
                this.obj = obj;
                ac = obj.GetComponent<CharacterController>();
            }
        }
        public LockTarget lockTarget = null;

        private void Update()
        {
            if (lockState == true)
            {
                if (Vector3.Distance(ac.model.transform.position, lockTarget.obj.transform.position) > unLockDistance)
                {
                    ac.lockController.UnLock();
                }
                if (ac.lockController.lockTarget != null && ac.lockController.lockTarget.ac != null && ac.lockController.lockTarget.ac.stateController != null && ac.lockController.lockTarget.ac.stateController.HPisZero)
                {
                    ac.lockController.UnLock();
                }
                if (ac.stateController.isDie)
                {
                    ac.lockController.UnLock();
                }
            }
        }

        public bool lockState = false;
        public void LockUnLock()
        {

            var originPos1 = ac.model.transform.position;
            var originPos2 = originPos1 + new Vector3(0, 1, 0);
            var center = originPos2 + ac.model.transform.forward * 5;
            var cols = Physics.OverlapBox(center, new Vector3(0.5f, 0.5f, 5f), ac.model.transform.rotation, LayerMask.GetMask(isAI ? "Player" : "Enemy"));


            if (lockTarget != null)
            {
                UnLock();
            }
            else
            {
                foreach (var col in cols)
                {
                    Lock(col);
                    break;
                }
            }
        }
        public void UnLock()
        {
            lockTarget = null;
            lockState = false;
        }
        public void Lock(Collider target)
        {
            Lock(target.gameObject);
        }
        public void Lock(GameObject target)
        {
            lockTarget = new LockTarget(target);
            lockState = true;
        }
    }
}
