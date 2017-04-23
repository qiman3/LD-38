using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
	public GameObject[] spawnList;
	public float Acceleration;
	public int StartSpeed = 3;
	public float CurrentSpeed;
	private float TimeTilSpawnNext;
	private int spawnCounter = 0;
	public bool Isflipped = true;
	public bool IsSlowDown = false;

	public int timeToSlow;
	private float timeSinceSlow = 0;
	public float speedBeforeSlow;

	// Use this for initialization
	void Start()
	{
		CurrentSpeed = StartSpeed;
		TimeTilSpawnNext = 1 / CurrentSpeed;
	}

	// Update is called once per frame
	void Update()
	{
		if (IsSlowDown)
		{
			timeSinceSlow += Time.deltaTime;
			float percent = timeSinceSlow / timeToSlow;
			if (percent >= 1f)
			{
				IsSlowDown = false;
			}

			CurrentSpeed = Mathf.MoveTowards(speedBeforeSlow, StartSpeed, percent);
		}
		else
		{
			CurrentSpeed += Acceleration * Time.deltaTime;
		}
		TimeTilSpawnNext -= Time.deltaTime;
		if (TimeTilSpawnNext < 0)
		{
			if (!Isflipped)
			{
				float horzExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
				Vector2 spawnPos = new Vector2(horzExtent + 0.5f, 0);
				GameObject curGO = Instantiate(spawnList[spawnCounter], spawnPos, Quaternion.identity);

				foreach (Transform block in curGO.transform)
				{
					BlockMove curBM = block.GetComponent<BlockMove>();
					curBM.speed = CurrentSpeed;
				}
				spawnCounter += 1;
				if (spawnCounter > spawnList.Length-1)
				{
					spawnCounter -= 2;
					Isflipped = !Isflipped;
				}
			}
			else
			{
				float horzExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
				Vector2 spawnPos = new Vector2(horzExtent + 0.5f, 0);
				GameObject curGO = Instantiate(spawnList[spawnCounter], spawnPos, Quaternion.identity);
				curGO.transform.Rotate(transform.forward, 180f);
				foreach (Transform block in curGO.transform)
				{
					BlockMove curBM = block.GetComponent<BlockMove>();
					SpriteRenderer curSR = block.GetComponent<SpriteRenderer>();
					curSR.flipY = !curSR.flipY;
					curBM.speed = CurrentSpeed;
					block.transform.Rotate(transform.forward, 180f);
				}

				spawnCounter -= 1;
				if (spawnCounter < 0)
				{
					spawnCounter += 2;
					Isflipped = !Isflipped;
				}
			}


			TimeTilSpawnNext = 1 / CurrentSpeed;
		}
	}
}
