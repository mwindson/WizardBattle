using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void OnClick()
    {
        Debug.Log("Game Start OnClick button");
        Application.Quit();
    }
}
