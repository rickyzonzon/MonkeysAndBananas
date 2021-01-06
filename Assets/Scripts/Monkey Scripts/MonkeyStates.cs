using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonkeyStates : MonoBehaviour
{

    // public string[] allowedStates = { "Targetting", "Confused", "Deceased" }; 
    public string _state = "Confused";
    public bool bored = false;
    public bool breedable = false;
    public bool baby = true;
    public int years = 0;
    public int months = 0;
    private float timeAlive = 0f;
    public int generation = 0;
    public bool natural = true;
    public List<GameObject> parents;
    public List<GameObject> mates;
    [HideInInspector] public int numChildren = 0;
    public List<GameObject> children;
    private GameController game;
    private ParticleSystem heartParticles;
    private ParticleSystem boredParticles;

    // Start is called before the first frame update
    void Start()
    {
        game = this.GetComponent<GameController>();

        children = new List<GameObject>();      // we do not do this with parents because we initialize the parents already after spawning
        mates = new List<GameObject>();
        heartParticles = transform.GetChild(1).GetComponent<ParticleSystem>();
        boredParticles = transform.GetChild(2).GetComponent<ParticleSystem>();
    }

    void Update()
    {
        timeAlive += Time.deltaTime;

        years = (int) ((10 * timeAlive) / 365);
        months = (int) (((10 * timeAlive) % 365) / 30);

        var heartEmission = heartParticles.emission;
        var boredEmission = boredParticles.emission;

        if (baby)
        {
            
        }
        else
        {
            
        }

        if (months >= 10)
        {
            baby = false;
        }

        if (breedable)
        {
            heartEmission.enabled = true;
        }
        else
        {
            heartEmission.enabled = false;
        }

        if (bored)
        {
            boredEmission.enabled = true;
        }
        else
        {
            boredEmission.enabled = false;
        }
    }
}
