using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerSolider :soldier
{
    Tile currentTile;
   
    NavMeshAgent agent;
    scanRoute rangeScan;
    [HideInInspector]
  public  bool scaned;
  
    // Use this for initialization
    void Start()
    {
        rangeScan = GetComponent<scanRoute>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 0;
        head = transform.Find("Head");
        body = transform.Find("Body");
        leg = transform.Find("Leg");
        GameObject.Find("GameController").GetComponent<gameController>().playerChList.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentTile = allTiles.getTile(transform.position);
        if(!scaned)
        {
            rangeScan.scanRange(allTiles, currentTile.transform.position, 0, moveLimit);
            rangeScan.showRange(allTiles);
            scaned = true;
        }
      
        if(Attacked)
        {
           
        }
        //if (Input.GetButton("Fire1"))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           
            if(agent.remainingDistance<agent.stoppingDistance&&agent.speed>0)
            {
               // agent.speed = 0;
                 Moved = true;
            GetComponent<SoilderAnimation>().targetSpeed = 0;
            rangeScan.disableShowedRange();
            transform.position = allTiles.getTile(transform.position).transform.position;
            }


        //}
    }
    public void ProcessInputRay(Ray ray)
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.transform.GetComponent<Tile>()||hitInfo.transform.GetComponent<enemySolider>())
            {
                Tile tile = allTiles.getTile(hitInfo.transform.position); //hitInfo.transform.GetComponent<Tile>();
                if (tile.status == Tile.TileStatus.EMPTY && rangeScan.rangeList.ContainsKey(tile.transform.position)&&!Moved)
                {
                    if (rangeScan.rangeList[tile.transform.position] > attackLimit)
                    {
                       Attacked = true;
                    }
                  
                    agent.SetDestination(tile.transform.position);
                    GetComponent<SoilderAnimation>().targetSpeed = moveSpeed;
                }
                else if(tile.status==Tile.TileStatus.ENEMY)
                {
                    if(!Attacked)
                    {
                      
                        enemySolider enemy= tile.objectOnTile.GetComponent<enemySolider>();
                        //Debug.Log(enemy.id);
                        GetComponent<SoilderAnimation>().aimAt(enemy.transform);
                        GetComponent<LineRenderer>().positionCount = 0;
                        Moved = true;
                    }
                }
            }


        }
    }
}
