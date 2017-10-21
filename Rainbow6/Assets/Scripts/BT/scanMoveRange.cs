using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class scanMoveRange :Action {

    // Use this for initialization
    public SharedBool moved;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override TaskStatus OnUpdate()
    {
        moved.Value = false;
        enemySolider character = transform.GetComponent<enemySolider>();
        transform.GetComponent<scanRoute>().scanRange(character.allTiles, transform.position, 0, character.moveLimit);
        transform.GetComponent<scanRoute>().showRange(character.allTiles);
        return TaskStatus.Success;
    }

}
