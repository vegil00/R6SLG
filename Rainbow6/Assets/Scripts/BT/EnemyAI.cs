using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class EnemyAI : BTTree {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    protected override void Init()
    {
        base.Init();
        _root = new BTPrioritySelector();
    }
}
