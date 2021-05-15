using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Level")]
public class Level : ScriptableObject
{
    public string Name;
    public Material Skybox;
    public int EnemyNumber;
    //public Color EnemyColor;

    [Space]
    [Space]
    public float minEnemySpeed;
    public float maxEnemySpeed;
}