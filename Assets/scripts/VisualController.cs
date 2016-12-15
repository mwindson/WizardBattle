using UnityEngine;
using System.Collections;

public class VisualController : MonoBehaviour {
	public GameObject wizard;

	private static Vector3 offset = new Vector3(0f, 14f, -8f);

	private Vector3 initialPosition;

	void Start(){
		// initialPosition = new Vector3 (transform.position);
		initialPosition = new Vector3 (0f, 14f, -8f);
	}

	void Update(){
		GameObject player = GameObject.FindWithTag ("Player");
		if(player != null)
			transform.position = player.transform.position + offset;
	}

	public void resetTransform(){
		transform.position = initialPosition;
	}
}
