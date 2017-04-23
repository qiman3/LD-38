using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour {
    public float speed = 1f;
	private float horzExtent;
	// Use this for initialization
	void Start () {
		horzExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 translateAmount = Vector2.left * speed * Time.deltaTime;
		transform.Translate(translateAmount);

		if (transform.position.x < -(horzExtent+ 0.5f))
		{ 
			Destroy(this.transform.parent.gameObject);
		}
	}
}
