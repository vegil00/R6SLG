using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class withinSight : Conditional {
    public int targetInsightNum;
    enemySolider character;
   public SharedTransform currentTarget;
    public Dictionary<playerSolider, int> targetList;
   
    // Use this for initialization
    void Start () {
        character = GetComponent<enemySolider>();
        targetList = new Dictionary<playerSolider, int>();
        currentTarget = null;
    }
	
	// Update is called once per frame
	void Update () {
    
	}
    public override TaskStatus OnUpdate()
    {
        if (targetList.Count > 0)
        {
            foreach(KeyValuePair<playerSolider,int> kvp in targetList)
            {
                if(currentTarget==null)
                {
                    currentTarget = kvp.Key.transform;
                }
                else
                {
                    if(kvp.Value>targetList[currentTarget.Value.GetComponent<playerSolider>()])
                    {
                        currentTarget = kvp.Key.transform;
                    }
                }
            }
            return TaskStatus.Success;
        }
            
        else
            return TaskStatus.Failure;
    }
    public override void OnTriggerStay(Collider other)
   
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
    public override void OnTriggerExit(Collider other)
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
