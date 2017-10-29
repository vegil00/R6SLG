using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {
    public Transform target;
    public float distance;
    Vector3 standardOffset;
    Vector3 targetPos;
    public float posSmooth;
    public float rotateSmooth;
	// Use this for initialization
	void Start () {
		
	}
    void Awake()
    {
        standardOffset = (target.position - transform.position).normalized*distance;
        transform.position = target.position - standardOffset;
        targetPos = transform.position;
    }
	void FixedUpdate()
    {
        Vector3 standardPos = target.position - standardOffset;
        Vector3 abovePos = target.position + Vector3.up * distance;
        Vector3[] posList = new Vector3[5];
        posList[0] = standardPos;
        posList[1] = Vector3.Lerp(standardPos, abovePos, 0.25f);
        posList[2] = Vector3.Lerp(standardPos, abovePos, 0.5f);
            posList[3] = Vector3.Lerp(standardPos, abovePos, 0.75f);
        posList[4] = abovePos;
        for(int i=0;i<5;i++)
        {
            if (checkPos(posList[i]))
                break;
        }
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime*posSmooth);
        smoothRotation();

    }
    bool checkPos(Vector3 pos)
    {
        RaycastHit hit;
        Physics.Raycast(pos, target.position-pos, out hit,(target.position - pos).magnitude);
        if(hit.transform!=target)
        {
            return false;
        }
        else
        {
            targetPos = pos;
            return true;
        }
    }
    void smoothRotation()
    {
        Vector3 targetForward = target.position - transform.position;
        Quaternion targetQuaternion = Quaternion.LookRotation(targetForward, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, Time.deltaTime*rotateSmooth);
    }
	// Update is called once per frame
	void Update () {
       // transform.position = target.transform.position - distance * transform.forward.normalized;
	}
}
