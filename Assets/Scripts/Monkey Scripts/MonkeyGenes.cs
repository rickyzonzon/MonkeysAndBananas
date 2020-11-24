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
    public int targettingStamina;
    public float wanderingSpeed;
    public int wanderingStamina;
    public float size;
    [System.NonSerialized] public float energyDepletionNerf;
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

            red = UnityEngine.Random.Range(game.colorBounds[0], game.colorBounds[1]);
            green = UnityEngine.Random.Range(game.colorBounds[0], game.colorBounds[1]);
            blue = UnityEngine.Random.Range(game.colorBounds[0], game.colorBounds[1]);

            intelligence = UnityEngine.Random.Range(game.intelligenceBounds[0], game.intelligenceBounds[1]);
            targettingSpeed = UnityEngine.Random.Range(game.targettingSpeedBounds[0], game.targettingSpeedBounds[1]);
            targettingStamina = UnityEngine.Random.Range(game.targettingStaminaBounds[0], game.targettingStaminaBounds[1] + 1);
            wanderingSpeed = UnityEngine.Random.Range(game.wanderingSpeedBounds[0], game.wanderingSpeedBounds[1]);
            wanderingStamina = UnityEngine.Random.Range(game.wanderingStaminaBounds[0], game.wanderingStaminaBounds[1] + 1);
            size = UnityEngine.Random.Range(game.sizeBounds[0], game.sizeBounds[1]);
            maxClimb = UnityEngine.Random.Range(game.maxClimbBounds[0], game.maxClimbBounds[1] + 1);
            breedingThreshold = UnityEngine.Random.Range(game.breedingThresholdBounds[0], game.breedingThresholdBounds[1] + 1);
            babyEnergy = UnityEngine.Random.Range(game.babyEnergyBounds[0], game.babyEnergyBounds[1] + 1);
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

        transform.localScale = new Vector3(size, size, 1);
        float sizeSpeedBoost = (((size - game.sizeBounds[0]) * 1f) / (game.sizeBounds[1] - game.sizeBounds[0]));
        targettingSpeed += sizeSpeedBoost;
        wanderingSpeed += sizeSpeedBoost;
        energyDepletionNerf = ((size - game.sizeBounds[0]) * 2.5f) / (game.sizeBounds[1] - game.sizeBounds[0]) + 
            ((targettingSpeed - game.targettingSpeedBounds[0]) * 1.75f) / (game.sizeBounds[1] - game.sizeBounds[0]) +
            ((wanderingSpeed - game.wanderingSpeedBounds[0]) * 1.75f) / (game.sizeBounds[1] - game.sizeBounds[0]);

        UnityEngine.Debug.Log("Monkey " + this.gameObject.name + " was born.");
    }

    public void Mutation()
    {
        game.numMutations++;
        int randGene = UnityEngine.Random.Range(0, 9);

        red = UnityEngine.Random.Range(game.colorBounds[0], game.colorBounds[1]);
        green = UnityEngine.Random.Range(game.colorBounds[0], game.colorBounds[1]);
        blue = UnityEngine.Random.Range(game.colorBounds[0], game.colorBounds[1]);
        color = new Color(red, green, blue, 1f);
        sprite.color = color;

        mutated = true;

        if (randGene == 0)
        {
            intelligence = UnityEngine.Random.Range(game.intelligenceBounds[0], game.intelligenceBounds[1]);
            UnityEngine.Debug.Log(this.gameObject.name + " had an intelligence mutation.");
        }
        else if (randGene == 1)
        {
            targettingSpeed = UnityEngine.Random.Range(game.targettingSpeedBounds[0], game.targettingSpeedBounds[1]);
            UnityEngine.Debug.Log(this.gameObject.name + " had a targetting speed mutation.");
        }
        else if (randGene == 2)
        {
            targettingStamina = UnityEngine.Random.Range(game.targettingStaminaBounds[0], game.targettingStaminaBounds[1] + 1);
            UnityEngine.Debug.Log(this.gameObject.name + " had a targetting stamina mutation.");
        }
        else if (randGene == 3)
        {
            wanderingSpeed = UnityEngine.Random.Range(game.wanderingSpeedBounds[0], game.wanderingSpeedBounds[1]);
            UnityEngine.Debug.Log(this.gameObject.name + " had a wandering speed mutation.");
        }
        else if (randGene == 4)
        {
            wanderingStamina = UnityEngine.Random.Range(game.wanderingStaminaBounds[0], game.wanderingStaminaBounds[1] + 1);
            UnityEngine.Debug.Log(this.gameObject.name + " had a wandering stamina mutation.");
        }
        else if (randGene == 5)
        {
            size = UnityEngine.Random.Range(game.sizeBounds[0], game.sizeBounds[1]);
            UnityEngine.Debug.Log(this.gameObject.name + " had a size mutation.");
        }
        else if (randGene == 6)
        {
            maxClimb = UnityEngine.Random.Range(game.maxClimbBounds[0], game.maxClimbBounds[1] + 1);
            UnityEngine.Debug.Log(this.gameObject.name + " had a max climb mutation.");
        }
        else if (randGene == 7)
        {
            breedingThreshold = UnityEngine.Random.Range(game.breedingThresholdBounds[0], game.breedingThresholdBounds[1] + 1);
            UnityEngine.Debug.Log(this.gameObject.name + " had a breeding threshold mutation.");
        }
        else
        {
            babyEnergy = UnityEngine.Random.Range(game.babyEnergyBounds[0], game.babyEnergyBounds[1] + 1);
            UnityEngine.Debug.Log(this.gameObject.name + " had a baby energy mutation.");
        }
    }
}
