using UnityEngine;
using System.Collections;

public class strongAI : MonoBehaviour {
	private float nextmove = 0F;
	private Vector3 moveDirection;
	// Use this for initialization
	private GameObject player ;
	void Start () {
		Random.seed = (int)Mathf.Floor(Time.time);
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (player) {
			GetComponent<FireShoot> ().Shoot (player.transform.position + player.GetComponent<Rigidbody>().velocity);
		}
		
		if (Time.time > nextmove) {
			nextmove = Time.time+0.5F;
			float moveX = Random.value-0.5F;
			float moveZ = Random.value-0.5F;
			Vector3 direction = (new Vector3(moveX,0F,moveZ)).normalized;
			moveDirection = direction;
			// Debug.Log ("want to move"+moveDirection);
			if ((transform.position).magnitude > 13){
				float mx = transform.position.x < 0 ? 1F:-1F;
				float mz = transform.position.z < 0 ? 1F:-1F;
				moveDirection = (new Vector3(Mathf.Abs(moveDirection.x)*mx,0,Mathf.Abs(moveDirection.z)*mz)).normalized;
			}
			
		}
		// Debug.Log ("move"+moveDirection);
		GetComponent<WizardCommon>().move (moveDirection);
	}
}
