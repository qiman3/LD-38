using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawer : MonoBehaviour {

	public int Countdown = 5;

	private float TimeTilStart;
	private bool IsPlayerSpawned = false;
	public Text ScoreCounter;


	public GameObject UIGO;
	public GameObject player;

	public float Score = 0;
	// Use this for initialization
	void Start () {
		TimeTilStart = Countdown;
		player.GetComponent<PlayerControl>().UIGO = UIGO;
		UIGO.SetActive(false);

	}

	public void OnTryAgain()
	{
		IsPlayerSpawned = false;
		TimeTilStart = Countdown;
		UIGO.SetActive(false);
		ObjectSpawner ObjSpw = gameObject.GetComponent<ObjectSpawner>();
		//ObjSpw.CurrentSpeed = ObjSpw.StartSpeed;
		ObjSpw.timeToSlow = Countdown;
		ObjSpw.IsSlowDown = true;
		ObjSpw.speedBeforeSlow = ObjSpw.CurrentSpeed;
		Score = 0;

	}
	
	// Update is called once per frame
	void Update () {
		TimeTilStart -= Time.deltaTime;
		if (TimeTilStart < 0 && !IsPlayerSpawned)
		{
			Instantiate(player);
			IsPlayerSpawned = true;
		}
		ScoreCounter.text = "Score: " + Mathf.RoundToInt(Score);
	}
}
