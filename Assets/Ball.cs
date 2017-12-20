using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour {

	public float speed = 0.1f;
	public float drag = 0.1f;
	public float deadZone =0.0015f;

	public Vector3 position;
	public Vector3 velocity;
	public Vector3 acceleration;

	public float speedMulti = 1f;
	public float speedUpRate = 0.01f;

	Camera cam;
	float height;
	float width;

	public Text score;
	public int p1 = 0;
	public int p2 = 0;

	// Use this for initialization
	void Start () {
			
		Camera cam = Camera.main;
		height = 2 * cam.orthographicSize;
		width = height * cam.aspect;

		recenter ();

		score = GameObject.Find ("Score").GetComponent<Text> ();//Goes through the hierarchy, finds score and gets the text component
	}
	
	// Update is called once per frame
	void Update () {

		Move ();

	}

	void Move(){

		velocity = velocity + acceleration;
		position = position + velocity * speed;

		position = bounds (position);

		this.gameObject.transform.position = position;
	}

	void recenter(){

		acceleration = Vector3.zero;

		if (Random.value > 0.5f)
			velocity = new Vector3 (0.05f, Random.Range (0, .05f), 0f);
		else
			velocity = new Vector3 (-0.05f, Random.Range (0, 0.05f), 0f);

		position = Vector3.zero;

		speedMulti = 1;
	}

	Vector3 bounds(Vector3 pos){

		//when you hit a wall
		if (pos.y > height / 2) {
			velocity = new Vector3 (velocity.x, -velocity.y, 0);
		} else if (pos.y < -height / 2) {
			velocity = new Vector3 (velocity.x, -velocity.y, 0);
		}

		//when you score
		else if (pos.x > width / 2) {

			p2++;
			score.text = p2 + " - " + p1;
			recenter ();
			return Vector3.zero;
		}
		else if (pos.x < -width / 2) {

			p1++;
			score.text = p2 + " - " + p1;
			recenter ();
			return Vector3.zero;
		}

		return pos;
	}

	public void hit (Vector3 velo){
		velocity = velo * speedMulti;
		speedMulti = speedMulti + speedUpRate;
	}
}
