using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour {
    public ActorController ac;

    [Header("=== Auto Gen If Null ===")]
    public BattleManager bm;
    public WeaponManager wm;
    public StateManager sm;
    public DirectorManager dm;
    public InteractionManager im;
    // Use this for initialization
    private void Awake() {
        ac = GetComponent<ActorController>();
        ac.OnAction += DoAction;
    }

    private void DoAction() {
        foreach (var ecastManager in im.ecastmanaList) {
            if (!ecastManager.active) {
                continue;
            }
            if (ecastManager.eventName == "frontStab") {
                dm.Play("frontStab", this, ecastManager.am);
            }
            else if (ecastManager.eventName == "treasureBox") {

                bool canOpenBox = BattleManager.CheckAnglePlayer(this.ac.model, ecastManager.am.gameObject, 30);
                if (canOpenBox) {
                    dm.Play("treasureBox", this, ecastManager.am);
                    ecastManager.active = false;
                    Debug.Log("treasreBox");
                }
            }
        }
    }

    void Start() {
        var sensorTransform = transform.Find("sensors");
        if (sensorTransform != null) {
            GameObject sensor = sensorTransform.gameObject;
            bm = Bind<BattleManager>(sensor);
            im = Bind<InteractionManager>(sensor);
        }

        GameObject model = ac.model;
        wm = Bind<WeaponManager>(model);

        sm = Bind<StateManager>(gameObject);

        dm = Bind<DirectorManager>(gameObject);

    }

    private T Bind<T>(GameObject obj) where T : IActorManager {
        T iacM = obj.GetComponent<T>();
        if (iacM == null) {
            iacM = obj.AddComponent<T>();
        }
        iacM.am = this;
        return iacM;
    }

    public void SetCounterBack(bool val) {
        sm.isCounterBackEnable = val;
    }


    public void TryDoDamage(WeaponController wcTarget, bool attackVeild, bool counterVeild) {
        if (sm.counterBackSuccess && counterVeild) {
            wcTarget.wm.am.Stunned();
        }
        else if (sm.counterBackFailer && attackVeild) {
            HitOrDie(false);
        }
        else if (sm.HPisZero) {
            //do no thing
        }
        else if (sm.immortal) {
            //do no thing
        }
        else if (sm.isDefense) {
            Blocked();
        }
        else {
            if (attackVeild) {
                HitOrDie();
            }
        }
    }

    public void HitOrDie(bool doHitAnimation = true) {
        sm.AddHP(-5);
        if (sm.HPisZero) {
            Die();
        }
        else if (doHitAnimation) {
            Damage();
        }
    }

    public void Stunned() {
        ac.IssueTrigger("stunned");
    }

    public void Blocked() {
        ac.IssueTrigger("blocked");
    }

    public void Damage() {
        ac.IssueTrigger("damage");
    }

    public void Lock(bool val) {
        ac.IssueBool("lock", val);
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
