using UnityEngine;
using System.Collections;

public class FireShoot : MonoBehaviour {

	public GameObject fireball;
	public float fireball_speed;
	public float fireRate;
	private bool locked = false;

	private float nextFire = 0.0f;
	private Vector3 spawn_pos;
	private float shottime = -1;
	private Vector3 target;
	private Vector3 direction;

	public void Shoot(Vector3 target){

		if (Time.time > nextFire && shottime == -1) {
			shottime = Time.time;
			nextFire = Time.time + fireRate;
			target.y = 0;
			this.target = target;
			direction = (target - transform.position).normalized;
		}

	}
	void Update(){
		locked = GetComponent<WizardCommon> ().locked;
		// Debug.Log ("shottime" + shottime);
		if (shottime != -1 && shottime + 0.5 < Time.time) {
			shottime = -1;
			spawn_pos = transform.position + 1.2F * direction;
			GameObject shot = Instantiate (fireball, spawn_pos, new Quaternion ()) as GameObject;
			shot.GetComponent<Rigidbody> ().velocity = direction * fireball_speed;
			GetComponent<animationS>().isFiring = false;
		} else if (shottime != -1) {
			direction = (target - transform.position).normalized;
			GetComponent<animationS>().isFiring = true;
			GetComponent<animationS>().fireDirection = direction;
		}
	}

}
