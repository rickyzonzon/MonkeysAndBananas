using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int treeSpawnFreq = 5;
    public int[] treeSpawnBounds = { 1, 6 };
    public int treeEnergy = 10;
    public int maxTrees = 40;
    public int currentTrees = 0;
    public int totalTrees = 0;
    public float mutationProbability = 0.24f;
    public float energyLossRate = 4f;
    public int startingEnergy = 80;
    public int currentMonkeys = 0;
    public int totalMonkeys = 0;
    public int numMutations = 0;
    public int youngestGeneration = 0;
    private float timeOfExistence = 0;
    public int yearsOfExistence = 0;
    public int monthsOfExistence = 0;
    public bool extinction = false;

    public float[] colorBounds = { 0.35f, 1f };
    public float[] intelligenceBounds = { 2.5f, 9f }; // Detection radius
    public float[] targettingSpeedBounds = { 0.5f, 4.5f };
    public float[] wanderingSpeedBounds = { 1.5f, 5f };
    public int[] targettingStaminaBounds = { 2, 6 };
    public int[] wanderingStaminaBounds = { 2, 6 };
    public int[] maxClimbBounds = { 1, 6 };
    public int[] breedingThresholdBounds = { 80, 41 };
    public int[] babyEnergyBounds = { 10, 51 };

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
    public ParticleSystem[] particles;
    public GameObject[] trees;
    public GameObject[] collidables;

    public Transform objectPos;

    // Start is called before the first frame update
    void Start()
    {
        LevelGeneration levelGen = this.GetComponent<LevelGeneration>();
        levelGen.Generate();
        AstarPath.active.Scan();
        InvokeRepeating("SpawnTree", treeSpawnFreq, treeSpawnFreq);
    }

    void Update()
    {
        if (currentMonkeys == 0)
        {
            extinction = true;
        }

        if (extinction)
        {
            Time.timeScale = 0;
        }

        if (currentTrees < 0)
        {
            currentTrees = 0;
        }

        timeOfExistence += Time.deltaTime;
        yearsOfExistence = (int)((10 * timeOfExistence) / 365);
        monthsOfExistence = (int)(((10 * timeOfExistence) % 365) / 30);
    }

    void SpawnTree()
    {
        int randNum = UnityEngine.Random.Range(treeSpawnBounds[0], treeSpawnBounds[1]);
        for (int i = 0; i < randNum; i++)
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
                    randPos.z = -7.5f;
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
