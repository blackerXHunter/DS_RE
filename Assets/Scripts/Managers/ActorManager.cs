using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour {
    private ActorController ac;

    [Header("=== Auto Gen If Null ===")]
    private BattleManager bm;
    private WeaponManager wm;
    private StateManager sm;
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
        ac.IssueTrigger("damage");
        sm.AddHP(- 5);
        if (sm.IsDie) {
            ac.IssueTrigger("die");
        }
    }
}
