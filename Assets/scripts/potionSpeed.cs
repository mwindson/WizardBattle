using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionSpeed : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            WizardCommon wizardcommon = other.gameObject.GetComponent<WizardCommon>();
            wizardcommon.speed += 3;
            wizardcommon.speedBuffTime = 500;
            Destroy(this.gameObject);
        }
    }
}
