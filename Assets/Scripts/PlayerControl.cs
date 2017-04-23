using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

	public float playerAcc = 20;
	public float maxSpeed = 10;
	public float jumpSpeed = 10;
	public float scoreSpeed = 10;


	private ObjectSpawner objSpawn;
	private Rigidbody2D PlayerRB2D;
	private PlayerSpawer PlayerSpawn;


	private float horz;

	public GameObject UIGO;

	private bool IsJumping;
	private bool IsGrounded;
	private float horzExtent;
	// Use this for initialization
	void Start () {
		objSpawn = GameObject.FindGameObjectWithTag("GameController").GetComponent<ObjectSpawner>();
		PlayerSpawn = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerSpawer>();
		PlayerRB2D = GetComponent<Rigidbody2D>();
	}
	
	void CheckGround() {
		RaycastHit2D hit = Physics2D.Raycast(transform.position+Vector3.down * GetComponent<Collider2D>().bounds.extents.y, Vector2.down,0.1f);
		if (hit.collider != null)
		{
			IsGrounded = true;
		}
		else {
			IsGrounded = false;
		}
	}

	// Update is called once per frame
	void Update () {
		horzExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
		CheckGround();

		if (Input.GetAxis("Jump") > 0.1f)
		{
			IsJumping = true;
		}
		else
		{
			IsJumping = false;
		}

		if (IsJumping && IsGrounded)
		{
			PlayerRB2D.velocity += Vector2.up * jumpSpeed;
		}

		horz = Input.GetAxis("Horizontal");
		Vector2 next = ((Vector2.right * playerAcc * horz) + (Vector2.left * objSpawn.CurrentSpeed))*Time.deltaTime;
		PlayerRB2D.velocity += next;
		float scale = Mathf.Clamp(PlayerRB2D.velocity.magnitude,-maxSpeed,maxSpeed);
		PlayerRB2D.velocity = PlayerRB2D.velocity.normalized * scale;

		Vector2 newPos = new Vector2(Mathf.Min(transform.position.x,horzExtent),transform.position.y);
		transform.position = newPos;
		if (transform.position.x < -(horzExtent+0.5) || transform.position.y < -(Camera.main.orthographicSize + 0.5))
		{
			Die();
		}
		PlayerSpawn.Score += Time.deltaTime * scoreSpeed;
	}

	public void Die()
	{
		Debug.Log("You are dead, no big suprise!");
		UIGO.SetActive(true);
		int Score = Mathf.RoundToInt(PlayerSpawn.Score);
		foreach (Transform child in UIGO.transform)
		{
			if (child.tag == "Score")
			{
				child.GetComponent<Text>().text = "You Scored: " + Score.ToString() + " points";
			}
		}
		Destroy(gameObject);
	}
}
