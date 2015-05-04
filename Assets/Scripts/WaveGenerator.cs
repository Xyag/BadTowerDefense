using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WaveGenerator : MonoBehaviour {
	public enum EnemyType {Earth=0, Ice=1, Fire=2, Resistant=3};
	public enum TowerType {Earth=0,EarthII=1, Ice=2,IceII=3, Fire=4,FireII=5};
	public GameObject[] EditorTowerTypes;
	public GameObject[] EditorEnemyTypes;

	public static GameObject[] TowerTypes;
	public static GameObject[] EnemyTypes;
	public static Dictionary<string,GameObject> WorldEnemies = new Dictionary<string, GameObject>();
	public static Dictionary<string,GameObject> WorldTowers = new Dictionary<string, GameObject>();

	public GameObject TextObject;
	private Text text;
	public int level = 0;

    public bool SpawningWave = false;
    bool groupSpawn = false;
    bool waitSpawn = false;
    bool randomSpawn = false;

    public GameObject ButtonObject;
    private Button button;
	// Use this for initialization
	void Start () {
		WaveGenerator.TowerTypes = EditorTowerTypes;
		WaveGenerator.EnemyTypes = EditorEnemyTypes;
		text = TextObject.GetComponent<Text> ();
        button = ButtonObject.GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Wave " + level.ToString ();
        button.interactable = SpawningWave;
        if (GameOver.IsGameOver)
        {
            button.interactable = false;
        }
        SpawningWave = !groupSpawn && !waitSpawn && !randomSpawn;
	}
	IEnumerator GroupedSpawner(int amount, float frequency, EnemyType type)
	{
        groupSpawn = true;
		for (int i = 0; i < amount; i++) {
			SpawnEnemy(type);
			yield return new WaitForSeconds(frequency);
		}
        groupSpawn = false;
	}
    IEnumerator WaitThenGroupSpawner(int amount, float frequency, float delay, EnemyType type)
    {
        waitSpawn = true;
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < amount; i++)
        {
            SpawnEnemy(type);
            yield return new WaitForSeconds(frequency);
        }
        waitSpawn = false;
    }
	public void SpawnEnemy(EnemyType type)
	{
        Enemy.TotalEnemies++;
		GameObject enemy = Instantiate(EnemyTypes[(int)type]);
        enemy.name = Enemy.TotalEnemies.ToString();
		WaveGenerator.WorldEnemies.Add (enemy.name, enemy);
	}
    public void NextWave()
    {
        if (level > 10 && MoneyManager.KillCashMult > 0.01f)
        {
            MoneyManager.KillCashMult *= 0.9f;
        }
        if (level > 25 && Enemy.GlobalDamageMult > 0.2f)
        {
            Enemy.GlobalDamageMult *= 0.9f;
        }
        if (level > 40)
        {
            Enemy.GlobalDamageMult *= 0.95f;
        }
        SpawnWave(level + 1);
    }
    public void SpawnWave(int number)
    {
        SpawningWave = true;
        level = number;
        switch (number)
        {
            case 1:
                StartCoroutine(GroupedSpawner(10, 1, EnemyType.Fire));
                break;
            case 2:
                StartCoroutine(GroupedSpawner(10, 0.5f, EnemyType.Fire));
                break;
            case 3:
                SpawnEnemy(EnemyType.Resistant);
                StartCoroutine(GroupedSpawner(5, 1, EnemyType.Fire));
                break;
            case 4:
                StartCoroutine(GroupedSpawner(7, 1, EnemyType.Earth));
                StartCoroutine(WaitThenGroupSpawner(5, 0.5f, 2, EnemyType.Fire));

                break;
            case 5:
                StartCoroutine(GroupedSpawner(10, 0.1f, EnemyType.Fire));
                SpawnEnemy(EnemyType.Ice);
                break;
            default:
                StartCoroutine(SpawnRandomWave(number));
                break;
        }
    }
    IEnumerator SpawnRandomWave(int wave)
    {
        randomSpawn = true;
        StartCoroutine(GroupedSpawner(wave + 2, 0.3f, EnemyType.Fire));
        SpawnEnemy(EnemyType.Resistant);
        yield return new WaitForSeconds(2);
        for (int i = 0; i < wave - 3; i++)
        {
            SpawnEnemy(EnemyType.Earth);
            yield return new WaitForSeconds(0.3f);
        }
        SpawnEnemy(EnemyType.Resistant);
        yield return new WaitForSeconds(3);
        for (int i = 0; i < wave; i++)
        {
            SpawnEnemy(EnemyType.Fire);
            yield return new WaitForSeconds(0.15f);
        }
        SpawnEnemy(EnemyType.Resistant);
        yield return new WaitForSeconds(1);
        SpawnEnemy(EnemyType.Resistant);
        yield return new WaitForSeconds(2);
        for (int i = 0; i < wave / 2 - 2; i++ )
        {
            SpawnEnemy(EnemyType.Ice);
            yield return new WaitForSeconds(0.5f);
        }
        if (wave % 5 == 0 && wave >= 10)
        {
            for (int i = 0; i < wave; i++)
            {
                SpawnEnemy(EnemyType.Resistant);
                yield return new WaitForSeconds(0.1f);
            }
            for (int i = 0; i < wave / 4; i++)
            {
                SpawnEnemy(EnemyType.Earth);
                yield return new WaitForSeconds(0.2f);
            }
            for (int i = 0; i < wave; i++)
            {
                SpawnEnemy(EnemyType.Ice);
                yield return new WaitForSeconds(0.2f);
            }
        }
        if (wave >= 20)
        {
            for (int i = 0; i < wave / 1.5f; i++)
            {
                SpawnEnemy(EnemyType.Resistant);
                yield return new WaitForSeconds(0.03f);
            }
            for(int i = 0; i < wave / 2; i++)
            {
                SpawnEnemy(EnemyType.Ice);
                yield return new WaitForSeconds(0.03f);
            }
        }
        if (wave > 20 && wave % 10 == 0)
        {
            for (int i = 0; i < wave; i++)
            {
                SpawnEnemy(EnemyType.Earth);
            }
        }
        randomSpawn = false;
    }
}

