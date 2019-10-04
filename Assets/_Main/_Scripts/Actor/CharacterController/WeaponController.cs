using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DS_RE
{
    public class WeaponController : MonoBehaviour
    {
        public CharacterController ac;
        public Collider weaponColliderL, weaponColliderR;
        public GameObject whL, whR;
        public Weapon weaponL, weaponR;
        public float GetAtk() => weaponR.GetAtk();

        private void Start()
        {
            try
            {
                whR = transform.DeepFind("weaponHandleR").gameObject;
                weaponR = BindWeaponController(whR);
                weaponColliderR = whR.GetComponentInChildren<Collider>();
            }
            catch (System.Exception)
            {
                Debug.LogWarning("right weapon is null");
            }

            try
            {
                whL = transform.DeepFind("weaponHandleL").gameObject;
                weaponL = BindWeaponController(whL);
                weaponColliderL = whL.GetComponentInChildren<Collider>();
            }
            catch (System.Exception)
            {
                Debug.LogWarning("left weapon is null");
            }

        }

        private Weapon BindWeaponController(GameObject bindObj)
        {
            Weapon tempWc;
            tempWc = bindObj.GetComponent<Weapon>();
            if (tempWc == null)
            {
                tempWc = bindObj.AddComponent<Weapon>();
            }
            tempWc.wc = this;
            return tempWc;
        }
        public void WeaponEnable()
        {
            weaponColliderL.enabled = true;
            weaponColliderR.enabled = true;
        }
        public void WeaponDisable()
        {
            weaponColliderL.enabled = false;
            weaponColliderR.enabled = false;
        }

        private void CounterBackEnable()
        {
            ac.stateController.isCounterBackEnable = true;
        }
        private void CounterBackDisable()
        {
            ac.stateController.isCounterBackEnable = false;
        }

        private void OnCounterBackExit()
        {
            ac.stateController.isCounterBackEnable = false;
        }
    }
}
