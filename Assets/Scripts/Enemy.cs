using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public GameObject TrackObject;
	private Track track;
	public GameObject Particles;
	private ParticleSystem ps;
	private GameObject particles;

    public static float GlobalDamageMult = 1;
    public static float TotalEnemies = 0;

	private float currentTime = 0;
	public float Speed = 1;
	public float Health = 10;
	public float MaxHealth = 10;
	public int CurrentTrackDestination = 1;
	public int KillWorth = 10;

    public float DistanceMoved = 0;

	public GameObject HealthBar;
	public GameObject DamageBar;
	// Use this for initialization
	void Start () {
		track = TrackObject.GetComponent<Track> ();
		transform.position = track.TrackPoints [0];
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (new Vector2 (transform.position.x, transform.position.y), track.TrackPoints [CurrentTrackDestination]) < 0.01f) {
			CurrentTrackDestination++;
			if(CurrentTrackDestination >= track.TrackPoints.Length)
			{
				OnKill(false);
			}
			currentTime = 0;
		}
		currentTime += Time.deltaTime;
		transform.position = Vector2.MoveTowards (transform.position, track.TrackPoints [CurrentTrackDestination], Speed * Time.deltaTime);
        DistanceMoved += Speed * Time.deltaTime;

		HealthBar.transform.localScale = new Vector3 (Health / MaxHealth, HealthBar.transform.localScale.y, HealthBar.transform.localScale.z);
		HealthBar.transform.localPosition = new Vector3 ((Health / (MaxHealth * 4)) - 0.25f, .25f, 0);
	}
	public void TakeDamage(float amount)
	{
		Health -= amount * Enemy.GlobalDamageMult;
		if (Health <= 0) {
			OnKill(true);
		}
	}
	public void GetHealed(float amount)
	{
		Health += amount;
		if (Health > MaxHealth)
			Health = MaxHealth;
	}
	public virtual void OnKill(bool byPlayer)
	{
		particles = Instantiate (Particles);
		particles.GetComponent<ParticleSystem> ().Play ();
		particles.transform.position = transform.position;
		if (byPlayer) {
			MoneyManager.Cash += KillWorth * MoneyManager.KillCashMult;
		}
		else {
			LivesManager.Lives -= 1;
		}
		DestroyThis ();
	}
	void DestroyThis()
	{
		WaveGenerator.WorldEnemies.Remove (gameObject.name);
		Destroy (gameObject);
	}
}
