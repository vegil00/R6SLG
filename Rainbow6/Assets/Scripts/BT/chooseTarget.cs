using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class chooseTarget :Action {
    //public SharedGameObjectList targetList;
    //enemySolider character;
    public SharedTransform curTarget;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override TaskStatus OnUpdate()
    {
        if(transform.GetComponent<enemySolider>().targetList.Count>0)
        {
            //int[] rateList = new int[targetList.Value.Count];
           // int maxIndex = 0;
            int maxRate = 0;
            foreach(KeyValuePair<playerSolider,int> kvp in transform.GetComponent<enemySolider>().targetList)
            {
               
             
                if(kvp.Value>maxRate)
                {
                    curTarget.Value = kvp.Key.transform;
                    maxRate = kvp.Value;
                }
            }
           // curTarget = targetList.Value[maxIndex].transform;
            
          
        }
        return TaskStatus.Success;
    }
    int calculateBasicRate(playerSolider playerCh)
    {
      enemySolider  character = GetComponent<enemySolider>();
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
        return basicRate;
    }
}
