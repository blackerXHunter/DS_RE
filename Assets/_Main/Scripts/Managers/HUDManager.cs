using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public ActorManager am;
    public PlayerPanel pp;
    private void Start(){

    }

    private void Update( ){
        if (am != null)
        {
            pp.personHealth.fillAmount = am.sm.HP/am.sm.HPMAX;
        }
    }
}
