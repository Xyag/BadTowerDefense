using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour {
	public GameObject Projectile;
	public float FireRate = 0.3f;
	public float Range = 3;
	public float ProjectileDamage;
	public float ProjectileSpeed;
	public GameObject projTarget;
	public int Cost;
    public string TowerName;

    public bool TargetWeak;

    public GameObject NextUpgrade;
    public bool HasUpgrade = false;

    public TowerInfoPane selector;

	public bool Placed = false;
	void Start () {
	}
	void Fire()
	{
		//random number just so it's not a coincidence if a unit has the same health as base
		float lowestHealth = -1;
        //-1 becuase distance starts at 0. same as above
        float mostDistance = -1;
        if (TargetWeak)
        {
            foreach (GameObject obj in WaveGenerator.WorldEnemies.Values)
            {
                if (obj.GetComponent<Enemy>().Health < lowestHealth && Vector2.Distance(obj.transform.position, transform.position) < Range)
                {
                    lowestHealth = obj.GetComponent<Enemy>().Health;
                    projTarget = obj;
                }
            }

            if (lowestHealth != -1)
            {
                GameObject proj = Instantiate(Projectile);
                proj.transform.position = transform.position;
                TowerBullet bullet = proj.GetComponent<TowerBullet>();
                bullet.Target = projTarget;
                bullet.Damage = ProjectileDamage;
                bullet.Speed = ProjectileSpeed;
            }
        }
        else
        {
            foreach (GameObject obj in WaveGenerator.WorldEnemies.Values)
            {
                if (Vector2.Distance(obj.transform.position, transform.position) < Range && obj.GetComponent<Enemy>().DistanceMoved > mostDistance)
                {
                    mostDistance = obj.GetComponent<Enemy>().DistanceMoved;
                    projTarget = obj;
                }
            }
            if (mostDistance != -1)
            {
                GameObject proj = Instantiate(Projectile);
                proj.transform.position = transform.position;
                TowerBullet bullet = proj.GetComponent<TowerBullet>();
                bullet.Target = projTarget;
                bullet.Damage = ProjectileDamage;
                bullet.Speed = ProjectileSpeed;
            }
        }
	}
	// Update is called once per frame
	void Update () {
		if (!Placed) {
			transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0));
			transform.position += new Vector3(0,0,10);
			if (Input.GetMouseButtonDown(1))
			{
                Place();
			}
		}
	}
    public void Place()
    {
        Placed = true;
        InvokeRepeating("Fire", 0, FireRate);
    }
    void OnMouseDown()
    {
        selector.SelectTower(gameObject);
    }
}
