using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DS_RE
{
    public class LockController : MonoBehaviour
    {
        public CharacterController ac;
        public bool isAI = false;
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
            public ActorManager am;

            public LockTarget(GameObject obj)
            {
                this.obj = obj;
                am = obj.GetComponent<ActorManager>();
            }
        }

        [SerializeField]
        public LockTarget lockTarget = null;

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
