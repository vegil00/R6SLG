using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scanRoute : MonoBehaviour {
  
    //Dictionary<Vector3, int> tempList;
   public Dictionary<Vector3,int> rangeList;
    List<Vector3> outerLine;
	// Use this for initialization
	void Start () {
        //tempList = new Dictionary<Vector3, int>();
        rangeList = new Dictionary<Vector3, int>();
        outerLine = new List<Vector3>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void showRange(Tiles tiles)
    {


        outerLine.Clear();
        foreach (KeyValuePair<Vector3, int> kvp in rangeList)
        {
            if (!rangeList.ContainsKey(kvp.Key + Vector3.left * tiles.tileSize) || !rangeList.ContainsKey(kvp.Key + Vector3.right * tiles.tileSize) || !rangeList.ContainsKey(kvp.Key + Vector3.forward * tiles.tileSize) || !rangeList.ContainsKey(kvp.Key + Vector3.back * tiles.tileSize))
            {
                outerLine.Add(kvp.Key);
            }



        }
        int index = 0;
        for(int i=0;i<outerLine.Count;i++)
        {
            if(outerLine[i].x>outerLine[index].x)
            {
                index = i;
            }
           //tiles.getTile(outerLine[i]).transform.GetComponent<MeshRenderer>().enabled = true;
        }
       //foreach(KeyValuePair<Vector3,int> item in rangeList)
       // {
       //     tiles.getTile(item.Key).transform.GetComponent<MeshRenderer>().enabled = true;
       // }
        //Vector3 index = outerLine[0];
        List<Vector3> result = new List<Vector3>();
        result.Add(outerLine[index]);
        for (int i = 0; i < outerLine.Count; i++)
        {
           
            if (outerLine.Contains(result[i] + Vector3.right * tiles.tileSize) && !result.Contains(result[i] + Vector3.right * tiles.tileSize))
            {
                result.Add(result[i] + Vector3.right * tiles.tileSize);
            }
            else if (outerLine.Contains(result[i] + (Vector3.right + Vector3.forward) * tiles.tileSize) && !result.Contains(result[i] + (Vector3.right + Vector3.forward) * tiles.tileSize))
            {
                result.Add(result[i] + (Vector3.right + Vector3.forward) * tiles.tileSize);
            }
            else if (outerLine.Contains(result[i] + Vector3.forward * tiles.tileSize) && !result.Contains(result[i] + Vector3.forward * tiles.tileSize))
            {
                result.Add(result[i] + Vector3.forward * tiles.tileSize);
            }
            else if (outerLine.Contains(result[i] + (Vector3.left + Vector3.forward) * tiles.tileSize) && !result.Contains(result[i] + (Vector3.left + Vector3.forward) * tiles.tileSize))
            {
                result.Add(result[i] + (Vector3.left + Vector3.forward) * tiles.tileSize);
            }
            else if (outerLine.Contains(result[i] + Vector3.left * tiles.tileSize) && !result.Contains(result[i] + Vector3.left * tiles.tileSize))
            {
                result.Add(result[i] + Vector3.left * tiles.tileSize);
            }
            else if (outerLine.Contains(result[i] + (Vector3.left + Vector3.back) * tiles.tileSize) && !result.Contains(result[i] + (Vector3.left + Vector3.back) * tiles.tileSize))
            {
                result.Add(result[i] + (Vector3.left + Vector3.back) * tiles.tileSize);
            }
            else if (outerLine.Contains(result[i] + Vector3.back * tiles.tileSize) && !result.Contains(result[i] + Vector3.back * tiles.tileSize))
            {
                result.Add(result[i] + Vector3.back * tiles.tileSize);
            }
            else if (outerLine.Contains(result[i] + (Vector3.back + Vector3.right) * tiles.tileSize) && !result.Contains(result[i] + (Vector3.back + Vector3.right) * tiles.tileSize))
            {
                result.Add(result[i] + (Vector3.back + Vector3.right) * tiles.tileSize);
            }
            else
                break;
            Debug.Log(i);

        }
        //outerLine.Sort();
        GetComponent<LineRenderer>().positionCount =result.Count;
        GetComponent<LineRenderer>().SetPositions(result.ToArray());

    }
    public void scanRange(Tiles tiles,Vector3 pos,int minLimit,int maxLimit)
    {
        rangeList.Clear();
        Dictionary<Vector3, int> tempList = new Dictionary<Vector3, int>();
        tempList.Add(pos, 0);
        rangeList.Add(pos, 0);
        for(int i=0;i<maxLimit;i++)
        {
           // KeyValuePair<Vector3, int> kvp = new KeyValuePair<Vector3, int>();
           
           for(int j=0;j<tempList.Count;j++)
            {
                List<Vector3> temp = new List<Vector3>(tempList.Keys);
                if(tempList[temp[j]]==i)
                {
                   
                    DirectionalScan(temp[j], i, Vector3.left, tiles, minLimit, maxLimit,tempList);
                    DirectionalScan(temp[j], i, Vector3.right, tiles, minLimit, maxLimit,tempList);
                    DirectionalScan(temp[j], i, Vector3.forward, tiles, minLimit, maxLimit,tempList);
                    DirectionalScan(temp[j], i, Vector3.back, tiles, minLimit, maxLimit,tempList);
                }
               

            }
            Debug.Log(i);
        }
       
    }
    void DirectionalScan(Vector3 origin,int basicValue,Vector3 dir,Tiles tiles,int minLimit,int maxLimit,Dictionary<Vector3, int> tempList)
    {
        Vector3 newPos=new Vector3();
        dir = dir.normalized;
        if (tiles.getTile(origin + dir * tiles.tileSize) != null)
            newPos = tiles.getTile(origin + dir * tiles.tileSize).transform.position;
        else
            return;
        //if(newPos!=null)
        {
            if (effectiveTile(tiles, newPos, minLimit, maxLimit, basicValue + 1))
            {
                if (!tempList.ContainsKey(newPos))
                {
                    tempList.Add(newPos, basicValue + 1);
                    rangeList.Add(newPos, basicValue + 1);
                }
            }
        }
      
        //switch( dir){
        //    case SCANDIR.LEFT:
        //        newPos = origin + Vector3.left * tiles.tileSize;
        //        break;
        //    case SCANDIR.RIGHT:
        //        newPos = origin + Vector3.right * tiles.tileSize;
        //        break;
        //    case SCANDIR.UP:
        //        newPos = origin + Vector3.forward * tiles.tileSize;
        //        break;
        //    case SCANDIR.DOWN:
        //        newPos = origin + Vector3.back * tiles.tileSize;
        //        break;
        //}

    }
    bool effectiveTile(Tiles tiles,Vector3 pos, int minLimit, int maxLimit,int basicValue)
    {
        if (tiles.allTiles.ContainsKey(pos))
        {
            if (basicValue  >= minLimit && basicValue  <= maxLimit)
            {
                //if (tiles.allTiles[pos].status == Tile.TileStatus.EMPTY)
                    return true;
            }
        }
        
        return false;
    }
    public void disableShowedRange()
    {
        GetComponent<LineRenderer>().positionCount = 0;
    }
}
