using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour {
    public ActorController ac;

    [Header("=== Auto Gen If Null ===")]
    public BattleManager bm;
    public WeaponManager wm;
    public StateManager sm;
    // Use this for initialization
    void Start() {
        ac = GetComponent<ActorController>();

        GameObject sensor = transform.Find("sensors").gameObject;
        bm = Bind<BattleManager>(sensor);

        GameObject model = ac.model;
        wm = Bind<WeaponManager>(model);

        sm = Bind<StateManager>(gameObject);

    }

    private T Bind<T>(GameObject obj) where T : IActorManager {
        T iacM = obj.GetComponent<T>();
        if (iacM == null) {
            iacM = obj.AddComponent<T>();
        }
        iacM.am = this;
        return iacM;
    }


    public void TryDoDamage() {
        if (sm.HPisZero) {
            return;
        }

        if (sm.isDefense) {
            Blocked();
        }
        else {
            Damage();
            sm.AddHP(-5);
            if (sm.HPisZero) {
                Die();
            }
        }
    }

    public void Blocked() {
        ac.IssueTrigger("blocked");
    }

    public void Damage() {
        ac.IssueTrigger("damage");
    }

    public void Die() {
        ac.IssueTrigger("die");
        ac.playerInput.inputEnable = false;
        if (ac.camCtrl.lockState == true) {
            ac.camCtrl.LockUnLock();
        }
        ac.camCtrl.enabled = false;
    }
}
