using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControll : MonoBehaviour {
    public float moveSpeed;
    Rigidbody rigid;
    float curSpeed;
	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if(v!=0||h!=0)
        {
            Vector3 dir = new Vector3(h, 0, v).normalized;

            rigid.MovePosition(transform.position + dir * moveSpeed);
            curSpeed = Mathf.Lerp(curSpeed, moveSpeed, Time.deltaTime);
        }
        else
        {
            curSpeed = 0;
        }
        GetComponent<Animator>().SetFloat("Speed", curSpeed);
      
	}
}
