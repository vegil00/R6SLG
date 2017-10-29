using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyList : MonoBehaviour {
    List<enemySolider> list;
  public  gameController game_controller;
	// Use this for initialization
	void Start () {
        list = new List<enemySolider>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(list.Count);
		
	}
   public void addTarget(enemySolider s)
    {
        list.Add( s);
    }
    public void BeClick(int ChildCount)
    {
        game_controller.playerAimAtEnemy(list[ChildCount]);
    }
    public void clearList()
    {
        list.Clear();
    }
}
