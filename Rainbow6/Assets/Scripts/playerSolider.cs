using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerSolider :soldier
{
    Tile currentTile;
   
    NavMeshAgent agent;
    scanRoute rangeScan;
    public Dictionary<enemySolider, int> targetList;
    
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
        targetList = new Dictionary<enemySolider, int>();
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
        Physics.Raycast(ray, out hitInfo);
        while(hitInfo.collider is SphereCollider)
            {
            Physics.Raycast(hitInfo.point, ray.direction, out hitInfo);
        }
        //if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.transform.GetComponent<Tile>())
            {
                processHitInfo(hitInfo);
               
            }
            else if ((hitInfo.transform.GetComponent<enemySolider>()))
            {
               if(hitInfo.collider is SphereCollider)
                {
                    while(hitInfo.collider is SphereCollider)
                    {
                        Physics.Raycast(hitInfo.point, ray.direction, out hitInfo);

                    }
                    processHitInfo(hitInfo);
                    //RaycastHit inSphere;
                    //Physics.Raycast(hitInfo.point+Mathf.Epsilon*ray.direction.normalized, ray.direction, out inSphere);
                    //if(inSphere.collider is SphereCollider)
                    //{
                    //    RaycastHit outSphere;
                    //    Physics.Raycast(inSphere.point, ray.direction, out outSphere);
                    //    processHitInfo(outSphere);
                    //}
                    //else
                    //processHitInfo(inSphere);
                }
            }


        }
    }
    void processHitInfo(RaycastHit hit)
    {
       
        Tile tile = allTiles.getTile(hit.transform.position); //hitInfo.transform.GetComponent<Tile>();
        if (tile.status == Tile.TileStatus.EMPTY && rangeScan.rangeList.ContainsKey(tile.transform.position) && !Moved)
        {
            if (rangeScan.rangeList[tile.transform.position] > attackLimit)
            {
                Attacked = true;
            }

            agent.SetDestination(tile.transform.position);
            GetComponent<SoilderAnimation>().targetSpeed = moveSpeed;
        }
        else if (tile.status == Tile.TileStatus.ENEMY)
        {
            if (!Attacked)
            {

                enemySolider enemy = tile.objectOnTile.GetComponent<enemySolider>();
                //Debug.Log(enemy.id);
                GetComponent<SoilderAnimation>().aimAt(enemy.transform);
                GetComponent<LineRenderer>().positionCount = 0;
                Moved = true;
            }
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if(other is CapsuleCollider &&other.GetComponent<enemySolider>())
        {
            float angle = Vector3.Angle(transform.forward, (other.transform.position - transform.position));
            enemySolider enemyCh = other.GetComponent<enemySolider>();
            if (angle<viewAngle/2)
            {
                
                RaycastHit headToHead;
                Physics.Raycast(head.position, enemyCh.head.position - head.position, out headToHead);

                RaycastHit headToBody;
                Physics.Raycast(head.position, enemyCh.body.position - head.position, out headToBody);

                RaycastHit headToLeg;
                Physics.Raycast(head.position, enemyCh.leg.position - head.position, out headToLeg);

                int basicRate = 0;
                if (headToHead.collider == enemyCh.GetComponent<Collider>())
                    basicRate += 20;
                if (headToBody.collider == enemyCh.GetComponent<Collider>())
                    basicRate += 20;
                if (headToLeg.collider == enemyCh.GetComponent<Collider>())
                    basicRate += 20;
                if (basicRate > 0)
                {
                    if (targetList.ContainsKey(enemyCh))
                    {
                        targetList[enemyCh] = basicRate;
                    }
                    else
                    {
                        //enemyCh.transform.GetComponent<Renderer>().enabled = true;
                        targetList.Add(enemyCh, basicRate);
                    }
                }



                else
                {
                    if (targetList.ContainsKey(enemyCh))
                    {
                        //enemyCh.transform.GetComponent<Renderer>().enabled = false;
                        targetList.Remove(enemyCh);
                    }
                }
            }
            else
            {
                if(targetList.ContainsKey(enemyCh))
                {
                    targetList.Remove(enemyCh);
                }
                //enemyCh.transform.GetComponent<Renderer>().enabled = false;
            }
        }
    }
}
