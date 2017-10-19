using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public enum TileStatus { EMPTY=0,PLAYER,ENEMY};
    public TileStatus status;
    public Transform objectOnTile;
	// Use this for initialization
	void Start () {
		
	}
	void Awake()
    {
        status = TileStatus.EMPTY;
    }
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerStay(Collider other)
    {
        if(other is CapsuleCollider)
        {
            if (status == TileStatus.EMPTY)
            {
                if (other.tag == "Player")
                {
                    status = TileStatus.PLAYER;
                    objectOnTile = other.transform;
                }
                if (other.tag == "Enemy")
                {
                    status = TileStatus.ENEMY;
                    objectOnTile = other.transform;
                }
            }
        }
       
       
      
    }
    private void OnTriggerExit(Collider other)
    {
        if(other is CapsuleCollider)
        {
            objectOnTile = null;
            status = TileStatus.EMPTY;
        }
       
    }
}
