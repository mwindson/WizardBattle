using UnityEngine;
using System.Collections;

public class animationS : MonoBehaviour {
	Animation act;
	private bool hit = false;
	public bool isFiring = false;
	public Vector3 fireDirection;
	public float deadTime;
	private Transform transform;
	private Rigidbody rigidbody;
	// Use this for initialization
	private bool dead;
	void Start () {
		transform = GetComponent<Transform>();
		deadTime = 0;
		dead = false;
		act = GetComponent<Animation> ();
		rigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<WizardCommon> ().HP <= 0 && !dead) {
			act.CrossFade ("die", 0.2f);
			dead = true;
			deadTime = Time.time;
		}

		if (dead && Time.time - deadTime > 3) {
			Destroy (gameObject);
		}
		if (hit == false && !dead) {
			if (isFiring) {
				act.CrossFade ("attack",0.2F);
				transform.LookAt (transform.position + fireDirection);
			} else {

				if (Mathf.Abs (rigidbody.velocity.sqrMagnitude) > 0.001F) {
					if (Mathf.Abs (rigidbody.velocity.sqrMagnitude) >15F) {
						act.CrossFade("run", 0.2F);
					
					} else {
						act.CrossFade ("walk", 0.2F);
					}

					transform.LookAt (transform.position + rigidbody.velocity.normalized);
				} else {
					act.CrossFade ("idle", (float)0.2);
				}
			}
		}
	}

	void FixedUpdate(){

	}
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("fireball") && !dead) {
			hit = true;
			act.CrossFade("defend",(float)0.2);
		}
	}
	void OnCollisionExit(Collision other) {
		if (other.gameObject.CompareTag("fireball") && !dead) {
			hit = false;
			act.CrossFade("idle",(float)0.2);
			//Destroy(other.gameObject);
		}
	}
}
