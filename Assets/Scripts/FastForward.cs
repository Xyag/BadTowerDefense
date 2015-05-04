using UnityEngine;
using System.Collections;

public class FastForward : MonoBehaviour {
    bool fastForward = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ToggleFastForward()
    {
        fastForward = !fastForward;
        if (fastForward)
        {
            Time.timeScale = 4;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
