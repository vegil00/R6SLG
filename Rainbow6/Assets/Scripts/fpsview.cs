using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpsview : MonoBehaviour {
    float yRot;
    float xRot;
    public bool smooth;
    CharacterController controller;
    public float speed;
    public float mouseSensitive;
    public float smoothTime;
    public Transform camera;
    public Vector3 foward;
    public Vector3 right;
    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
#if !MOBILE_INPUT
       yRot = Input.GetAxis("Mouse X")*mouseSensitive;
        xRot = Input.GetAxis("Mouse Y")*mouseSensitive;
        Quaternion target = transform.rotation;
        Quaternion cameratarget = camera.rotation;
      
        if(!smooth)
        {
            transform.rotation *= Quaternion.Euler(-xRot, yRot, 0);
        }
        else
        {
            target *= Quaternion.Euler(0, yRot, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smoothTime);

            cameratarget *= Quaternion.Euler(-xRot, 0, 0);
            camera.rotation = Quaternion.Slerp(camera.rotation, cameratarget, Time.deltaTime*smoothTime);
           
        }
        foward = transform.forward;
        right = transform.right;
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 dir = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
            //dir = transform.TransformDirection(dir);
            controller.Move(speed * dir * Time.deltaTime);
            //transform.Translate(speed*dir,Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 dir = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
            //dir = transform.TransformDirection(dir);
            controller.Move(speed * -dir * Time.deltaTime);
            //transform.Translate(-speed*dir, Space.World);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 dir = new Vector3(transform.right.x, 0, transform.right.z).normalized;
            //dir = transform.TransformDirection(dir);
            controller.Move(speed * -dir * Time.deltaTime);
            //transform.Translate(-speed*dir, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 dir = new Vector3(transform.right.x, 0, transform.right.z).normalized;
            //dir = transform.TransformDirection(dir);
            controller.Move(speed * dir * Time.deltaTime);
            //transform.Translate(speed*dir, Space.World);
        }

#endif
    }
}
