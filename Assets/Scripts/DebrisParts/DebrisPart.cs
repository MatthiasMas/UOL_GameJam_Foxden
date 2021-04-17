using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Debris Part", menuName = "Debris")]
public class DebrisPart : ScriptableObject
{
    public string partName;
    public Sprite sprite;

    public float minSpawnDistance;
    public float maxSpawnDistance;
    public float distanceBeforeNextSpawn;
}
