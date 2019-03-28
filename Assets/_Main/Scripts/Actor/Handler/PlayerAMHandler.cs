using UnityEngine;

class PlayerAMHandler : IActorManagerHandler
{

    private void SetCounterBack(bool val)
    {
        am.sm.isCounterBackEnable = val;
    }


    private void TryDoDamage(WeaponController wcTarget, bool attackVeild, bool counterVeild)
    {
        if (am.sm.counterBackSuccess && counterVeild)
        {
            HitOrDie(wcTarget, 2, false);
            wcTarget.wm.am.SendCommand("Stunned");
        }
        else if (am.sm.counterBackFailer && attackVeild)
        {
            HitOrDie(wcTarget, 1, false);
        }
        else if (am.sm.HPisZero)
        {
            //do no thing
        }
        else if (am.sm.immortal)
        {
            //do no thing
        }
        else if (am.sm.isDefense)
        {
            Blocked();
        }
        else
        {
            if (attackVeild)
            {
                HitOrDie(wcTarget);
            }
        }
    }

    private void HitOrDie(WeaponController targetWc, float damageSample = 1f, bool doHitAnimation = true)
    {
        float damageVal = targetWc.GetAtk() * damageSample;
        am.sm.AddHP(-damageSample);
        if (am.sm.HPisZero)
        {
            Die();
        }
        else if (doHitAnimation)
        {
            Damage();
        }
    }

    private void Stunned()
    {

        am.ac.IssueTrigger("stunned");
    }

    private void Blocked()
    {
        am.ac.IssueTrigger("blocked");
    }

    private void Damage()
    {
        am.ac.IssueTrigger("damage");
    }

    private void Lock(bool val)
    {
        am.ac.IssueBool("lock", val);
    }

    private void Die()
    {
        am.ac.IssueTrigger("die");
        var ac = am.ac as PlayerAC;
        ac.playerInput.inputEnable = false;
        if (ac.camCtrl.lockState == true)
        {
            ac.camCtrl.LockUnLock();
        }
        ac.camCtrl.enabled = false;
    }


    void Start()
    {
        var sensorTransform = transform.Find("sensors");
        if (sensorTransform != null)
        {
            GameObject sensor = sensorTransform.gameObject;
            am.bm = am.Bind<BattleManager>(sensor);
            am.im = am.Bind<InteractionManager>(sensor);
        }
        else
        {
            Debug.LogWarning("sensor is null");
        }

        GameObject model = am.ac.model;
        am.wm = am.Bind<WeaponManager>(model);

        am.sm = am.Bind<StateManager>(gameObject);

        am.dm = am.Bind<DirectorManager>(gameObject);

    }

    public override void DoAction()
    {
        foreach (var ecastManager in am.im.ecastmanaList)
        {
            if (!ecastManager.active || am.dm.IsPlaying())
            {
                continue;
            }
            if (ecastManager.eventName == "frontStab")
            {
                ecastManager.active = false;
                transform.position = ecastManager.am.transform.position + ecastManager.am.transform.forward * ecastManager.offset.z;
                am.ac.model.transform.LookAt(ecastManager.am.transform, Vector3.up);
                am.dm.Play("frontStab", am, ecastManager.am);
            }
            else if (ecastManager.eventName == "treasureBox")
            {

                bool canOpenBox = BattleManager.CheckAnglePlayer(this.am.ac.model, ecastManager.am.gameObject, 45);
                if (canOpenBox)
                {

                    transform.position = ecastManager.am.transform.position + ecastManager.am.transform.forward * ecastManager.offset.z;
                    am.ac.model.transform.LookAt(ecastManager.am.transform, Vector3.up);
                    am.dm.Play("treasureBox", am, ecastManager.am);
                    ecastManager.active = false;
                }
            }
            else if (ecastManager.eventName == "leverUp")
            {
                bool canLeverUp = BattleManager.CheckAnglePlayer(am.ac.model, ecastManager.am.gameObject, 45);
                if (canLeverUp)
                {
                    transform.position = ecastManager.am.transform.position + ecastManager.am.transform.forward * ecastManager.offset.z;
                    am.ac.model.transform.LookAt(ecastManager.am.transform, Vector3.up);
                    am.dm.Play("leverUp", am, ecastManager.am);
                    ecastManager.active = false;
                }
            }
            else if (ecastManager.eventName == "item")
            {
                //Destroy( ecastManager.am.gameObject);
                var ac = ecastManager.am.ac as ItemAC;
                Debug.Log(ac);
                InventoryManager.Instance.Add(ac.item);
                ac.model.GetComponent<Renderer>().enabled = false;
                FindObjectOfType<HUDManager>().takingPanel.gameObject.SetActive(true);
                ecastManager.active = false;
            }
        }
    }

    public override void Handle(string command, object[] objs)
    {
        switch (command)
        {
            case "Lock":
                Lock((bool)objs[0]);
                break;
            case "Damage":
                Damage();
                break;
            case "Die":
                Die();
                break;
            case "Blocked":
                Blocked();
                break;
            case "Stunned":
                Stunned();
                break;
            case "SetCounterBack":
                SetCounterBack((bool)objs[0]);
                break;
            case "TryDoDamage":
                TryDoDamage(objs[0] as WeaponController, (bool)objs[1], (bool)objs[2]);
                break;
        }
    }
}