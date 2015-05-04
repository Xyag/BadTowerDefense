using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour {
	public static int Lives = 10;
	public GameObject UIText;
	private Text text;
	private GameOver over;
	// Use this for initialization
	void Start () {
		text = UIText.GetComponent<Text> ();
		over = GetComponent<GameOver> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Lives: " + Lives.ToString ();
		if (Lives <= 0) {
			over.GameOverNow();
			LivesManager.Lives = 0;
		}
	}
}
