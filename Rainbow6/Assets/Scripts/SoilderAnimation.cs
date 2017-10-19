using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoilderAnimation : MonoBehaviour {
    Animator animator;
    NavMeshAgent agent;
    [HideInInspector]
    bool aim;
  public  float targetSpeed;
    [HideInInspector]
    public Quaternion targetRotation;
    public float rotateDampTime;
    public float speedDampTime;
    public float shootTime;
    public Transform thirdPersonalCamera;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        aim = false;
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetFloat("Speed", agent.speed);
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            targetRotation = Quaternion.LookRotation(agent.desiredVelocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateDampTime);


        }
        else
            agent.speed = 0;
        if(aim)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateDampTime);
            if(Mathf.Abs(transform.rotation.eulerAngles.y- targetRotation.eulerAngles.y)<1)
                //if(transform.rotation==targetRotation)
            {
                transform.rotation = targetRotation;
                aim = false;
                GetComponent<Animator>().SetBool("shoot", true);
                if(transform.tag=="Player")
                {
                    thirdPersonalCamera.gameObject.SetActive(true);
                    thirdPersonalCamera.position = transform.Find("cameraPos").position;
                    thirdPersonalCamera.rotation = transform.Find("cameraPos").rotation;
                }
               
                StartCoroutine(shootReturnIdle());
                
            }
        }
        agent.speed = Mathf.Lerp(agent.speed, targetSpeed, Time.deltaTime);
        if(agent.speed>0)
        {
            if(transform.tag == "Player")
            {
                thirdPersonalCamera.gameObject.SetActive(true);
                thirdPersonalCamera.position = transform.Find("cameraPos").position;
                thirdPersonalCamera.rotation = transform.Find("cameraPos").rotation;
            }
           
        }
        else
        {
            if(!GetComponent<Animator>().GetBool("shoot")&&transform.tag=="Player")
            thirdPersonalCamera.gameObject.SetActive(false);
        }
       
    }
   public void aimAt(Transform target)
    {
        Vector3 lookDir = target.position - transform.position;
        lookDir.y = 0;
        targetRotation = Quaternion.LookRotation(lookDir);
        aim = true;
    }
    IEnumerator shootReturnIdle()
    {
        yield return new WaitForSeconds(shootTime);
        GetComponent<Animator>().SetBool("shoot", false);
        if (transform.tag == "Player") 
        thirdPersonalCamera.gameObject.SetActive(false);
        GetComponent<soldier>().Attacked = true;

    }
}
