using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DS_RE
{

    public class BattleController : MonoBehaviour
    {
        public CharacterController ac;
        private CapsuleCollider defenseCollider;
        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Weapon"))
            {
                WeaponController wc = other.GetComponentInParent<WeaponController>();

                GameObject attcker = wc.ac.model;
                GameObject reciver = ac.model;
                ac.TryDoDamage(wc, CheckAngleTarget(reciver, attcker, 60), CheckAnglePlayer(reciver, attcker, 35));
                //ac.SendCommand("TryDoDamage", wc, CheckAngleTarget(reciver, attcker, 60), CheckAnglePlayer(reciver, attcker, 35));
            }
        }

        // Use this for initialization
        void Start()
        {
            defenseCollider = GetComponent<CapsuleCollider>();
            defenseCollider.center = new Vector3(0, 0.9f, 0);
            defenseCollider.height = 1.6f;
            defenseCollider.radius = 0.4f;
        }

        public static bool CheckAnglePlayer(GameObject player, GameObject target, float playerAngleLimit)
        {
            Vector3 counterDir = target.transform.position - player.transform.position;

            float counterAngle1 = Vector3.Angle(player.transform.forward, counterDir);
            float counterAngle2 = Vector3.Angle(target.transform.forward, player.transform.forward);

            bool counterVeild = counterAngle1 < playerAngleLimit && Mathf.Abs(counterAngle2 - 180) < playerAngleLimit;
            return counterVeild;
        }

        public static bool CheckAngleTarget(GameObject player, GameObject target, float targetAngleLimit)
        {
            Vector3 attackingDir = player.transform.position - target.transform.position;

            float attackingAngle1 = Vector3.Angle(target.transform.forward, attackingDir);

            return attackingAngle1 < targetAngleLimit;
        }
    }
}
