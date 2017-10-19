using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour {
    [HideInInspector]
    public Dictionary<Vector3, Tile> allTiles;
    public float tileSize;
	// Use this for initialization
	void Start () {
		
	}
    void Awake()
    {
        allTiles = new Dictionary<Vector3, Tile>();
        for(int i=0;i<transform.childCount;i++)
        {
            if(i==transform.childCount-1)
            {
                Debug.Log(transform.childCount - 1);
            }
          
            allTiles.Add(transform.GetChild(i).position, transform.GetChild(i).GetComponent<Tile>());
        }
    }
  public  Tile getTile(Vector3 pos)
    {
        List<Tile> results=new List<Tile>();
        foreach(KeyValuePair<Vector3,Tile> kvp in allTiles)
        {
            if((pos.x>=kvp.Key.x-tileSize/2&&pos.x<=kvp.Key.x+tileSize/2)&&(pos.z>=kvp.Key.z-tileSize/2&&pos.z<=kvp.Key.z+tileSize/2))
            {
                results.Add(kvp.Value);
            }
        }
        if(results.Count==0)
        {
            return null;
        }
        Tile result=results[0];


       foreach(Tile tile in results)
        {
            if(tile.transform.position.y-pos.y<result.transform.position.y-pos.y)
            {
                result = tile;
            }
            
        }
        return result;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
