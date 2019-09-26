using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "scene configuration", menuName = "Configurations/Scene")]
public class SceneConfiguration : ScriptableObject
{
    public Vector3 playerPos, enemyPos,boxPos,leverPos;
}
