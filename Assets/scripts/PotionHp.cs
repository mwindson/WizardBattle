using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionHp : MonoBehaviour {
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
            if (wizardcommon.HP < wizardcommon.initialHP)
            {
                if (wizardcommon.HP + 5 < wizardcommon.initialHP)
                {
                    wizardcommon.HP = wizardcommon.HP + 5;
                }
                else
                {
                    wizardcommon.HP = wizardcommon.initialHP;
                }
                Destroy(this.gameObject);
            }
        }
    }
}
