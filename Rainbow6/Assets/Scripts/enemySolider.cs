using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemySolider :soldier {

    public bool scaned;
    public bool aimed;
    [HideInInspector]
    //public enemyBehavior behavior;
    NavMeshAgent agent;
    public BehaviorDesigner.Runtime.BehaviorTree behaviorTree;
    public Dictionary<playerSolider, int> targetList;
    scanRoute rangeScan;
    public float patrolSpeed;
    
	// Use this for initialization
	void Start () {
        scaned = false;
       // behavior = GetComponent<enemyBehavior>();
        Moved = false;
        Attacked = false;
        aimed = false;
        head = transform.Find("Head");
        body = transform.Find("Body");
        leg = transform.Find("Leg");
       // GameObject.Find("GameController").GetComponent<gameController>().enemyChList.Add(this);
        agent = GetComponent<NavMeshAgent>();
        rangeScan = GetComponent<scanRoute>();
        behaviorTree = GetComponent<BehaviorDesigner.Runtime.BehaviorTree>();
        targetList = new Dictionary<playerSolider, int>();
        if(behaviorTree!=null)
        {
            behaviorTree.enabled = false;
            Debug.Log("tree");
        }
    }
	
	// Update is called once per frame
	void Update () {
		//if(Attacked||Moved)
  //      {
  //          GetComponent<LineRenderer>().positionCount = 0;
  //      }
      
    }
    void OnTriggerStay(Collider other)
    {
        // behaviorTree.SendEvent<Collider>("OnTriggerStay", other);
        if (other.transform.GetComponent<playerSolider>()&&other is CapsuleCollider)
        {
            playerSolider playerCh = other.transform.GetComponent<playerSolider>();
            float angle = Vector3.Angle(transform.forward, (playerCh.transform.position - transform.position));
            if(Mathf.Abs(angle)<viewAngle/2)
            {
                RaycastHit headToHead;
                Physics.Raycast(head.position, playerCh.head.position - head.position, out headToHead);

                RaycastHit headToBody;
                Physics.Raycast(head.position, playerCh.body.position - head.position, out headToBody);

                RaycastHit headToLeg;
                Physics.Raycast(head.position, playerCh.leg.position - head.position, out headToLeg);

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


}
