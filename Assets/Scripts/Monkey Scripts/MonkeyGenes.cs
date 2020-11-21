using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyGenes : MonoBehaviour
{
    [HideInInspector] public SpriteRenderer sprite;
    [HideInInspector] public float red;
    [HideInInspector] public float green;
    [HideInInspector] public float blue;
    public Color color;

    public string firstName;
    public string lastName;
    public bool mutated = false;
    public float intelligence; // Detection radius
    public float targettingSpeed;
    public float wanderingSpeed;
    public int stamina;
    public int maxClimb;
    public int breedingThreshold;
    public int babyEnergy;

    private MonkeyStates state;
    private GameController game;

    // Start is called before the first frame update
    void Start()
    {
        state = this.GetComponent<MonkeyStates>();
        game = GameObject.Find("GameController").GetComponent<GameController>();
        
        sprite = this.GetComponent<SpriteRenderer>();

        firstName = game.firstNames[UnityEngine.Random.Range(0, game.firstNames.Length)];

        if (state.generation == 0)
        {
            lastName = game.lastNames[UnityEngine.Random.Range(0, game.lastNames.Length)];
            this.gameObject.name = firstName + " " + lastName + " (" + this.gameObject.name + ")";

            red = UnityEngine.Random.Range(0.35f, 1f);
            green = UnityEngine.Random.Range(0.35f, 1f);
            blue = UnityEngine.Random.Range(0.35f, 1f);

            intelligence = UnityEngine.Random.Range(2.5f, 9f);
            targettingSpeed = UnityEngine.Random.Range(0.5f, 4.5f);
            wanderingSpeed = UnityEngine.Random.Range(1.5f, 5f);
            stamina = UnityEngine.Random.Range(2, 5);       // note: max is exclusive for int
            maxClimb = UnityEngine.Random.Range(1, 6);
            breedingThreshold = UnityEngine.Random.Range(80, 141);
            babyEnergy = UnityEngine.Random.Range(10, 51);
        }
        else
        {
            this.gameObject.name = firstName + " " + lastName + " (" + game.totalMonkeys + ")";

            float mutation = UnityEngine.Random.Range(0f, 1f);
            if (mutation <= game.mutationProbability)
            {
                Mutation();
            }
        }

        color = new Color(red, green, blue, 1f);
        sprite.color = color;
        UnityEngine.Debug.Log("Monkey " + this.gameObject.name + " was born.");
    }

    public void Mutation()
    {
        game.numMutations++;
        int randGene = UnityEngine.Random.Range(0, 7);
        red = UnityEngine.Random.Range(0.35f, 1f);
        green = UnityEngine.Random.Range(0.35f, 1f);
        blue = UnityEngine.Random.Range(0.35f, 1f);
        color = new Color(red, green, blue, 1f);
        sprite.color = color;

        mutated = true;

        if (randGene == 0)
        {
            intelligence = UnityEngine.Random.Range(2.5f, 10.5f);
            UnityEngine.Debug.Log(this.gameObject.name + " had an intelligence mutation.");
        }
        else if (randGene == 1)
        {
            targettingSpeed = UnityEngine.Random.Range(0.5f, 4.5f);
            UnityEngine.Debug.Log(this.gameObject.name + " had a targetting speed mutation.");
        }
        else if (randGene == 2)
        {
            wanderingSpeed = UnityEngine.Random.Range(1.5f, 5f);
            UnityEngine.Debug.Log(this.gameObject.name + " had a wandering speed mutation.");
        }
        else if (randGene == 3)
        {
            stamina = UnityEngine.Random.Range(2, 5);
            UnityEngine.Debug.Log(this.gameObject.name + " had a stamina mutation.");
        }
        else if (randGene == 4)
        {
            maxClimb = UnityEngine.Random.Range(1, 6);
            UnityEngine.Debug.Log(this.gameObject.name + " had a max climb mutation.");
        }
        else if (randGene == 5)
        {
            breedingThreshold = UnityEngine.Random.Range(80, 141);
            UnityEngine.Debug.Log(this.gameObject.name + " had a breeding threshold mutation.");
        }
        else
        {
            babyEnergy = UnityEngine.Random.Range(10, 51);
            UnityEngine.Debug.Log(this.gameObject.name + " had a baby energy mutation.");
        }
    }
}
