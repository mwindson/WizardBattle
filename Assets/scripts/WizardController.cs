using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WizardController : MonoBehaviour {
	public Text HPDisplay;
	private Game game;

	private Vector3 click_point;
	private WizardCommon wizardCommon;

	void Start(){
		wizardCommon = GetComponent<WizardCommon> ();
		game = GameObject.FindWithTag ("GameController").GetComponent<Game>();
		HPDisplay = game.HPDisplay;
	}

	void Update(){
		if (Input.GetButton ("Fire1") && game.is_started()) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast(ray, out hit);
			click_point = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, hit.distance));
			GetComponent<FireShoot>().Shoot(click_point);
		}
	}

	void FixedUpdate(){
		// 根据玩家输入移动术士
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement =  new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;
		if (game.is_started ()) {
			GetComponent<WizardCommon>().move (movement);
		}

		// 更新HP的显示
		if(wizardCommon.HP > 0)
			HPDisplay.text = "HP: " + wizardCommon.HP +  " Score: " + game.score;
		else
			HPDisplay.text = "Dead";

		// 如果玩家死亡, 游戏进入gameover阶段
		if (game.is_started () && wizardCommon.HP == 0) {
			game.do_gameover();
		}
	}
}
