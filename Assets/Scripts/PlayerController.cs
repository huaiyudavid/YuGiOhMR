using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
	public Camera cam;

	// Use this for initialization
	void Start () {
		transform.Translate(0, 3, -10);
		if (!isLocalPlayer) {
			cam.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
		var y = Input.GetAxis("Jump") * Time.deltaTime * 3.0f;
		var xAngle = Input.GetAxis ("RotateX") * Time.deltaTime * 150.0f;
		var yAngle = -Input.GetAxis ("RotateY") * Time.deltaTime * 150.0f;
		transform.Rotate (yAngle, xAngle, 0);
		transform.Translate(x, y, z);
	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.blue;
	}
}
