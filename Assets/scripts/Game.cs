using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	public Text mainText;
    public GameObject visual;
	// public WizardController wizard;
	public GameObject AI_KnightPrefab, AI_MagicianPrefab, AI_PriestPrefab;
	public GameObject PlayerKnightPrefab, PlayerMagicianPrefab, PlayerPriestPrefab;
	public Text HPDisplay;
	public GameObject introText;
    public int enemyCount = 3;

	private static string chooseCharacterText = "" +
		"Choose number of your rivals:\n" +
		"A. 1\n" +
		"B. 3\n" +
		"C. 5\n";
	private static string chooseWizardText = "" +
		"Click On your character\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";
	private static string gameOverText = "GAME OVER.\nPress R to try again.";
	private static string victoryText = "Victory!\nPress R to play again.";
	private GameObject characterDisplay;

	// choosing_wizard表示用户正在选择游戏角色		---- 1
	// choosing_rivals表示用户正在选择对手       	---- 2
	// playing表示用户正在游戏中					---- 3
	// dead表示用户操作的角色死亡, 用户挑战失败		---- 4
	// victory表示用户消灭了所有其他对手,获得了胜利 	---- 5
	public int state;
	private int rivalCount; // 剩余敌人的数量

	// Use this for initialization
	void Start ()
	{
		characterDisplay = GameObject.FindWithTag ("CharacterDisplay");
		restart ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (state == 1) {
			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					string select_name = hit.collider.gameObject.name;
					selectPlayer (select_name);
				}
			}
			// if (Input.GetKeyDown (KeyCode.M)) {
			// 	spawnPlayer ("magician");
			// 	changeStateTo (2);
			// } else if (Input.GetKeyDown (KeyCode.K)) {
			// 	spawnPlayer ("knight");
			// 	changeStateTo (2);
			// } else if (Input.GetKeyDown (KeyCode.P)) {
			// 	spawnPlayer ("priest");
			// 	changeStateTo (2);
			// }
		} else if (state == 2) {
			if (Input.GetKeyDown (KeyCode.A)) {
				spawnAI (1);
				rivalCount = 1;
				changeStateTo (3);
			} else if (Input.GetKeyDown (KeyCode.B)) {
				spawnAI (3);
				rivalCount = 3;
				changeStateTo (3);
			} else if (Input.GetKeyDown (KeyCode.C)) {
				spawnAI (5);
				rivalCount = 5;
				changeStateTo (3);
			}
		} else if (state == 3) {
			// 玩家正在游戏中
//			if (rivalCount == 0) { // 对手全部死亡, 玩家获得胜利
//				changeStateTo (5);
//			}
		} else if (state == 4 || state == 5) {
			if (Input.GetKeyDown (KeyCode.R)) {
				restart ();
			}
		}
	}

	void changeStateTo (int state)
	{
		this.state = state;
		if (state == 1) {
			mainText.text = chooseWizardText;
			visual.GetComponent<VisualController> ().resetTransform ();
			characterDisplay.SetActive (true);
			introText.SetActive(true);
		} else if (state == 2) {
			mainText.text = chooseCharacterText;
			characterDisplay.SetActive (false);
			introText.SetActive(false);
		} else if (state == 3) {
			mainText.text = "";
		} else if (state == 4) {
			mainText.text = gameOverText;
		} else if (state == 5) {
			mainText.text = victoryText;
		}
	}

	void restart ()
	{
		// 清理AI
		GameObject[] AI_list = GameObject.FindGameObjectsWithTag ("AI");
		for (int i = 0; i < AI_list.Length; i++) {
			Destroy (AI_list [i]);
		}
		// 清理玩家
		GameObject[] Player_list = GameObject.FindGameObjectsWithTag ("Player");
		for (int i = 0; i < Player_list.Length; i++) {
			Destroy (Player_list [i]);
		}
        // 清理道具
        GameObject[] Potion_list = GameObject.FindGameObjectsWithTag("potion");
        for (int i = 0; i < Potion_list.Length; i++)
        {
            Destroy(Potion_list[i]);
        }
        // 改变游戏状态
        changeStateTo (1);
	}

	public void selectPlayer (string type)
	{ // 生成玩家的角色
		if (state == 1) {
			if (type == "magician") {
				GameObject player = Instantiate (PlayerMagicianPrefab, new Vector3 (), new Quaternion ()) as GameObject;
				player.GetComponent<WizardCommon> ().resetProperties ();
			} else if (type == "knight") {
				GameObject player = Instantiate (PlayerKnightPrefab, new Vector3 (), new Quaternion ()) as GameObject;
				player.GetComponent<WizardCommon> ().resetProperties ();
			} else if (type == "priest") {
				GameObject player = Instantiate (PlayerPriestPrefab, new Vector3 (), new Quaternion ()) as GameObject;
				player.GetComponent<WizardCommon> ().resetProperties ();
			} else {
				return;
			}
			changeStateTo (2);
		}
	}

	void spawnAI (int num)
	{
		Vector3 pos;
		Quaternion rotation = new Quaternion (0, 0, 0, 0);
		float x, z, max_z;
		Random.seed = (int)(Time.time);

		for (int i = 0; i < num; i++) {
			x = Random.Range (-15f, 15f);
			max_z = Mathf.Sqrt (225f - x * x);
			z = Random.Range (-max_z, max_z);
			while (Mathf.Abs(x) < 4F || Mathf.Abs(z) < 4F) {
				x = Random.Range (-15f, 15f);
				max_z = Mathf.Sqrt (225f - x * x);
				z = Random.Range (-max_z, max_z);
			}

			pos = new Vector3 (x, 0, z);
			// Instantiate (AI_PriestPrefab, pos, rotation);
			if(Random.value < 0.5)
				Instantiate(AI_MagicianPrefab, pos, rotation);
			else
				Instantiate(AI_PriestPrefab, pos, rotation);
		}
	}

   

	public void rival_dead (int num)
	{
		rivalCount -= num;
		if (rivalCount == 0) {

            Debug.Log("emeny appear");
            spawnAI(enemyCount);
            Debug.Log("ok");
			//do_victory ();
		}
	}

	public bool is_started ()
	{
		return state == 3;
	}

	public void do_gameover ()
	{
		changeStateTo (4);
	}

	public void do_victory ()
	{
		changeStateTo (5);
	}
}
