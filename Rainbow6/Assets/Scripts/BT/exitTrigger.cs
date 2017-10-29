using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class exitTrigger : HasExitedTrigger {
  public  SharedGameObjectList targetList;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
    public override void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<playerSolider>())
        {
          
            if (targetList.Value.Contains(other.gameObject))
            {
                targetList.Value.Remove(other.gameObject);
            }
        }
    }
}
