using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHp : MonoBehaviour {
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
            WizardCommon player = other.gameObject.GetComponent<WizardCommon>();
            if (player.HP < player.initialHP)
            {
                if (player.HP + 5 < player.initialHP)
                {
                    player.HP = player.HP + 5;
                }
                else
                {
                    player.HP = player.initialHP;
                }
                Destroy(this.gameObject);
            }
        }
    }
}
