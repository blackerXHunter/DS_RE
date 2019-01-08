using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformHelpers{
    public static Transform DeepFind(this Transform parent, string name) {
        
        foreach (Transform child in parent) {
            if (child.name == name) {
                return child;
            }
            else {
                Transform tempTransform = child.DeepFind(name);
                if (tempTransform != null) {
                    return tempTransform;
                }
            }
        }
        return null;
    }
}
