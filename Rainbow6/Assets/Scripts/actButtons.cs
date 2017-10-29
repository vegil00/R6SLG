using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class actButtons : MonoBehaviour {
    public Transform actPanel;
    public gameController game_controller;
    public enemyList eneList;
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
        int i = 0;
        eneList.clearList();
        foreach(KeyValuePair<enemySolider,int> kvp in game_controller.playerChList[game_controller. curIndex].targetList)
        {
            if(i<eneList.transform.childCount)
            {
                eneList.transform.GetChild(i).gameObject.SetActive(true);
                eneList.addTarget( kvp.Key);
            }
            i++;
           
        }
    }
}
