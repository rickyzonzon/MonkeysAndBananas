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
        // if collides with tree, then EatBanana
        // return;
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
                energy += game.treeEnergy;
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
            // currently testing
            if (hit.transform.gameObject.GetComponent<MonkeyStates>().generation == state.generation)
            {
                if (GetInstanceID() > hit.transform.gameObject.GetInstanceID())
                {
                    GameObject parent = hit.transform.gameObject;
                    MonkeyGenes parentGenes = parent.GetComponent<MonkeyGenes>();

                    if (state.breedable && parent.GetComponent<MonkeyStates>().breedable)
                    {
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
                        GameObject baby = Instantiate(game.monkeyTemplate, babyPos, Quaternion.identity) as GameObject;
                        MonkeyGenes babyGenes = baby.GetComponent<MonkeyGenes>();
                        game.currentMonkeys++;
                        game.totalMonkeys++;

                        // add wanderAI to children
                        GameObject wander = new GameObject("wanderAI");
                        wander.transform.parent = baby.transform;

                        // add particle system to children
                        ParticleSystem hearts = Instantiate(game.particles[0], baby.transform.position, Quaternion.identity);
                        ParticleSystem bored = Instantiate(game.particles[1], baby.transform.position, Quaternion.identity);
                        hearts.transform.parent = baby.transform;
                        bored.transform.parent = baby.transform;

                        // pass on last name to baby
                        babyGenes.lastName = genes.lastName;

                        // parent energies transfer to baby
                        baby.GetComponent<MonkeyEnergy>().energy = genes.babyEnergy + parentGenes.babyEnergy;
                        parent.GetComponent<MonkeyEnergy>().energy -= parentGenes.babyEnergy;
                        energy -= genes.babyEnergy;

                        // increase generation by 1, from whichever parent is younger, update youngestGeneration
                        if (state.generation > parent.GetComponent<MonkeyStates>().generation)
                        {
                            baby.GetComponent<MonkeyStates>().generation = state.generation + 1;
                        }
                        else
                        {
                            baby.GetComponent<MonkeyStates>().generation = parent.GetComponent<MonkeyStates>().generation + 1;
                        }

                        if (baby.GetComponent<MonkeyStates>().generation > game.youngestGeneration)
                        {
                            game.youngestGeneration = baby.GetComponent<MonkeyStates>().generation;
                        }


                        // baby color gene inherited from parents
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

                        // inheriting targetting speed
                        randGene = UnityEngine.Random.Range(0, 2);
                        if (randGene == 0)
                        {
                            babyGenes.targettingSpeed = genes.targettingSpeed;
                        }
                        else
                        {
                            babyGenes.targettingSpeed = parentGenes.targettingSpeed;
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

                        // inheriting targetting stamina
                        randGene = UnityEngine.Random.Range(0, 2);
                        if (randGene == 0)
                        {
                            babyGenes.targettingStamina = genes.targettingStamina;
                        }
                        else
                        {
                            babyGenes.targettingStamina = parentGenes.targettingStamina;
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

                        // inheriting breedhing threshold
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
                    }
                }
            }
        }
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
            energy -= genes.targettingStamina;
        }
        else
        {
            energy -= genes.wanderingStamina;
        }
    }
}
