using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {
    void Start(){
    }

    //计算按钮的点击事件
    void OnClick()
    {
        SceneManager.LoadScene("WizardBattle");
    }
}
