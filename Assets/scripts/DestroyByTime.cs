using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {
	public float maxtime;
	private float spawnTime;
	private bool canDestroy=true;
	
	void Start () {
		spawnTime = Time.time;
	}
	void OnCollisionEnter(Collision other) {
		canDestroy = false;
	}
	void OnCollisionExit(Collision other) {
		canDestroy = true;
	}
	void FixedUpdate () {
		if(Time.time >= spawnTime + maxtime && canDestroy == true){
			Destroy(this.gameObject);
		}
	}
}
