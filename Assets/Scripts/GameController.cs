using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int treeSpawnFreq = 3;
    public int maxTrees = 40;
    public float mutationProbability = 0.18f;
    public int currentTrees = 0;
    public int totalTrees = 0;
    public int currentMonkeys = 0;
    public int totalMonkeys = 0;
    public bool extinction = false;
    public int timeOfExistence = 0;
    public int numMutations = 0;
    public int youngestGeneration = 0;

    [System.NonSerialized]
    public string[] firstNames = { "Abu", "Aldo", "Amy", "Andross", "Ari", "Bingo", "Babo", "Bobo", "Bonzo",
                                  "Clements", "Clyde", "Crystal", "Dodger", "Dunston", "Ed", "Grape", "George",
                                  "Ham", "Jack", "Joe", "Lazlo", "Loui", "Kassim", "Raffles", "Rafiki", "Peaches",
                                  "Spike", "Suzzane", "Sydney", "Treelo", "Virgil", "Yono", "Albert", "Bear",
                                  "Bing", "Charlie", "Chester", "Chucky", "Edward", "Flunkey", "Freedo", "Hector",
                                  "Hanky", "Jack", "Jasper", "Leo", "Max", "Mike", "Ricky", "Ned", "Pilo", "Titano",
                                  "Alene", "Anie", "Anna", "April", "Bibi", "Calli", "Ira", "Isis", "Kiki", "Kila",
                                  "Koko", "Kye", "Lazy", "Liz", "Lolly", "Maggie", "Merry", "Ania", "Molly", "Nicky",
                                  "Oli", "Rose", "Sheila", "Star", "Suri", "Wink", "Zini", "Chip", "Lolo", "Mini",
                                  "Rio", "Nim", "King", "Zuzu", "Juju" };
    [System.NonSerialized]
    public string[] lastNames = { "Curious", "Caesar", "Bubbles", "Kong", "Beans", "Ape", "Cheeks", "Congo", "Sun",
                                 "Bing Bong", "Yum Yum", "Hopper", "Jelly", "Sugar", "Jaffa", "Crunch", "Butter",
                                 "Banana", "Marbles", "Chiffon", "Marzipan", "Raisin", "Chunk", "Mentos", "Nectar",
                                 "Duck", "Pez", "Brownie", "Mustard", "Scrappy", "Wiggles", "Tango", "Jabba",
                                 "Monkey", "Bunny", "Buffalo", "Mandarin", "Wobble", "Rider", "Stone", "Rock", "Steel",
                                 "Munch", "Cheesy", "Blue", "Red", "Silver", "Orange", "Green", "Popsicle", "Grass" };

    public GameObject monkeyTemplate;
    public GameObject[] trees;
    public GameObject[] collidables;

    public Transform objectPos;

    // Start is called before the first frame update
    void Start()
    {
        LevelGeneration levelGen = this.GetComponent<LevelGeneration>();
        levelGen.Generate();

        InvokeRepeating("SpawnTree", treeSpawnFreq, treeSpawnFreq);
        InvokeRepeating("UpdateScan", 0f, treeSpawnFreq / 2);
    }

    void Update()
    {
        if (currentMonkeys == 0)
        {
            extinction = true;
        }

        timeOfExistence = (int)Time.time;

        if (extinction)
        {
            Time.timeScale = 0;
        }

        if (currentTrees < 0)
        {
            currentTrees = 0;
        }
    }

    void UpdateScan()
    {
        AstarPath.active.Scan();
    }

    void SpawnTree()
    {
        for (int i = 0; i < 3; i++)
        {
            if (maxTrees >= currentTrees)
            {
                float safetyNet = 0;
                int randObj = UnityEngine.Random.Range(0, trees.Length);
                Vector3 randPos = Vector3.zero;

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
                while (!SafeSpawn(randPos, "tree"));

                objectPos.position = randPos;
                GameObject tree = Instantiate(trees[randObj], objectPos.position, Quaternion.identity) as GameObject;
                currentTrees++;
                totalTrees++;
            }
        }
    }

    public bool SafeSpawn(Vector3 pos, String type)
    {
        Collider2D overlap = null;

        if (type == "tree" || type == "monkey")
        {
            overlap = Physics2D.OverlapCircle(new Vector2(pos.x, pos.y), 1);
        } else
        {
            overlap = Physics2D.OverlapCircle(new Vector2(pos.x, pos.y), 2);
        }

        if (overlap == null)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
