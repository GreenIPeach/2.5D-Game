using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private CharacterController CharController;
	private float HorizontalDir;
	private float VerticalVeleocity;
	private float gravity = 30.0f;
	private float speed = 5.0f;
	private float jumpForce = 10.0f;
	private Vector3 moveVector;
	private bool secondJump = false;
	// Use this for initialization
	void Start () {
		CharController = GetComponent<CharacterController> ();

	}
	
	// Update is called once per frame
	void Update () {
		IsControllerGrounded();
		HorizontalDir = Input.GetAxis ("Horizontal") * speed;
		Debug.Log (HorizontalDir);
		if (IsControllerGrounded()) {
			VerticalVeleocity = 0;
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)){
				Debug.Log("Space!");
				VerticalVeleocity = jumpForce;
				secondJump = true;
				}
		} else {
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)){
				if (secondJump) {
					VerticalVeleocity = jumpForce;
					secondJump = false;
				}
			}
			VerticalVeleocity -= gravity * Time.deltaTime;
		}

		moveVector = new Vector3 (HorizontalDir, VerticalVeleocity, 0);
		CharController.Move(moveVector* Time.deltaTime);
	}

	private bool IsControllerGrounded()
	{
		Vector3 leftRayStart;
		Vector3 rightRayStart;

		leftRayStart = CharController.bounds.center;
		rightRayStart = CharController.bounds.center;

		leftRayStart.x -= CharController.bounds.extents.x;
		rightRayStart.x += CharController.bounds.extents.x;

		Debug.DrawRay(leftRayStart,Vector3.down,Color.red);
		Debug.DrawRay(rightRayStart,Vector3.down,Color.green);

		if (Physics.Raycast (leftRayStart, Vector3.down, (CharController.height / 2) + 0.1f))
			return true;

		if (Physics.Raycast (rightRayStart, Vector3.down, (CharController.height / 2) + 0.1f))
			return true;

		return false;
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		// Walljump
		if (CharController.collisionFlags == CollisionFlags.Sides) {
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)){
				moveVector = hit.normal * speed;
				moveVector.y = jumpForce;
				secondJump = true;
			}
		}

		// Einsammeln von Münzen
		switch (hit.gameObject.tag) 
		{
		case "Coin":
			Destroy(hit.gameObject);
			LevelManager.Instance.CollectCoin();
			break;
		case "JumpPad":
			VerticalVeleocity = jumpForce * 2f;
			break;
		case "Teleporter":
			transform.position = hit.transform.GetChild (0).position;
			break;
		case "Win":
			LevelManager.Instance.win ();
			break;
		}


	}


}



