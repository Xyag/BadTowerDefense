using UnityEngine;
using System.Collections;

public class TowerBullet : MonoBehaviour {
	public GameObject Target;
	public float Damage;
	public float Speed;
	bool hit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Target == null) { Destroy(gameObject); }
		if (!hit) {
			transform.position = Vector3.MoveTowards (transform.position, Target.transform.position, Speed * Time.deltaTime);
			if (Vector2.Distance (transform.position, Target.transform.position) < 0.1f) {
				Target.GetComponent<Enemy> ().TakeDamage (Damage);
				hit = true;
				Destroy(gameObject);

			}
		}
	}
}
