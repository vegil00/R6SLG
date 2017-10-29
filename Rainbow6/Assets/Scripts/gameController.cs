using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;


public class gameController : MonoBehaviour {
    public enum PHASESTATUS { PLAYERPHASE,ENEMYPHASE};
public enum GAMESTATUS { GAME};
    public PHASESTATUS phaseStatus;
    public GAMESTATUS gameStatus;
   public int curIndex;
    public List<playerSolider> playerChList;
    public List<enemySolider> enemyChList;
	// Use this for initialization
    void Awake()
    {
        playerChList = new List<playerSolider>();
        enemyChList = new List<enemySolider>();
    }
	void Start () {
        curIndex = 0;
        //playerChList = new List<playerSolider>();
	}
	
	// Update is called once per frame
	void Update () {
		if(gameStatus==GAMESTATUS.GAME)
        {
            if(phaseStatus==PHASESTATUS.PLAYERPHASE)
            {
                if(!playerChList[curIndex].Moved|| !playerChList[curIndex].Attacked)
                {
                    if (Input.GetButton("Fire1"))
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        playerChList[curIndex].ProcessInputRay(ray);
                    }
                }
                if(playerChList[curIndex].Attacked)
                {
                    nextCh();
                }
                for(int i=0;i<playerChList.Count;i++)
                {
                    if(!playerChList[i].Attacked)
                    {
                        break;
                    }
                    if(i==playerChList.Count-1)
                    {
                        changePhase(PHASESTATUS.ENEMYPHASE);
                        Debug.Log("enemyPhase");
                    }
                }
                
            }
            else
            {
                //if(!enemyChList[curIndex].scaned)
                //{
                //    enemyChList[curIndex].behavior.phaseBegin();
                //}
                //if(!enemyChList[curIndex].Attacked&&!enemyChList[curIndex].Moved)
                //{
                //    if(enemyChList[curIndex].behavior.targetList.Count>0)
                //    {

                //        enemyChList[curIndex].behavior.getTarget();





                //    }
                //    else
                //    {
                //        enemyChList[curIndex].behavior.patrol();
                //    }
                //}
                //if(enemyChList[curIndex].Attacked)
                //{
                //    nextCh();
                //    if(curIndex>=enemyChList.Count)
                //    {
                //        changePhase(PHASESTATUS.PLAYERPHASE);
                //    }
                //}
                //GlobalVariables.Instance.GetVariable("ActFinish");
               SharedBool actfinish =(SharedBool)enemyChList[curIndex].behaviorTree.GetVariable("actFinish");
                if (actfinish.Value==true)
                {
                    enemyChList[curIndex].GetComponent<BehaviorTree>().DisableBehavior();
                    nextCh();
                    if (curIndex >= enemyChList.Count)
                    {
                        changePhase(PHASESTATUS.PLAYERPHASE);
                    }
                }
                //else
                //{
                //    enemyChList[curIndex].GetComponent<BehaviorDesigner.Runtime.BehaviorTree>().enabled = true;
                //}

            }
        }
	}
    public void playerAimAtEnemy(enemySolider s)
    {
        playerChList[curIndex].aimAtEnemy(s);
    }
    void nextCh()

    {
       // curIndex = 0;
        if (phaseStatus == PHASESTATUS.PLAYERPHASE)
        {
            //while (curIndex < playerChList.Count-1)
            //{
            //    curIndex++;
            //    if(!playerChList[curIndex].Attacked)
            //    {
            //        break;
            //    }
            //}
            for(int i=0;i<playerChList.Count;i++)
            {
                if(!playerChList[i].Attacked)
                {
                    curIndex =i;
                    break;
                }
            }
        }
        else
        {
            curIndex++;
            if(curIndex<enemyChList.Count)
            {
                enemyChList[curIndex].behaviorTree.EnableBehavior();
                //if (!enemyChList[curIndex].scaned)
                //{
                //    enemyChList[curIndex].behavior.phaseBegin();
                //}
            }
        }
        
    }
    void changePhase(PHASESTATUS nextPhase)
    {
        phaseStatus = nextPhase;
        curIndex = 0;
        switch (nextPhase)
        {
            case PHASESTATUS.PLAYERPHASE:
               for(int i=0;i<playerChList.Count;i++)
                {
                    playerChList[i].Moved = false;
                    playerChList[i].Attacked = false;
                    playerChList[i].scaned = false;
                }
                break;
            case PHASESTATUS.ENEMYPHASE:
                enemyChList[0].behaviorTree.EnableBehavior();
                for (int i=0;i<enemyChList.Count;i++)
                {
                    enemyChList[i].Attacked = false;
                    enemyChList[i].Moved = false;
                    enemyChList[i].aimed = false;
                    enemyChList[i].scaned = false;
                    enemyChList[i].GetComponent<BehaviorTree>().SetVariableValue("actFinish", false);
                }
                break;
        }
    }
}
