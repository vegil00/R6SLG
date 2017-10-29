using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public int maxDamage;
    public int minDamage;
    public int distanceRate;
    public int zeroAcc;
    public float fireRate;
   public Transform firePosition;
    float fireTimer;
    CBulletPool pool;
    // Use this for initialization
    void Start () {
        fireTimer = fireRate;
        //firePosition = transform.Find("FirePosition");
        pool = GameObject.Find("BulletPool").GetComponent<CBulletPool>();
	}
	
	// Update is called once per frame
	void Update () {
        fireTimer += Time.deltaTime;
	}
  public  void Fire()
    {
        if(fireTimer>=fireRate)
        {
            fireTimer = 0;
            pool.active(firePosition.position, transform.forward);
        }
    }
}
