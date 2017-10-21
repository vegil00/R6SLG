using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class patrol : Action{
    public SharedTransformList wayPoints;
    public SharedInt curIndex;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override TaskStatus OnUpdate()
    {
        Vector3 test = transform.GetComponent<enemySolider>().allTiles.getTile(wayPoints.Value[curIndex.Value].position).transform.position;
        if ((transform.position- wayPoints.Value[curIndex.Value].position).magnitude<1)
        {
            curIndex.Value++;
            if(curIndex.Value>=wayPoints.Value.Count)
            {
                curIndex.Value = 0;
            }
        }
        return TaskStatus.Success;
       
    }
}
