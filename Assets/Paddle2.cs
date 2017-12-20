using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paddle2 : MonoBehaviour {

	public float speed = 1;
	public float drag = 0.1f;
	public float deadZone = 0.0015f;

	Vector3 position;
	Vector3 velocity;
	Vector3 acceleration;

	Camera cam;
	float height;
	float width;

	public float maxVelocity = 0.4f;
	// Use this for initialization
	void Start () {

		Camera cam = Camera.main;
		height = 2 * cam.orthographicSize;
		width = height * cam.aspect;

		acceleration = Vector3.zero;
		velocity = Vector3.zero;
		position = new Vector3 (-width / 2, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {

		ProcessInputs ();

		Move ();
	}

	void ProcessInputs(){

		if (Input.GetAxisRaw ("Vertical2") > 0) {
			acceleration = Vector3.up * Time.deltaTime * speed;	//Time.deltaTime is the time taken to compute the last frame. This makes up for the difference in framerate.	
		}

		if (Input.GetAxisRaw ("Vertical2") < 0) {
			acceleration = Vector3.up * Time.deltaTime * -speed;//Alternatively Vector3.down and no - sign
		}

		if (Input.GetAxisRaw ("Vertical2") == 0) {
			acceleration = Vector3.zero;
		}
	}

	void Move(){

		velocity = velocity + acceleration;

		if (Mathf.Abs(velocity.y) > maxVelocity) {
			velocity.y = maxVelocity * Mathf.Sign (velocity.y);
		} 

		position = position + velocity;

		position = worldWrap (position);

		this.gameObject.transform.position = position;
	}

	Vector3 worldWrap(Vector3 pos){

		if (pos.y > height / 2) {
			pos.y = -height / 2;
		} else if (pos.y < -height / 2) {
			pos.y = height / 2;
		}

		return pos;
	}

	void OnCollisionEnter2D(Collision2D coll){//When a collider enters another collider this function is automatically called

		if (coll.gameObject.tag == "Ball") {//Checks to see that the collider has a tag called ball

			var ball = coll.gameObject.GetComponent<Ball> ();//Looks for the parent of the collider and gets the component ball attached to the object
			ball.hit (new Vector3 (-ball.velocity.x, ball.velocity.y, 0f));

		}
	}
}
