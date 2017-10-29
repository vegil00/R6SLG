using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CBulletPool : MonoBehaviour {
    List<GameObject> pool = new List<GameObject>();
    public GameObject bulletPrefab;
    public int num;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < num; i++)
        {
            GameObject obj =Object.Instantiate(bulletPrefab);
            obj.SetActive(false);
            //obj.GetComponent<bullet>().ACTIVE = false;
            pool.Add(obj);
        }
    }
	
	// Update is called once per frame
	void Update () {
        //for (int i = 0; i < num; i++)
        //{
        //    pool[i].GetComponent<CBullet>().Update();
        //}
    }
   public GameObject active( Vector3 direction, Vector3 position)
    {
       
       for(int i=0;i<num;i++)
        {
            //bool test = pool[i].GetComponent<bullet>().ACTIVE;
            if (!(pool[i].activeSelf))
            {
                pool[i].SetActive(true);
                pool[i].GetComponent<bullet>().active( direction, position);
                GameObject obj = pool[i];
                return obj;
            }
            else if(i==num-1)
            {
                GameObject obj = Object.Instantiate(bulletPrefab);
                pool.Add(obj);
                pool[i].GetComponent<bullet>().active(direction, position);
                return obj;

            }
        }
        return bulletPrefab;

    }
}
