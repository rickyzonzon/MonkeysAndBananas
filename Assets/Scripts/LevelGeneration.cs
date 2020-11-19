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
        Vector3 randPos = Vector3.zero;

        for (int i = 0; i < numMonkeys; i++)
        {
            float safetyNet = 0;

            do
            {
                if (safetyNet > 500)
                {
                    UnityEngine.Debug.Log("Too many monkeys.");
                    break;
                }
                randPos.x = UnityEngine.Random.Range(-20f, 20f);
                randPos.y = UnityEngine.Random.Range(-7.5f, 7.5f);
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
                    UnityEngine.Debug.Log("Too many trees.");
                    break;
                }
                randPos.x = UnityEngine.Random.Range(-20f, 20f);
                randPos.y = UnityEngine.Random.Range(-7.5f, 7.5f);
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
                    UnityEngine.Debug.Log("Too many obstacles.");
                    break;
                }
                randPos.x = UnityEngine.Random.Range(-20f, 20f);
                randPos.y = UnityEngine.Random.Range(-7.5f, 7.5f);
                safetyNet++;
            }
            while (!game.SafeSpawn(randPos, "collidables"));

            game.objectPos.position = randPos;
            GameObject obstacle = Instantiate(game.collidables[randObj], game.objectPos.position, Quaternion.identity) as GameObject;
            UnityEngine.Debug.Log("New collidable spawned.");
        }
    }
}
