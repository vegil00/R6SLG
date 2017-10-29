using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
    Vector3 dir;
    Rigidbody rigid;
	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void active(Vector3 direction, Vector3 position)
    {
        transform.position = position;
      
        rigid.AddForce(direction.normalized * 10);
       
    }
}
