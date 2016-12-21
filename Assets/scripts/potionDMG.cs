using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionDMG : MonoBehaviour {
    private WizardCommon AIWC;

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
            GameObject[] AI_list = GameObject.FindGameObjectsWithTag("AI");
            for(int i = 0; i < AI_list.Length; i++)
            {
                AIWC = AI_list[i].GetComponent<WizardCommon>();
                // AIWC.getDmg += 5;
                AIWC.attackBuffTime = 500;
            }

            Destroy(this.gameObject);
        }
    }
}
