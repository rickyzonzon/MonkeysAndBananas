using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public int numCollidables = 0;
    public int numMonkeys = 0;
    public int numTrees = 0;

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
        Vector3 randPos = Vector3.zero;

        for (int i = 0; i < numMonkeys; i++)
        {
            float safetyNet = 0;

            do
            {
                if (safetyNet > 500)
                {
                    UnityEngine.Debug.Log("Not enough space for this monkey.");
                    break;
                }
                randPos.x = UnityEngine.Random.Range(-106f, 106f);
                randPos.y = UnityEngine.Random.Range(-56f, 56f);
                safetyNet++;
            }
            while (!game.SafeSpawn(randPos, "monkey"));

            game.currentMonkeys++;
            game.totalMonkeys++;
            game.objectPos.position = randPos;
            GameObject monkey = Instantiate(game.monkeyTemplate, game.objectPos.position, Quaternion.identity) as GameObject;
            monkey.name = "" + game.totalMonkeys;
        }
    }

    void GenerateTrees()
    {
        Vector3 randPos = Vector3.zero;

        for (int i = 0; i < numTrees; i++)
        {
            float safetyNet = 0;
            int randObj = UnityEngine.Random.Range(0, game.trees.Length);

            do
            {
                if (safetyNet > 500)
                {
                    UnityEngine.Debug.Log("Not enough space for this tree.");
                    break;
                }
                randPos.x = UnityEngine.Random.Range(-106f, 106f);
                randPos.y = UnityEngine.Random.Range(-56f, 56f);
                safetyNet++;
            }
            while (!game.SafeSpawn(randPos, "tree"));

            game.objectPos.position = randPos;
            GameObject tree = Instantiate(game.trees[randObj], game.objectPos.position, Quaternion.identity) as GameObject;
            UnityEngine.Debug.Log("New tree spawned.");
            game.currentTrees++;
            game.totalTrees++;
        }
    }

    void GenerateCollidables()
    {
        Vector3 randPos = Vector3.zero;

        for (int i = 0; i < numCollidables; i++)
        {
            float safetyNet = 0;
            int randObj = UnityEngine.Random.Range(0, game.collidables.Length);

            do
            {
                if (safetyNet > 500)
                {
                    UnityEngine.Debug.Log("Not enough space for this obstacle.");
                    break;
                }
                randPos.x = UnityEngine.Random.Range(-106f, 106f);
                randPos.y = UnityEngine.Random.Range(-56f, 56f);
                safetyNet++;
            }
            while (!game.SafeSpawn(randPos, "collidables"));

            game.objectPos.position = randPos;
            GameObject obstacle = Instantiate(game.collidables[randObj], game.objectPos.position, Quaternion.identity) as GameObject;
            UnityEngine.Debug.Log("New collidable spawned.");
        }
    }
}
