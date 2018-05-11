using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 2.5f;

	void Start () {}
	
	void Update () {
		float tr_z = Input.GetAxis("Vertical");
		float tr_x = Input.GetAxis("Horizontal");
		Vector3 tmp = new Vector3(tr_x, 0, tr_z);
		transform.Translate(tmp.normalized * speed);
	}
}
