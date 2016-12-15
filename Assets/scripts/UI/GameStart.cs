using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {
    GameObject canvas;
    void Start(){
        canvas = GameObject.Find("Canvas");
        canvas.SetActive(false);
    }

    //计算按钮的点击事件
    void OnClick()
    {
        Debug.Log("Game Start OnClick button");
        SceneManager.LoadScene("WizardBattle");
        canvas.SetActive(true);
    }
}
