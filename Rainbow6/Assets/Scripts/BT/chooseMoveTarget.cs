using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class chooseMoveTarget :Action {
    public SharedTransformList wayPoints;
    public SharedInt curIndex;
    public SharedVector3 moveTarget;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override TaskStatus OnUpdate()
    {
        
        Vector3 curMoveTarget = transform.GetComponent<enemySolider>().allTiles.getTile(wayPoints.Value[curIndex.Value].position).transform.position;
        scanRoute scaner = transform.GetComponent<scanRoute>();
        Vector3 moveTo = new Vector3 (transform.position.x,transform.position.y,transform.position.z);
        foreach(KeyValuePair<Vector3,int> kvp in scaner.rangeList)
        {
            if((curMoveTarget-moveTo).magnitude>(curMoveTarget-kvp.Key).magnitude)
            {
                moveTo = kvp.Key;
            }
        }
        moveTarget.Value = moveTo;
        return TaskStatus.Success;
    }
}
