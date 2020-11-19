using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyGenes : MonoBehaviour
{
    private SpriteRenderer sprite;
    [HideInInspector] public float red;
    [HideInInspector] public float green;
    [HideInInspector] public float blue;
    public Color color;

    public string firstName;
    public string lastName;
    public float intelligence; // Detection radius
    public float speed;
    public int stamina;
    public int maxClimb;
    public int breedingThreshold;
    public int babyEnergy;

    // Start is called before the first frame update
    void Start()
    {
        MonkeyStates state = this.GetComponent<MonkeyStates>();
        GameController game = GameObject.Find("GameController").GetComponent<GameController>();

        firstName = game.firstNames[UnityEngine.Random.Range(0, game.firstNames.Length)];

        if (state.generation == 0)
        {
            lastName = game.lastNames[UnityEngine.Random.Range(0, game.lastNames.Length)];
            this.gameObject.name = firstName + " " + lastName + " (" + this.gameObject.name + ")";

            sprite = this.GetComponent<SpriteRenderer>();
            red = UnityEngine.Random.Range(0.35f, 1f);
            green = UnityEngine.Random.Range(0.35f, 1f);
            blue = UnityEngine.Random.Range(0.35f, 1f);
            sprite.color = new Color(red, green, blue, 1f);
            color = sprite.color;

            intelligence = UnityEngine.Random.Range(2.5f, 10.5f);
            speed = UnityEngine.Random.Range(0.5f, 4f);
            stamina = UnityEngine.Random.Range(1, 6);       // note: max is exclusive for int
            maxClimb = UnityEngine.Random.Range(1, 6);
            breedingThreshold = UnityEngine.Random.Range(80, 141);
            babyEnergy = UnityEngine.Random.Range(10, 51);
        }
        else
        {
            this.gameObject.name = firstName + " " + lastName + " (" + game.totalMonkeys + ")";
        }

        UnityEngine.Debug.Log("Monkey " + this.gameObject.name + " was born.");
    }

    public void Mutation()
    {
        int randGene = UnityEngine.Random.Range(0, 6);
        
        if (randGene == 0)
        {
            intelligence = UnityEngine.Random.Range(2.5f, 10.5f);
        }
        else if (randGene == 1)
        {
            speed = UnityEngine.Random.Range(0.5f, 4f);
        }
        else if (randGene == 2)
        {
            stamina = UnityEngine.Random.Range(1, 6);      
        }
        else if (randGene == 3)
        {
            maxClimb = UnityEngine.Random.Range(1, 6);
        }
        else if (randGene == 4)
        {
            breedingThreshold = UnityEngine.Random.Range(80, 141);
        }
        else
        {
            babyEnergy = UnityEngine.Random.Range(10, 51);
        }
    }
}
