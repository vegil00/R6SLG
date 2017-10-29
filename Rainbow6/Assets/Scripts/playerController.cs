using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    public float moveSpeed;
    float curSpeed;
    Animator animator;
    public float speedDamp;
    public float rotateDamp;
    CharacterController controller;
    public Weapon weapon;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

    }
	
	// Update is called once per frame
	void Update () {
       
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector3 dir = new Vector3(h, 0, v).normalized;
        if(h!=0||v!=0)
        {
            curSpeed = Mathf.Lerp(curSpeed, moveSpeed, Time.deltaTime);

        }
        else
        {
            curSpeed = Mathf.Lerp(curSpeed, 0, Time.deltaTime);
        }
        animator.SetFloat("Speed", curSpeed);
        controller.Move(dir * curSpeed);
        Vector3 mousePos =new Vector3();
        mousePos = getMousePos();
        mousePos.y = transform.position.y;
        Quaternion targetQuaternion = Quaternion.LookRotation(mousePos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, Time.deltaTime);
        if(Input.GetAxis("Fire1")>0)
        {
            animator.SetBool("shoot", true);
            weapon.Fire();
        }
        else
        {
            animator.SetBool("shoot", false);
        }

    }
    Vector3 getMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo);
        return hitInfo.point;
    }
}
