using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class rotateToward : Action {
   public SharedTransform target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override TaskStatus OnUpdate()
    {
        if(Mathf.Abs(transform.rotation.eulerAngles.y - target.Value.rotation.eulerAngles.y) < 1)
        {
            return TaskStatus.Success;
        }
        else
        {
            Quaternion targetRotaion = Quaternion.LookRotation(target.Value.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotaion, Time.deltaTime);
            return TaskStatus.Running;
        }
    }
    public int Fabroic(int n)
    {
        if(n<=1)
        {
            return n;
        }
        else
        {
            int n0 = 0;
            int n1 = 1;
            int tp =0;
            for(int i=2;i<=n;i++)
            {
                tp = n0 + n1;
                n0 = n1;
                n1 = tp;
            }
            return tp;
        }
    }
}
