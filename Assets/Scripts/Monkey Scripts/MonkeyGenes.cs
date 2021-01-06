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
    public float size;
    public float targetingSpeed;
    public float wanderingSpeed;
    public int targetingStamina;
    public int wanderingStamina;
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

        if (state.generation == 0)
        {
            red = UnityEngine.Random.Range(game.colorBounds[0], game.colorBounds[1]);
            green = UnityEngine.Random.Range(game.colorBounds[0], game.colorBounds[1]);
            blue = UnityEngine.Random.Range(game.colorBounds[0], game.colorBounds[1]);

            if (state.natural)
            {
                firstName = game.firstNames[UnityEngine.Random.Range(0, game.firstNames.Length)];
                lastName = game.lastNames[UnityEngine.Random.Range(0, game.lastNames.Length)];
                this.gameObject.name = firstName + " " + lastName + " (" + this.gameObject.name + ")";

                intelligence = UnityEngine.Random.Range(game.intelligenceBounds[0], game.intelligenceBounds[1]);
                size = UnityEngine.Random.Range(game.sizeBounds[0], game.sizeBounds[1]);
                targetingSpeed = UnityEngine.Random.Range(game.targetingSpeedBounds[0], game.targetingSpeedBounds[1]);
                wanderingSpeed = UnityEngine.Random.Range(game.wanderingSpeedBounds[0], game.wanderingSpeedBounds[1]);
                targetingStamina = (int)System.Math.Round(((((intelligence / 2 + size + 2 * targetingSpeed) - (game.intelligenceBounds[0] / 2 + game.sizeBounds[0] + 2 * game.targetingSpeedBounds[0])) 
                    * (game.targetingStaminaBounds[1] - 1 - game.targetingStaminaBounds[0])) / (game.intelligenceBounds[1] / 2 + game.sizeBounds[1] + 2 * game.targetingSpeedBounds[1] 
                    - (game.intelligenceBounds[0] / 2 + game.sizeBounds[0] + 2 * game.targetingSpeedBounds[0]))) + game.targetingStaminaBounds[0]);
                wanderingStamina = (int)System.Math.Round(((((intelligence / 2 + size + 2 * wanderingSpeed) - (game.intelligenceBounds[0] / 2 + game.sizeBounds[0] + 2 * game.wanderingSpeedBounds[0]))
                    * (game.wanderingStaminaBounds[1] - 1 - game.wanderingStaminaBounds[0])) / (game.intelligenceBounds[1] / 2 + game.sizeBounds[1] + 2 * game.wanderingSpeedBounds[1]
                    - (game.intelligenceBounds[0] / 2 + game.sizeBounds[0] + 2 * game.wanderingSpeedBounds[0]))) + game.wanderingStaminaBounds[0]);
                maxClimb = UnityEngine.Random.Range(game.maxClimbBounds[0], game.maxClimbBounds[1]);
                breedingThreshold = UnityEngine.Random.Range(game.breedingThresholdBounds[0], game.breedingThresholdBounds[1]);
                babyEnergy = UnityEngine.Random.Range(game.babyEnergyBounds[0], game.babyEnergyBounds[1]);
            }
            else
            {
                this.gameObject.name = firstName + " " + lastName + " (" + game.totalMonkeys + ")";
            }
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
        transform.localScale = new Vector3(size, size, 1f);
        UnityEngine.Debug.Log("Monkey " + this.gameObject.name + " was born.");
    }

    public void Mutation()
    {
        game.numMutations++;
        int randGene = UnityEngine.Random.Range(0, 7);

        red = UnityEngine.Random.Range(game.colorBounds[0], game.colorBounds[1]);
        green = UnityEngine.Random.Range(game.colorBounds[0], game.colorBounds[1]);
        blue = UnityEngine.Random.Range(game.colorBounds[0], game.colorBounds[1]);

        mutated = true;

        if (randGene == 0)
        {
            intelligence = UnityEngine.Random.Range(game.intelligenceBounds[0], game.intelligenceBounds[1]);
            UnityEngine.Debug.Log(this.gameObject.name + " had an intelligence mutation.");
        }
        else if (randGene == 1)
        {
            size = UnityEngine.Random.Range(game.sizeBounds[0], game.sizeBounds[1]);
            UnityEngine.Debug.Log(this.gameObject.name + " had a size mutation.");
        }
        else if (randGene == 2)
        {
            targetingSpeed = UnityEngine.Random.Range(game.targetingSpeedBounds[0], game.targetingSpeedBounds[1]);
            UnityEngine.Debug.Log(this.gameObject.name + " had a targeting speed mutation.");
        }
        else if (randGene == 3)
        {
            wanderingSpeed = UnityEngine.Random.Range(game.wanderingSpeedBounds[0], game.wanderingSpeedBounds[1]);
            UnityEngine.Debug.Log(this.gameObject.name + " had a wandering speed mutation.");
        }
        else if (randGene == 4)
        {
            maxClimb = UnityEngine.Random.Range(game.maxClimbBounds[0], game.maxClimbBounds[1]);
            UnityEngine.Debug.Log(this.gameObject.name + " had a max climb mutation.");
        }
        else if (randGene == 5)
        {
            breedingThreshold = UnityEngine.Random.Range(game.breedingThresholdBounds[0], game.breedingThresholdBounds[1]);
            UnityEngine.Debug.Log(this.gameObject.name + " had a breeding threshold mutation.");
        }
        else
        {
            babyEnergy = UnityEngine.Random.Range(game.babyEnergyBounds[0], game.babyEnergyBounds[1]);
            UnityEngine.Debug.Log(this.gameObject.name + " had a baby energy mutation.");
        }
    }
}
