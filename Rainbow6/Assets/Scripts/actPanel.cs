using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actPanel : MonoBehaviour {
    public gameController game_controller;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OKButton()
    {
        game_controller.playerChList[game_controller.curIndex].shootAtEnemy();
    }
}
