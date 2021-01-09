using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public int numCollidables = 20;
    public int numMonkeys = 15;
    public int numTrees = 7;

    private GameController game;

    public void Generate()
    {
        game = this.GetComponent<GameController>();

        GenerateCollidables();
        GenerateTrees();
        GenerateMonkeys();
    }

    void GenerateMonkeys()
    {
        for (int i = 0; i < numMonkeys; i++)
        {
            float safetyNet = 0;
            Vector3 randPos = Vector3.zero;

            do
            {
                if (safetyNet > 500)
                {
                    UnityEngine.Debug.Log("Too many monkeys.");
                    break;
                }
                randPos.x = UnityEngine.Random.Range(-20f, 20f);
                randPos.y = UnityEngine.Random.Range(-7.5f, 7.5f);
                randPos.z = -10f;
                safetyNet++;
            }
            while (!game.SafeSpawn(randPos, "monkey"));

            game.SpawnMonkey(randPos);
        }
    }

    void GenerateTrees()
    {
        for (int i = 0; i < numTrees; i++)
        {
            game.SpawnTrees();
            UnityEngine.Debug.Log("New tree spawned.");
        }
    }

    void GenerateCollidables()
    {
        for (int i = 0; i < numCollidables; i++)
        {
            float safetyNet = 0;
            int randObj = UnityEngine.Random.Range(0, game.collidables.Length);
            Vector3 randPos = Vector3.zero;

            do
            {
                if (safetyNet > 500)
                {
                    UnityEngine.Debug.Log("Too many obstacles.");
                    break;
                }
                randPos.x = UnityEngine.Random.Range(-20f, 20f);
                randPos.y = UnityEngine.Random.Range(-7.5f, 7.5f);
                randPos.z = -5f;
                safetyNet++;
            }
            while (!game.SafeSpawn(randPos, "collidables"));

            game.objectPos.position = randPos;
            game.SpawnObject(game.objectPos.position, randObj);
            UnityEngine.Debug.Log("New collidable spawned.");
        }
    }
}
