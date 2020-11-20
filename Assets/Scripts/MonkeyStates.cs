using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonkeyStates : MonoBehaviour
{

    // public string[] allowedStates = { "Targetting", "Confused", "Deceased" }; 
    public string _state;
    public bool breedable;
    public bool baby;
    public float timeAlive;
    public int generation = 0;
    public List<GameObject> parents;
    public int numChildren = 0;
    public List<GameObject> children;

    // Start is called before the first frame update
    void Start()
    {
        GameController game = this.GetComponent<GameController>();

        breedable = false;
        baby = true;
        timeAlive = 0f;
        children = new List<GameObject>();
    }

    void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive >= 15)
        {
            baby = false;
        }
    }

}
