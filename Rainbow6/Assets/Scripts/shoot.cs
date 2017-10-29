using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class shoot : Action {
    bool acting;
    float timer;
    public float actTime;
    public SharedBool finish;
	// Use this for initialization
	void Start () {
        acting = false;
        timer = 0;
        finish.Value = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override TaskStatus OnUpdate()
    {
        if(!acting)
        {
            GetComponent<Animator>().SetBool("shoot", true);
            acting = true;
            
        }
      if(acting)
        {
            if(timer<actTime)
            {
                timer += Time.deltaTime;
                return TaskStatus.Running;
            }
            else
            {
                timer = 0;
                acting = false;
                finish.Value = true;
                GetComponent<Animator>().SetBool("shoot", false);
                return TaskStatus.Success;
            }
        }
        return TaskStatus.Running;
    }

}
