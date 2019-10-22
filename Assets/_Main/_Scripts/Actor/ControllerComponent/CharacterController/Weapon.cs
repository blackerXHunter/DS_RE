using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DS_RE
{
    public class Weapon : MonoBehaviour
    {
        public WeaponController wc;
        public WeaponData wdata;

        // Use this for initialization
        private void Awake()
        {
            wdata = GetComponentInChildren<WeaponData>();
        }

        public float GetAtk()
        {
            if (wdata == null)
            {
                Debug.Log(gameObject.name + " has not wdata !");
            }
            return wdata.ATK + wc.ac.stateController.ATK;
        }
    }
}
