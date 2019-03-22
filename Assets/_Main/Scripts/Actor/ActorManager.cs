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
            if (!ecastManager.active && !dm.IsPlaying()) {
                continue;
            }
            if (ecastManager.eventName == "frontStab") {
                transform.position = ecastManager.am.transform.position + ecastManager.am.transform.forward * ecastManager.offset.z;
                ac.model.transform.LookAt(ecastManager.am.transform, Vector3.up);
                dm.Play("frontStab", this, ecastManager.am);
            }
            else if (ecastManager.eventName == "treasureBox") {

                bool canOpenBox = BattleManager.CheckAnglePlayer(this.ac.model, ecastManager.am.gameObject, 45);
                if (canOpenBox) {

                    transform.position = ecastManager.am.transform.position + ecastManager.am.transform.forward * ecastManager.offset.z;
                    ac.model.transform.LookAt(ecastManager.am.transform, Vector3.up);
                    dm.Play("treasureBox", this, ecastManager.am);
                    ecastManager.active = false;
                }
            }
            else if (ecastManager.eventName == "leverUp") {
                bool canLeverUp = BattleManager.CheckAnglePlayer(this.ac.model, ecastManager.am.gameObject, 45);
                if (canLeverUp) {
                    Debug.Log("leverUp");
                    transform.position = ecastManager.am.transform.position + ecastManager.am.transform.forward * ecastManager.offset.z;
                    ac.model.transform.LookAt(ecastManager.am.transform, Vector3.up);
                    dm.Play("leverUp", this, ecastManager.am);
                    ecastManager.active = false;
                }
            }
            else if (ecastManager.eventName == "item")
            {
                Debug.Log("Item");
                Destroy( ecastManager.am.gameObject);
                FindObjectOfType<HUDManager>().takingPanel.gameObject.SetActive(true);
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
        else {
            Debug.LogWarning("sensor is null");
        }

        GameObject model = ac.model;
        wm = Bind<WeaponManager>(model);

        sm = Bind<StateManager>(gameObject);

        dm = Bind<DirectorManager>(gameObject);

    }

    private T Bind<T>(GameObject obj) where T : IActorManager {
        if (obj == null) {
            return null;
        }
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
            HitOrDie(wcTarget, 2, false);
            wcTarget.wm.am.Stunned();
        }
        else if (sm.counterBackFailer && attackVeild) {
            HitOrDie(wcTarget, 1, false);
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
                HitOrDie(wcTarget);
            }
        }
    }

    public void HitOrDie(WeaponController targetWc, float damageSample = 1f, bool doHitAnimation = true) {
        float damageVal = targetWc.GetAtk() * damageSample;
        Debug.Log(damageSample);
        sm.AddHP(-damageSample);
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
