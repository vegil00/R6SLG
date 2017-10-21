using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wayPointGizmo : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        transform.position = GameObject.Find("Tiles").GetComponent<Tiles>().getTile(transform.position).transform.position;
    }
    void  OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "wayPoint.png",true);
    }
}
