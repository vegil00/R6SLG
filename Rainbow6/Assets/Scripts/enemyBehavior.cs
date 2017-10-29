using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyBehavior : MonoBehaviour {
    [HideInInspector]
  public  Dictionary<playerSolider, int> targetList;
    [HideInInspector]
    public playerSolider curTarget;
    //Transform head;
    //Transform body;
    //Transform leg;
    enemySolider character;
    scanRoute rangeScan;
    public Transform[] patrolPoints;
    int curPatrolIndex;
    public bool bPartol;
    public bool bChase;
    NavMeshAgent agent;
    Vector3 destination;
    // Use this for initialization
    void Start () {
        character = GetComponent<enemySolider>();
         targetList = new Dictionary<playerSolider, int>();
        rangeScan = GetComponent<scanRoute>();
        curPatrolIndex = 0;
        bPartol = true;
        agent = GetComponent<NavMeshAgent>();
        destination = Vector3.zero;
        for(int i=0;i<patrolPoints.Length;i++)
        {
            patrolPoints[i].position = character.allTiles.getTile(patrolPoints[i].position).transform.position;
        }
         //head =character.head;
         //body = character.body;
         //leg =character.leg;
    }
	
	// Update is called once per frame
	void Update () {
		if(curTarget!=null&&!character.aimed)
        {
            character.aimed = true;
            GetComponent<SoilderAnimation>().aimAt(curTarget.transform);
        }
        if (agent.remainingDistance < agent.stoppingDistance && agent.speed > 0)
        {
            // agent.speed = 0;
            character.Moved = true;
            GetComponent<SoilderAnimation>().targetSpeed = 0;
            destination = Vector3.zero;
            transform.position = character.allTiles.getTile(transform.position).transform.position;
            if (targetList.Count == 0)
            {
                character.Attacked = true;
            }
            if (bPartol)
            {
                nextPoint();
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.transform.GetComponent<playerSolider>())
        {
            playerSolider playerCh = other.transform.GetComponent<playerSolider>();

            RaycastHit headToHead;
            Physics.Raycast(character.head.position, playerCh.head.position - character.head.position, out headToHead);

            RaycastHit headToBody;
            Physics.Raycast(character.head.position, playerCh.body.position - character.head.position, out headToBody);

            RaycastHit headToLeg;
            Physics.Raycast(character.head.position, playerCh.leg.position - character.head.position, out headToLeg);

            int basicRate = 0;
            if (headToHead.collider == playerCh.GetComponent<Collider>())
                basicRate += 20;
            if (headToBody.collider == playerCh.GetComponent<Collider>())
                basicRate += 20;
            if (headToLeg.collider == playerCh.GetComponent<Collider>())
                basicRate += 20;
            if (basicRate > 0)
            {
                if (targetList.ContainsKey(playerCh))
                {
                    targetList[playerCh] = basicRate;
                }
                else
                {
                    targetList.Add(playerCh, basicRate);
                }
            }



            else
            {
                if (targetList.ContainsKey(playerCh))
                {
                    targetList.Remove(playerCh);
                }
            }

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<playerSolider>())
        {
            playerSolider playerCh = other.transform.GetComponent<playerSolider>();
            if (targetList.ContainsKey(playerCh))
            {
                targetList.Remove(playerCh);
            }
        }

    }
    public void phaseBegin()
    {
        rangeScan.scanRange(character.allTiles,character.allTiles.getTile(transform.position).transform.position,0,character.moveLimit);
        rangeScan.showRange(character.allTiles);
        character.scaned = true;
    }
    public void getTarget()

    {
        int value = 0;
        if (targetList.Count > 0)
        {
            bPartol = false;
            foreach (KeyValuePair<playerSolider, int> kvp in targetList)
            {
                if (kvp.Value > value)
                {
                    value = kvp.Value;
                    curTarget = kvp.Key;
                }
            }
        }
        else curTarget = null;
       
       
    }
    public void patrol()
    {
        bPartol = true;
        //if(destination==Vector3.zero)
        {
            destination = transform.position;
            foreach (KeyValuePair<Vector3, int> kvp in rangeScan.rangeList)
            {
                if ((patrolPoints[curPatrolIndex].position - destination).magnitude < (patrolPoints[curPatrolIndex].position - destination).magnitude)
                {
                    if (character.allTiles.getTile(kvp.Key).status == Tile.TileStatus.EMPTY)
                        destination = kvp.Key;
                }
            }
            GetComponent<NavMeshAgent>().SetDestination(destination);
            GetComponent<SoilderAnimation>().targetSpeed = character.patrolSpeed;
        }
     
    }
    public void nextPoint()
    {
        curPatrolIndex++;
        if(curPatrolIndex>=patrolPoints.Length)
        {
            curPatrolIndex = 0;
        }
       
    }
}
