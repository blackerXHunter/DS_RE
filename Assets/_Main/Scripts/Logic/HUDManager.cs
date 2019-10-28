using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public DS_RE.PlayerController playerController;
    public PlayerPanel pp;
    public EnemyPanel ep;
    public TipPanel tp;
    public TakingPanel takingPanel;

    private void Update()
    {
        if (pp.isActiveAndEnabled && playerController != null)
        {
            pp.personHealth.fillAmount = playerController.stateController.HP / playerController.stateController.HPMAX;
        }

        if (playerController != null)
        {
            var ac = playerController;
            if (ac.lockController.lockState == true)
            {
                var esm = ac.lockController.lockTarget.ac.stateController;
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
