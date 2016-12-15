using UnityEngine;
using System.Collections;

public class DestroyWithDistance : MonoBehaviour {
	public float maxDistance;
	private float removetime = 0F;
	private bool coExit = false; 
	private bool hitPlayer = false;

	// Update is called once per frame
	void Update () {
		if ((transform.position - new Vector3 (0, 0, 0)).magnitude >= maxDistance) {
			Destroy (gameObject);
		}
	}
	void FixedUpdate() {
		if (removetime != 0 && Time.time > removetime)
			Destroy(gameObject);
	}
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag ("fireball") && hitPlayer == false) {
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			Destroy(GetComponent<SphereCollider>());
			//	Destroy (gameObject);
			removetime = Time.time + 0.5F;;
		}
		if (other.gameObject.CompareTag ("Player") || other.gameObject.CompareTag("AI")) {
			hitPlayer = true;
		}

		//if (removetime ==0) { 
		//	removetime = Time.time + 0.5F;
		//}
		//coExit = false;
	}
	void OnCollisionExit(Collision other)
	{
		if (other.gameObject.CompareTag ("Player") || other.gameObject.CompareTag("AI")) {
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			Destroy(GetComponent<SphereCollider>());
			//	Destroy (gameObject);
			removetime = Time.time + 0.5F;
		}

	}
}
