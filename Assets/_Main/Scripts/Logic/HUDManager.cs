using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public ActorManager playerAm;
    public PlayerPanel pp;
    public EnemyPanel ep;
    public TipPanel tp;
    public TakingPanel takingPanel;

    private void Update()
    {
        if (pp.isActiveAndEnabled && playerAm != null)
        {
            pp.personHealth.fillAmount = playerAm.sm.HP / playerAm.sm.HPMAX;
        }

        if (playerAm != null)
        {
            var ac = playerAm.ac as PlayerAC;
            if (ac.camCtrl.lockState == true)
            {
                var esm = ac.camCtrl.lockTarget.am.sm;
                ep.gameObject.SetActive(true);
                ep.personHealth.fillAmount = esm.HP / esm.HPMAX;
            }
            else
            {
                ep.gameObject.SetActive(false);
            }
        }
        if (takingPanel.isActiveAndEnabled && GlobalManager.Instance.globalInput.defense)
        {
            takingPanel.gameObject.SetActive(false);
        }
    }
}
