using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
	public GameObject ImageObject;
    public static bool IsGameOver = false;
	// Use this for initialization
	void Start () {
		ImageObject.transform.localScale = new Vector3 (0, 0, 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void GameOverNow()
	{
		StartCoroutine (GameOverEffect ());
        IsGameOver = true;
	}
	IEnumerator GameOverEffect()
	{
		while (ImageObject.transform.localScale.y < 0.99f) {
			ImageObject.transform.localScale = new Vector3 (Mathf.Lerp (ImageObject.transform.localScale.x, 1, Time.deltaTime * 2), Mathf.Lerp (ImageObject.transform.localScale.x, 1, Time.deltaTime), 1);
			yield return null;
		}
	}
}
