using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Pathfinding;

public class MonkeyMovement : MonoBehaviour
{
    private float targettingSpeed;
    private float wanderingSpeed;
    private float magnifier = 20f;
    private Rigidbody2D myRigidbody;
    public Vector3 change;
    private Animator animator;

    private GameController game;
    private MonkeyStates state;
    private AIPath aiPath;
    private AIDestinationSetter track;
    public List<GameObject> unclimbableTrees;
    public float wanderTimer = 0f;
    public float boredTimer = 0f;
    private float wanderTime = 1f;
    private float boredTime = 6f;

    // Start is called before the first frame update
    void Start()
    {
        targettingSpeed = this.GetComponent<MonkeyGenes>().targettingSpeed;
        wanderingSpeed = this.GetComponent<MonkeyGenes>().wanderingSpeed;
        myRigidbody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        game = GameObject.Find("GameController").GetComponent<GameController>();
        state = this.GetComponent<MonkeyStates>();
        aiPath = this.GetComponent<AIPath>();
        track = this.GetComponent<AIDestinationSetter>();
        unclimbableTrees = new List<GameObject>();

        aiPath.maxSpeed = targettingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (state._state == "Confused")
        {
            wanderTimer += Time.deltaTime;

            if (state.bored)
            {
                boredTimer += Time.deltaTime;

                if (boredTimer < 3f)
                {
                    if (wanderTimer < wanderTime)
                    {
                        myRigidbody.AddForce((Vector2)change * wanderingSpeed * magnifier);
                    }
                    else
                    {
                        Wandering();
                        wanderTimer = 0f;
                    }
                }
                else
                {
                    state.bored = false;
                    boredTimer = 0f;
                    FindTarget();
                }
            }
            else
            {
                FindTarget();

                if (state._state == "Confused" && wanderTimer < wanderTime)
                {
                    myRigidbody.AddForce((Vector2)change * wanderingSpeed * magnifier);
                }
                else if (state._state == "Confused" && wanderTimer >= wanderTime)
                {
                    Wandering();
                    wanderTimer = 0f;
                }
            }
        }
        else
        {
            boredTimer += Time.deltaTime;

            if (boredTimer < boredTime)
            {
                Tracking();
            }
            else
            {
                track.target = null;
                state._state = "Confused";
                state.bored = true;
                boredTimer = 0f;
                Wandering();
            }
        }
        UpdateAnim();
    }

    void Tracking()
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
        float randX = UnityEngine.Random.Range(0f, 1f);
        float randY = UnityEngine.Random.Range(0f, 1f);

        if (myRigidbody.position.x > 16f)
        {
            if (randX > 0.25)
            {
                change.x = -1f;
            }
            else if (randX > 0.1)
            {
                change.x = 0f;
            }
            else
            {
                change.x = 1f;
            }
        }
        else if (myRigidbody.position.x < -16f)
        {
            if (randX > 0.25)
            {
                change.x = 1f;
            }
            else if (randX > 0.1)
            {
                change.x = 0f;
            }
            else
            {
                change.x = -1f;
            }
        }
        else
        {
            if (randX > 0.665)
            {
                change.x = 1f;
            }
            else if (randX > 0.335)
            {
                change.x = -1f;
            }
            else
            {
                change.x = 0;
            }
        }

        if (myRigidbody.position.y > 6.5f)
        {
            if (randY > 0.25)
            {
                change.y = -1f;
            }
            else if (randY > 0.1)
            {
                change.y = 0f;
            }
            else
            {
                change.y = 1f;
            }
        }
        else if (myRigidbody.position.y < -6.5f)
        {
            if (randY > 0.25)
            {
                change.y = 1f;
            }
            else if (randY > 0.1)
            {
                change.y = 0f;
            }
            else
            {
                change.y = -1f;
            }
        }
        else
        {
            if (randY > 0.665)
            {
                change.y = 1f;
            }
            else if (randY > 0.335)
            {
                change.y = -1f;
            }
            else
            {
                change.y = 0;
            }
        }
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

                track.target = objects[index].transform;
                state._state = "Targetting";
            }
            else
            {
                track.target = null;
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

                track.target = objects[index].transform;
                state._state = "Targetting";
            }
            else
            {
                track.target = null;
                state._state = "Confused";
            }
        }
    }
}
