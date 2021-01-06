using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool paused = false;
    public int treeSpawnFreq = 5;
    public int[] treeSpawnBounds = { 1, 4 };
    public int maxTrees = 40;
    public int currentTrees = 0;
    public int totalTrees = 0;
    public float mutationProbability = 0.27f;
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
    public float[] intelligenceBounds = { 2f, 7f }; // Detection radius
    public float[] sizeBounds = { 0.65f, 1.5f };
    public float[] targetingSpeedBounds = { 0.5f, 3.5f };
    public float[] wanderingSpeedBounds = { 1f, 4f };
    public int[] targetingStaminaBounds = { 1, 6 };
    public int[] wanderingStaminaBounds = { 1, 6 };
    public int[] maxClimbBounds = { 1, 6 };
    public int[] breedingThresholdBounds = { 70, 161 };
    public int[] babyEnergyBounds = { 10, 61 };

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
                                 "Bing-Bong", "Yum-Yum", "Hopper", "Jelly", "Sugar", "Jaffa", "Crunch", "Butter",
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
            // TODO
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

    public GameObject SpawnTree()
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

                return tree;
            }
        }

        return null;
    }

    public GameObject SpawnTree(Vector3 pos, int height)
    {
        GameObject tree = Instantiate(trees[height - 1], pos, Quaternion.identity) as GameObject;
        currentTrees++;
        totalTrees++;

        return tree;
    }

    public GameObject SpawnMonkey(Vector3 pos)
    {
        currentMonkeys++;
        totalMonkeys++;
        objectPos.position = pos;
        GameObject monkey = Instantiate(monkeyTemplate, pos, Quaternion.identity) as GameObject;
        monkey.name = "" + totalMonkeys;

        GameObject wander = new GameObject("wanderAI");
        ParticleSystem hearts = Instantiate(particles[0], monkey.transform.position, Quaternion.identity);
        ParticleSystem bored = Instantiate(particles[1], monkey.transform.position, Quaternion.identity);

        wander.transform.parent = monkey.transform;
        hearts.transform.parent = monkey.transform;
        bored.transform.parent = monkey.transform;

        return monkey;
    }

    public bool SafeSpawn(Vector3 pos, String type)
    {
        Collider2D overlap = null;

        if (type == "tree" || type == "monkey")
        {
            overlap = Physics2D.OverlapCircle(new Vector2(pos.x, pos.y), 0.5f);
        } else
        {
            overlap = Physics2D.OverlapCircle(new Vector2(pos.x, pos.y), 1f);
        }

        if (overlap == null)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
