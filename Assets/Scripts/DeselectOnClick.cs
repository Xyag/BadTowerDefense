using UnityEngine;
using System.Collections;

public class DeselectOnClick : MonoBehaviour {
    public TowerInfoPane info;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {
        info.Deselect();
    }
}
