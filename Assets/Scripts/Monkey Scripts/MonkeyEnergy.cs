using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonkeyEnergy : MonoBehaviour
{
    private MonkeyGenes genes;
    private MonkeyStates state;
    private MonkeyMovement movement;
    private AIDestinationSetter track;
    private GameController game;
    public int energy;

    // Start is called before the first frame update
    void Start()
    {
        genes = this.GetComponent<MonkeyGenes>();
        state = this.GetComponent<MonkeyStates>();
        movement = this.GetComponent<MonkeyMovement>();
        track = this.GetComponent<AIDestinationSetter>();
        game = GameObject.Find("GameController").GetComponent<GameController>();
        if (state.generation == 0)
        {
            energy = game.startingEnergy;
        }

        InvokeRepeating("EnergyLoss", game.energyLossRate, game.energyLossRate);
    }
    
    void Update()
    {
        if (energy <= 0)
        {
            state._state = "Deceased";
            MonkeyDie();
            return;
        }

        if (!state.baby)
        {
            if (energy > genes.breedingThreshold)
            {
                state.breedable = true;
                this.GetComponent<MonkeySFX>().PlayHehe();
            }
            else
            {
                state.breedable = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D hit)
    {
        // eat banana
        if (hit.transform.gameObject.tag == "tree")
        {
            GameObject tree = hit.transform.gameObject;

            if (genes.maxClimb >= tree.GetComponent<TreeController>().height)
            {
                energy += tree.GetComponent<TreeController>().energy;
                Destroy(tree);
                game.currentTrees--;
                movement.boredTimer = 0f;
            }
            else
            {
                movement.unclimbableTrees.Add(tree);
                track.target = null;
                state._state = "Confused";
                movement.boredTimer = 0f;
            }
        }
        // breed
        else if (hit.transform.gameObject.tag == "monkey")
        {
            if (hit.transform.gameObject.GetComponent<MonkeyStates>().generation == state.generation)
            {
                if (GetInstanceID() > hit.transform.gameObject.GetInstanceID())
                {
                    GameObject parent = hit.transform.gameObject;

                    if (state.breedable && parent.GetComponent<MonkeyStates>().breedable)
                    {
                        Breed(parent);
                    }
                }
            }
        }
    }

    GameObject Breed(GameObject parent)
    {
        this.GetComponent<MonkeySFX>().PlayHehe();
        MonkeyGenes parentGenes = parent.GetComponent<MonkeyGenes>();

        // add other monkey to mates
        bool alreadyMated = false;
        foreach (GameObject monk in state.mates)
        {
            if (monk == parent)
            {
                alreadyMated = true;
                break;
            }
        }
        if (!alreadyMated)
        {
            state.mates.Add(parent);
            parent.GetComponent<MonkeyStates>().mates.Add(this.gameObject);
        }

        // spawn the baby
        Vector3 babyPos = new Vector3(parent.transform.position.x, parent.transform.position.y - 1, parent.transform.position.z);
        GameObject baby = game.SpawnMonkey(babyPos);
        MonkeyGenes babyGenes = baby.GetComponent<MonkeyGenes>();

        // pass on last name to baby
        babyGenes.lastName = genes.lastName;

        // parent energies transfer to baby
        baby.GetComponent<MonkeyEnergy>().energy = genes.babyEnergy + parentGenes.babyEnergy;
        parent.GetComponent<MonkeyEnergy>().energy -= parentGenes.babyEnergy;
        energy -= genes.babyEnergy;

        // increase baby generation by 1, change youngestGeneration if necessary
        baby.GetComponent<MonkeyStates>().generation = state.generation + 1;

        if (baby.GetComponent<MonkeyStates>().generation > game.youngestGeneration)
        {
            game.youngestGeneration = baby.GetComponent<MonkeyStates>().generation;
        }

        // baby color genes inherited from parents
        babyGenes.red = genes.red;
        babyGenes.green = parentGenes.green;
        babyGenes.blue = genes.blue;

        // inheriting intelligence
        int randGene = UnityEngine.Random.Range(0, 2);
        if (randGene == 0)
        {
            babyGenes.intelligence = genes.intelligence;
        }
        else
        {
            babyGenes.intelligence = parentGenes.intelligence;
        }

        // inheriting size
        randGene = UnityEngine.Random.Range(0, 2);
        if (randGene == 0)
        {
            babyGenes.size = genes.size;
        }
        else
        {
            babyGenes.size = parentGenes.size;
        }

        // inheriting targeting speed
        randGene = UnityEngine.Random.Range(0, 2);
        if (randGene == 0)
        {
            babyGenes.targetingSpeed = genes.targetingSpeed;
        }
        else
        {
            babyGenes.targetingSpeed = parentGenes.targetingSpeed;
        }

        // inheriting wandering speed
        randGene = UnityEngine.Random.Range(0, 2);
        if (randGene == 0)
        {
            babyGenes.wanderingSpeed = genes.wanderingSpeed;
        }
        else
        {
            babyGenes.wanderingSpeed = parentGenes.wanderingSpeed;
        }

        // inheriting targeting stamina
        randGene = UnityEngine.Random.Range(0, 2);
        if (randGene == 0)
        {
            babyGenes.targetingStamina = genes.targetingStamina;
        }
        else
        {
            babyGenes.targetingStamina = parentGenes.targetingStamina;
        }

        // inheriting wandering stamina
        randGene = UnityEngine.Random.Range(0, 2);
        if (randGene == 0)
        {
            babyGenes.wanderingStamina = genes.wanderingStamina;
        }
        else
        {
            babyGenes.wanderingStamina = parentGenes.wanderingStamina;
        }

        // inheriting max climb
        randGene = UnityEngine.Random.Range(0, 2);
        if (randGene == 0)
        {
            babyGenes.maxClimb = genes.maxClimb;
        }
        else
        {
            babyGenes.maxClimb = parentGenes.maxClimb;
        }

        // inheriting breeding threshold
        randGene = UnityEngine.Random.Range(0, 2);
        if (randGene == 0)
        {
            babyGenes.breedingThreshold = genes.breedingThreshold;
        }
        else
        {
            babyGenes.breedingThreshold = parentGenes.breedingThreshold;
        }

        // inheriting baby energy passover
        randGene = UnityEngine.Random.Range(0, 2);
        if (randGene == 0)
        {
            babyGenes.babyEnergy = genes.babyEnergy;
        }
        else
        {
            babyGenes.babyEnergy = parentGenes.babyEnergy;
        }

        // add child to children of both parents
        state.children.Add(baby);
        parent.GetComponent<MonkeyStates>().children.Add(baby);
        state.numChildren++;
        parent.GetComponent<MonkeyStates>().numChildren++;

        // add parents to parents
        baby.GetComponent<MonkeyStates>().parents.Add(parent);
        baby.GetComponent<MonkeyStates>().parents.Add(this.gameObject);

        return baby;
    }

    GameObject MonkeyDie()
    {
        GameObject monkey = this.gameObject;
        Destroy(gameObject);
        UnityEngine.Debug.Log("Monkey " + monkey.name + " died.");
        game.currentMonkeys--;

        return monkey;
    }

    void EnergyLoss()
    {
        if (state._state == "Targetting")
        {
            energy -= genes.targetingStamina;
        }
        else
        {
            energy -= genes.wanderingStamina;
        }
    }
}
