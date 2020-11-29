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
    public bool baby;
    public int years = 0;
    public int months = 0;
    private float timeAlive = 0f;
    public int generation = 0;
    public List<GameObject> parents;
    public List<GameObject> mates;
    [HideInInspector] public int numChildren = 0;
    public List<GameObject> children;

    // Start is called before the first frame update
    void Start()
    {
        GameController game = this.GetComponent<GameController>();

        bored = false;
        breedable = false;
        baby = true;
        children = new List<GameObject>();      // we do not do this with parents because we initialize the parents already after spawning
        mates = new List<GameObject>();
    }

    void Update()
    {
        timeAlive += Time.deltaTime;

        years = (int) ((10 * timeAlive) / 365);
        months = (int) (((10 * timeAlive) % 365) / 30);

        if (months >= 10)
        {
            baby = false;
        }
    }

}
