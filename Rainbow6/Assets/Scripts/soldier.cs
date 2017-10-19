using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class soldier : MonoBehaviour {
    public bool Attacked;
    public bool Moved;
    public Tiles allTiles;
    public int moveLimit;
    public int attackLimit;
  
    [HideInInspector]
   public int index;
  
    public float moveSpeed;
  
    public float curSpeed;
    [HideInInspector]
    public Transform head;
    [HideInInspector]
    public Transform body;
    [HideInInspector]
    public Transform leg;
  
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
