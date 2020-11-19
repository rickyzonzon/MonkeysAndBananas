using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Pathfinding;

public class MonkeyMovement : MonoBehaviour
{
    private float speed;
    private float magnifier = 10f;
    private Rigidbody2D myRigidbody;
    public Vector3 change;
    private Animator animator;
    private MonkeyStates state;
    private AIPath aiPath;
    private AIDestinationSetter track;
    public List<GameObject> unclimbableTrees;
    public float timer = 0f;
    private float waitWander = 1f;
    private float waitConfused = 3f;

    // Start is called before the first frame update
    void Start()
    {
        speed = this.GetComponent<MonkeyGenes>().speed;
        myRigidbody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        state = this.GetComponent<MonkeyStates>();
        aiPath = this.GetComponent<AIPath>();
        track = this.GetComponent<AIDestinationSetter>();
        unclimbableTrees = new List<GameObject>();

        aiPath.maxSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
       // if (state._state == "Confused" || state._state == "LookingToMate")
        if (track.target == null)
        {
            FindTarget();
           // if (state._state == "Confused" || state._state == "LookingToMate")
            if (track.target == null && timer >= waitWander)
            {
                Wandering();
                timer = 0f;
            }
            else if (track.target == null && timer < waitWander)
            {
                myRigidbody.AddForce((Vector2)change * speed * magnifier);
            }
        }
        else
        {
            if (timer >= waitConfused)
            {
                track.target = null;
                Wandering();
                timer = 0f;
            }

            Tracking();
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
                if (objects[i].tag != "monkey" && objects[i].tag != "tree")
                {
                    objects.RemoveAt(i);
                    i--;
                }
                else if (objects[i].tag == "monkey")
                {
                    if (!objects[i].gameObject.GetComponent<MonkeyStates>().breedable)
                    {
                        objects.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (objects.Count > 0)
            {
                int randObj = UnityEngine.Random.Range(0, objects.Count);
                if (unclimbableTrees.Contains(objects[randObj].gameObject))
                {
                    track.target = null;
                    state._state = "Confused";
                }
                else
                {
                    track.target = objects[randObj].transform;
                    state._state = "Targetting";
                }
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
            }

            if (objects.Count > 0)
            {
                int randObj = UnityEngine.Random.Range(0, objects.Count);
                if (unclimbableTrees.Contains(objects[randObj].gameObject))
                {
                    track.target = null;
                    state._state = "Confused";
                }
                else
                {
                    track.target = objects[randObj].transform;
                    state._state = "Targetting";
                }
            }
            else
            {
                track.target = null;
                state._state = "Confused";
            }
        }
    }
}
