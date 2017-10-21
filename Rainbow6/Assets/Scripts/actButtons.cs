using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class actButtons : MonoBehaviour {
    public Transform actPanel;
    public gameController game_controller;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Attack()
    {
       
        actPanel.Find("Title").GetComponent<Text>().text = "Fire";
        actPanel.Find("Act").GetComponent<Text>().text = "Does up to " + game_controller.playerChList[game_controller.curIndex].weapon.maxDamage.ToString() + " damage";
    }
}
