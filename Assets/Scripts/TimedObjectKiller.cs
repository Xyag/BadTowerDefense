using UnityEngine;
using System.Collections;

public class TimedObjectKiller : MonoBehaviour {
	public float TimeToKill = 1;
	// Use this for initialization
	void Start () {
		Invoke ("Kill", TimeToKill);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Kill()
	{
		Destroy (gameObject);
	}
}
