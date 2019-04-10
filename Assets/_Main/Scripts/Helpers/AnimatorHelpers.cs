using UnityEngine;

public static class AnimatorHelpers {
    
    public static bool CheckState(this Animator actor, string stateName, string layerName = "Base Layer")
    {
        return actor.GetCurrentAnimatorStateInfo(actor.GetLayerIndex(layerName)).IsName(stateName);
    }

    public static bool CheckStateTag(this Animator actor,string stateTag, string layerName = "Base Layer")
    {
        return actor.GetCurrentAnimatorStateInfo(actor.GetLayerIndex(layerName)).IsTag(stateTag);
    }
}