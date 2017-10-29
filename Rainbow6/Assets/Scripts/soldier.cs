using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class soldier : MonoBehaviour {
    public enum WEAPON_TYPE { SNIPER,SHOTGUN,RIFLE,PISTOL};
    public WEAPON_TYPE weaponType;
    public Weapon weapon;
    public bool Attacked;
    public bool Moved;
    public Tiles allTiles;
    public int moveLimit;
    public int attackLimit;
    public float viewAngle;
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
