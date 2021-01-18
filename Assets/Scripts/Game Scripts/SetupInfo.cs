using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupInfo : MonoBehaviour
{
    [HideInInspector] public int treeSpawnFreq;
    [HideInInspector] public int[] treeSpawnBounds;
    [HideInInspector] public int maxTrees;
    [HideInInspector] public int startingTrees;
    [HideInInspector] public int startingMonkeys;
    [HideInInspector] public int startingObjects;
    [HideInInspector] public float mutationProbability;
    [HideInInspector] public int energyLossRate;
    [HideInInspector] public int startingEnergy;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
