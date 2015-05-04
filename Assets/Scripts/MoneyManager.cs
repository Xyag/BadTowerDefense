using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof (TowerInfoPane))]
public class MoneyManager : MonoBehaviour {
	public static float Cash = 250;
    public static float KillCashMult = 1;
	public GameObject UIText;
	private Text text;
    private TowerInfoPane info;
	// Use this for initialization
	void Start () {
		text = UIText.GetComponent<Text> ();
        info = GetComponent<TowerInfoPane>();
	}
	public void BuyTower(int typenum)
	{
		if (WaveGenerator.TowerTypes [typenum].GetComponent<Tower> ().Cost <= Cash) {
			GameObject tower = Instantiate(WaveGenerator.TowerTypes [typenum]);
			MoneyManager.Cash -= tower.GetComponent<Tower> ().Cost;
            tower.GetComponent<Tower>().selector = info;
            info.SelectTower(tower);
		}
	}
    public void HoverTower(int typenum)
    {

    }
	// Update is called once per frame
	void Update () {
		text.text = "Cash: $" + Mathf.Floor(MoneyManager.Cash);
	}
}
