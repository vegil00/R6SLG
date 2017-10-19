using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemySolider :soldier {

    public bool scaned;
    public bool aimed;
    [HideInInspector]
    public enemyBehavior behavior;
    NavMeshAgent agent;
    scanRoute rangeScan;
    public float patrolSpeed;
	// Use this for initialization
	void Start () {
        scaned = false;
        behavior = GetComponent<enemyBehavior>();
        Moved = false;
        Attacked = false;
        aimed = false;
        head = transform.Find("Head");
        body = transform.Find("Body");
        leg = transform.Find("Leg");
        GameObject.Find("GameController").GetComponent<gameController>().enemyChList.Add(this);
        agent = GetComponent<NavMeshAgent>();
        rangeScan = GetComponent<scanRoute>();
    }
	
	// Update is called once per frame
	void Update () {
		if(Attacked||Moved)
        {
            GetComponent<LineRenderer>().positionCount = 0;
        }
      
    }
  
}
