using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class move : Action {

    public SharedVector3 curMoveTarget;
    public SharedBool actFinish;
    public SharedBool moved;
    public float speedDamp;
    public float rotateDamp;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override TaskStatus OnUpdate()
    {
        NavMeshAgent agent = transform.GetComponent<NavMeshAgent>();
        agent.destination = curMoveTarget.Value;
        Quaternion targetQuaternion = Quaternion.LookRotation(agent.desiredVelocity);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, Time.deltaTime);

      
        if (agent.remainingDistance < agent.stoppingDistance&&agent.speed>0)
        {
            agent.speed = 0;
            transform.GetComponent<Animator>().SetFloat("Speed", 0);
            transform.position = transform.GetComponent<enemySolider>().allTiles.getTile( agent.destination).transform.position;
           
            moved.Value = true;
            transform.GetComponent<scanRoute>().disableShowedRange();
            return TaskStatus.Success;

        }
        else
        {


            agent.speed = Mathf.Lerp(agent.speed, transform.GetComponent<enemySolider>().moveSpeed, Time.deltaTime);
            transform.GetComponent<Animator>().SetFloat("Speed", agent.speed);
            return TaskStatus.Running;
        }
       

    }
}
