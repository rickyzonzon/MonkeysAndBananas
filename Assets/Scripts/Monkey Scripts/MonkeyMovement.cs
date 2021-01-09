using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Pathfinding;

public class MonkeyMovement : MonoBehaviour
{
    private float targetingSpeed;
    private float wanderingSpeed;
    private Rigidbody2D myRigidbody;
    public Vector3 change;
    private Animator animator;
    private GameController game;
    private MonkeyGenes genes;
    private MonkeyStates state;
    private AIPath aiPath;
    private AIDestinationSetter track;
    public List<GameObject> unclimbableTrees;
    public float boredTimer = 0f;
    public float boredTime;
    private float wanderRange = 4f;
    private Transform wanderAI;
    private bool bump = false;
    private float bumpTimer = 0f;
    private Vector3 bumpDir = Vector3.zero;
    private float bumpMag = 0f;

    // Start is called before the first frame update
    void Start()
    {
        targetingSpeed = this.GetComponent<MonkeyGenes>().targetingSpeed;
        wanderingSpeed = this.GetComponent<MonkeyGenes>().wanderingSpeed;
        myRigidbody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        game = GameObject.Find("GameController").GetComponent<GameController>();
        genes = this.GetComponent<MonkeyGenes>();
        state = this.GetComponent<MonkeyStates>();
        aiPath = this.GetComponent<AIPath>();
        track = this.GetComponent<AIDestinationSetter>();
        unclimbableTrees = new List<GameObject>();

        boredTime = (((targetingSpeed - game.targetingSpeedBounds[0]) * (5f - 12f)) / (game.targetingSpeedBounds[1] - game.targetingSpeedBounds[0])) + 10f;
        wanderAI = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        boredTimer += Time.deltaTime;
        
        if (!state.bored)
        {
            if (boredTimer < boredTime)
            {
                if (track.target == wanderAI || track.target == null)
                {
                    FindTarget();
                    if (state._state == "Confused")
                    {
                        Wandering();
                        boredTimer = 0f;
                    }
                }
            }
            else
            {
                boredTimer = 0f;
                state.bored = true;
                this.GetComponent<MonkeySFX>().PlayHehe();
                Wandering();
            }
        }
        else
        {
            if (boredTimer >= game.treeSpawnFreq)
            {
                boredTimer = 0f;
                state.bored = false;
                Wandering();
            }
        }

        if (bump)
        {
            if (bumpTimer <= 4f)
            {
                bumpTimer += Time.deltaTime;
                myRigidbody.AddForce(bumpDir * bumpMag * (4f/bumpTimer), ForceMode2D.Force);
            }
            else
            {
                bumpTimer = 0f;
                bump = false;
            }

        }

        ChangeDirection();
        UpdateAnim();
    }

    void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.gameObject.tag == "monkey")
        {
            if (!hit.gameObject.GetComponent<MonkeyStates>().breedable || !state.breedable)
            {
                if (hit.gameObject.GetComponent<MonkeyGenes>().size > genes.size)
                {
                    bumpDir = (transform.position - hit.transform.position).normalized;
                    bumpMag = 5 * (hit.gameObject.GetComponent<MonkeyGenes>().size - genes.size);
                    bump = true;
                }
            }
        }
    }

    void OnTargetReached()
    {
        boredTimer = 0f;
        FindTarget();

        if (state._state == "Confused")
        {
            Wandering();
        }
    }

    void ChangeDirection()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            change.x = 1f;
        }
        else if (aiPath.desiredVelocity.x <= 0.01f)
        {
            change.x = -1f;
        }

        if (aiPath.desiredVelocity.y >= 0.01f)
        {
            change.y = 1f;
        }
        else if (aiPath.desiredVelocity.y <= 0.01f)
        {
            change.y = -1f;
        }
    }

    void Wandering()
    {
        state._state = "Confused";
        Vector3 newPos = Vector3.zero;
        Collider2D overlap = null;

        do
        {
            float randX = UnityEngine.Random.Range(transform.position.x - wanderRange, transform.position.x + wanderRange);
            float randY = UnityEngine.Random.Range(transform.position.y - wanderRange, transform.position.y + wanderRange);
            overlap = Physics2D.OverlapCircle(new Vector2(randX, randY), 0.5f);
            newPos = new Vector3(randX, randY, 0);
        }
        while (overlap != null || newPos.x < -20f || newPos.x > 20f ||
                newPos.y > 7.5f || newPos.y < -7.5f);

        wanderAI.position = newPos;
        track.target = wanderAI;
    }

    void UpdateAnim()
    {
        if (change != Vector3.zero)
        {
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        } 
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void FindTarget()
    {
        Collider2D[] locate = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), this.GetComponent<MonkeyGenes>().intelligence);

        List<Collider2D> objects = new List<Collider2D>(locate);

        if (state.breedable)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].gameObject == this.gameObject)
                {
                    objects.RemoveAt(i);
                    i--;
                }
                else if (objects[i].tag != "monkey" && objects[i].tag != "tree")
                {
                    objects.RemoveAt(i);
                    i--;
                }
                else if (unclimbableTrees.Contains(objects[i].gameObject))
                {
                    objects.RemoveAt(i);
                    i--;
                }
                else if (objects[i].tag == "monkey")
                {
                    if (!objects[i].gameObject.GetComponent<MonkeyStates>().breedable ||
                        objects[i].gameObject.GetComponent<MonkeyStates>().generation != state.generation)
                    {
                        objects.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (objects.Count > 0)
            {
                float shortestDist = float.MaxValue;
                int index = 0;

                for (int i = 0; i < objects.Count; i++)
                {
                    ColliderDistance2D dist = this.GetComponent<Collider2D>().Distance(objects[i]);
                    if (dist.distance < shortestDist)
                    {
                        shortestDist = dist.distance;
                        index = i;
                    }
                }

                if (game.timeOfExistence > 3)
                {
                    this.GetComponent<MonkeySFX>().PlayChirp();
                }
                track.target = objects[index].transform;
                aiPath.maxSpeed = targetingSpeed;
                state._state = "Targetting";
            }
            else
            {
                aiPath.maxSpeed = wanderingSpeed;
                state._state = "Confused";
            }
        }
        else
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].tag != "tree")
                {
                    objects.RemoveAt(i);
                    i--;
                }
                else if (unclimbableTrees.Contains(objects[i].gameObject))
                {
                    objects.RemoveAt(i);
                    i--;
                }
            }

            if (objects.Count > 0)
            {
                float shortestDist = float.MaxValue;
                int index = 0;

                for (int i = 0; i < objects.Count; i++)
                {
                    ColliderDistance2D dist = this.GetComponent<Collider2D>().Distance(objects[i]);
                    if (dist.distance < shortestDist)
                    {
                        shortestDist = dist.distance;
                        index = i;
                    }
                }

                if (game.timeOfExistence > 3)
                {
                    this.GetComponent<MonkeySFX>().PlayChirp();
                }
                track.target = objects[index].transform;
                aiPath.maxSpeed = targetingSpeed;
                state._state = "Targetting";
            }
            else
            {
                aiPath.maxSpeed = wanderingSpeed;
                state._state = "Confused";
            }
        }
    }
}
