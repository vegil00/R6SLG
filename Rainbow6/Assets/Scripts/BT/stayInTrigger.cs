using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class stayInTrigger :Conditional{
    enemySolider character;
   public  SharedGameObjectList targetList;
   
	// Use this for initialization
	void Start () {
        character = GetComponent<enemySolider>();
        
    }
    void OnEnable()
    {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public override TaskStatus OnUpdate()
    {
        if (targetList.Value == null)
            targetList.Value = new List<GameObject>();
        return TaskStatus.Running;
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
                if (!targetList.Value.Contains(playerCh.gameObject))
                {
                    targetList.Value.Add(playerCh.gameObject);
                }
              
            }



            else
            {
                if (targetList.Value.Contains(playerCh.gameObject))
                {
                    targetList.Value.Remove(playerCh.gameObject);
                }
            }

        }
    }
}
