using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TowerInfoPane : MonoBehaviour {
    public GameObject TowerSelected;
    private Tower tower;
    private bool hasSelection = false;
    private bool towerPlaced = true;

    public GameObject[] PrefabTowers;
    private Tower[] prefabTowers;

    public GameObject NameLabel;
    private Text nameLabel;

    public GameObject DamageLabel;
    private Text damageLabel;

    public GameObject FireRateLabel;
    private Text fireRateLabel;

    public GameObject RangeLabel;
    private Text rangeLabel;

    public GameObject CostLabel;
    private Text costLabel;

    public GameObject VelocityLabel;
    private Text velocityLabel;

    public GameObject UpgradeButtonText;
    private Text upgradeButtonText;

    public GameObject RangeCircle;
    private SpriteRenderer rangeSpriteRenderer;

    public GameObject SelectTargetWeak;
    private Toggle selectTargetWeak;
    public GameObject SelectTargetFirst;
    private Toggle selectTargetFirst;

    public bool TargetWeak = true;
	// Use this for initialization
	void Start () {
        nameLabel = NameLabel.GetComponent<Text>();
        damageLabel = DamageLabel.GetComponent<Text>();
        fireRateLabel = FireRateLabel.GetComponent<Text>();
        rangeLabel = RangeLabel.GetComponent<Text>();
        costLabel = CostLabel.GetComponent<Text>();
        velocityLabel = VelocityLabel.GetComponent<Text>();
        upgradeButtonText = UpgradeButtonText.GetComponent<Text>();

        rangeSpriteRenderer = RangeCircle.GetComponent<SpriteRenderer>();

        prefabTowers = new Tower[PrefabTowers.Length];
        for (int i = 0; i < prefabTowers.Length; i++ )
        {
            prefabTowers[i] = PrefabTowers[i].GetComponent<Tower>();
        }

        selectTargetWeak = SelectTargetWeak.GetComponent<Toggle>();
        selectTargetFirst = SelectTargetFirst.GetComponent<Toggle>();

        Deselect();
	}
	
	// Update is called once per frame
	void Update () {
        if (hasSelection && !tower.Placed)
        {
            rangeSpriteRenderer.enabled = true;
            RangeCircle.transform.localScale = new Vector3(0.26f, 0.26f, 1) * tower.Range;
            RangeCircle.transform.position = new Vector3(-0.01f, 0, 0) + TowerSelected.transform.position;
            towerPlaced = false;
        }
        if (hasSelection && !towerPlaced && tower.Placed)
        {
            towerPlaced = true;
            Deselect();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Deselect();
        }
	}
    public void ToggleSelection(GameObject toSelect)
    {
        if (hasSelection)
        {
            Deselect();
        }
        else
        {
            SelectTower(TowerSelected);
        }
    }
    public void SelectTower(GameObject toSelect)
    {
        TowerSelected = toSelect;
        hasSelection = true;
        tower = TowerSelected.GetComponent<Tower>();

        nameLabel.text = tower.TowerName;
        damageLabel.text = "Damage: " + tower.ProjectileDamage.ToString();
        fireRateLabel.text = "Fires one shot per " + tower.FireRate.ToString() + " seconds.";
        rangeLabel.text = "Range: " + tower.Range.ToString();
        costLabel.text = "Cost: $" + tower.Cost.ToString();
        velocityLabel.text = "Shot velocity: " + tower.ProjectileSpeed;

        SelectTargetFirst.SetActive(true);
        SelectTargetWeak.SetActive(true);

        if (tower.TargetWeak)
        {
            selectTargetWeak.isOn = true;
            selectTargetFirst.isOn = false;
        }
        else
        {
            selectTargetWeak.isOn = false;
            selectTargetFirst.isOn = true;
        }
        
        if (tower.HasUpgrade)
        {
            upgradeButtonText.text = "Upgrade: $" + tower.NextUpgrade.GetComponent<Tower>().Cost;
        }
        else
        {
            upgradeButtonText.text = "No more upgrades.";
        }

        rangeSpriteRenderer.enabled = true;
        RangeCircle.transform.localScale = new Vector3(0.26f, 0.26f, 1) * tower.Range;
        RangeCircle.transform.position = new Vector3(-0.01f, 0, 0) + TowerSelected.transform.position;
    }
    public void SelectPrefab(int num)
    {
        nameLabel.text = prefabTowers[num].TowerName;
        damageLabel.text = "Damage: " + prefabTowers[num].ProjectileDamage.ToString();
        fireRateLabel.text = "Fires one shot per " + prefabTowers[num].FireRate.ToString() + " seconds.";
        rangeLabel.text = "Range: " + prefabTowers[num].Range.ToString();
        costLabel.text = "Cost: $" + prefabTowers[num].Cost.ToString();
        velocityLabel.text = "Shot velocity: " + prefabTowers[num].ProjectileSpeed;

        if (prefabTowers[num].HasUpgrade)
        {
            upgradeButtonText.text = "Upgrade: $" + prefabTowers[num].NextUpgrade.GetComponent<Tower>().Cost;
        }
        else
        {
            upgradeButtonText.text = "No more upgrades.";
        }
    }
    public void Deselect()
    {
        if (towerPlaced)
        {
            hasSelection = false;
            nameLabel.text = "No tower selected.";
            damageLabel.text = "";
            fireRateLabel.text = "";
            rangeLabel.text = "";
            costLabel.text = "";
            velocityLabel.text = "";
            upgradeButtonText.text = "No tower selected.";
            rangeSpriteRenderer.enabled = false;
            SelectTargetFirst.SetActive(false);
            SelectTargetWeak.SetActive(false);
        }
    }
    public void UpgradeTower()
    {
        if(hasSelection && MoneyManager.Cash >= tower.NextUpgrade.GetComponent<Tower>().Cost)
        {
            GameObject oldTower = TowerSelected;
            TowerSelected = (GameObject)Instantiate(tower.NextUpgrade);
            tower = TowerSelected.GetComponent<Tower>();
            MoneyManager.Cash -= tower.Cost;
            TowerSelected.transform.position = oldTower.transform.position;
            tower.Place();
            tower.selector = this;
            tower.TargetWeak = oldTower.GetComponent<Tower>().TargetWeak;
            Deselect();
            SelectTower(TowerSelected);
            Destroy(oldTower);
        }
    }
    public void SetTargetFromCheckBox(bool isWeak)
    {
        SetTarget(isWeak);
    }
    public void SetTarget(bool weak)
    {
        if(hasSelection)
        {
            tower.TargetWeak = weak;
        }
    }
}
