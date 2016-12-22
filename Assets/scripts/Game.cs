using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

class Texts
{
    public static readonly string CHOOSE_DIFFICULTY = "" +
            "Choose number of your rivals:\n" +
            "A. 1\n" +
            "B. 3\n" +
            "C. 5\n";
    public static readonly string CHOOSE_WIZARD = "" +
       "Click On your character\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";
    public static readonly string LOSE = "GAME OVER.\nPress R to try again.";
    public static readonly string VICTORY = "Victory!\nPress R to play again.";
}


// 用于管理游戏生命周期的类. 使用状态机的思路进行实现.
public class Game : MonoBehaviour
{
    public enum Diffculty
    {
        easy, normal, hard
    }
    public enum State
    {
        // 正在选择游戏角色
        CHOOSING_WIZARD,
        // 正在选择难度(AI数量)
        CHOOSING_DIFFICULTY,
        // 正在游戏中
        PLAYING,
        // 用户操作的角色死亡, 用户挑战失败
        LOSE,
        // 用户消灭了所有其他对手,获得了胜利
        VICTORY,
    }

    // 当前游戏的阶段
    public State state;
    // 当前游戏波数
    public int wave;
    // 当前的游戏难度
    Diffculty difficulty;
    // 当前剩下的敌人数量
    private int rivalCount;
    // 当前的分数
    public int score = 0;

    // UI
    public GameObject scoreUI;
    public GameObject chooseUI;
    public GameObject diffcultUI;
    public GameObject gameOverUI;
    // 摄像头
    public GameObject visual;
    // 选取角色的对象
    public GameObject characterDisplay;
    // prefabs
    public GameObject AI_KnightPrefab, AI_MagicianPrefab, AI_PriestPrefab;
    public GameObject PlayerKnightPrefab, PlayerMagicianPrefab, PlayerPriestPrefab;

    // Use this for initialization
    void Start()
    {
        restart();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.CHOOSING_WIZARD)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    string select_name = hit.collider.gameObject.name;
                    selectPlayer(select_name);
                }
            }
        }
        else if (state == State.CHOOSING_DIFFICULTY)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                difficulty = Diffculty.easy;
                changeStateTo(State.PLAYING);
                startWave(0);
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                difficulty = Diffculty.normal;
                changeStateTo(State.PLAYING);
                startWave(0);
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                difficulty = Diffculty.hard;
                changeStateTo(State.PLAYING);
                startWave(0);
            }
        }
        else if (state == State.PLAYING)
        {

            // 玩家正在游戏中
            //			if (rivalCount == 0) { // 对手全部死亡, 玩家获得胜利
            //				changeStateTo (5);
            //			}
        }
        else if (state == State.LOSE || state == State.VICTORY)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                restart();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.UnloadScene("WizardBattle");
                SceneManager.LoadScene("StartUI");
            }
        }
    }

    // 根据状态控制UI
    void changeStateTo(State state)
    {
        this.state = state;
        if (state == State.CHOOSING_WIZARD)
        {
            visual.GetComponent<VisualController>().resetTransform();
            characterDisplay.SetActive(true);
            chooseUI.SetActive(true);
            scoreUI.SetActive(false);
            diffcultUI.SetActive(false);
            gameOverUI.SetActive(false);
        }
        else if (state == State.CHOOSING_DIFFICULTY)
        {
            characterDisplay.SetActive(false);
            chooseUI.SetActive(false);
            scoreUI.SetActive(true);
            diffcultUI.SetActive(true);
        }
        else if (state == State.PLAYING)
        {
            diffcultUI.SetActive(false);
            scoreUI.SetActive(true);
        }
        else if (state == State.LOSE)
        {
            gameOverUI.SetActive(true);
            gameOverUI.GetComponentInChildren<UILabel>().text += " 得分：" + this.score + "\n按R重新游戏\n按Q返回主菜单";
        }
        else if (state == State.VICTORY)
        {
        }
    }

    void restart()
    {
        // 清理AI
        GameObject[] AI_list = GameObject.FindGameObjectsWithTag("AI");
        for (int i = 0; i < AI_list.Length; i++)
        {
            Destroy(AI_list[i]);
        }
        // 清理玩家
        GameObject[] Player_list = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < Player_list.Length; i++)
        {
            Destroy(Player_list[i]);
        }
        // 清理道具
        GameObject[] Potion_list = GameObject.FindGameObjectsWithTag("potion");
        for (int i = 0; i < Potion_list.Length; i++)
        {
            Destroy(Potion_list[i]);
        }
        // 改变游戏状态
        changeStateTo(State.CHOOSING_WIZARD);
    }

    public void selectPlayer(string type)
    { // 生成玩家的角色
        if (state == State.CHOOSING_WIZARD)
        {
            if (type == "magician")
            {
                GameObject player = Instantiate(PlayerMagicianPrefab, new Vector3(), new Quaternion()) as GameObject;
                player.GetComponent<WizardCommon>().resetProperties();
            }
            else if (type == "knight")
            {
                GameObject player = Instantiate(PlayerKnightPrefab, new Vector3(), new Quaternion()) as GameObject;
                player.GetComponent<WizardCommon>().resetProperties();
            }
            else if (type == "priest")
            {
                GameObject player = Instantiate(PlayerPriestPrefab, new Vector3(), new Quaternion()) as GameObject;
                player.GetComponent<WizardCommon>().resetProperties();
            }
            else
            {
                return;
            }
            changeStateTo(State.CHOOSING_DIFFICULTY);
        }
    }

    void spawnAI(int num)
    {
        float x, z, max_z;
        Random.InitState((int)Time.time);

        for (int i = 0; i < num; i++)
        {
            // 随机选择出生地点
            do
            {
                x = Random.Range(-15f, 15f);
                max_z = Mathf.Sqrt(225f - x * x);
                z = Random.Range(-max_z, max_z);
            } while (Mathf.Abs(x) < 4F || Mathf.Abs(z) < 4F);

            // 随机选择一种类型
            GameObject AIPrefab = Random.value < 0.5 ? AI_MagicianPrefab : AI_PriestPrefab;
            GameObject AI = Instantiate(AIPrefab, new Vector3(x, 0, z), new Quaternion(0, 0, 0, 0));
            WizardCommon wizard = AI.GetComponent<WizardCommon>();
            // 根据当前波数来提升AI的战斗力
            wizard.initialAttack += wave;
            wizard.initialHP += wave * 2;
        }
        rivalCount += num;
    }

    // 每次敌人死亡的时候, 该函数将被调用
    public void rivalDead(int num)
    {
        rivalCount -= num;
        score += (wave + 1) * num;
        if (rivalCount == 0)
        {
            // Debug.Log("emeny appear");
            // Debug.Log("ok");
            // 开始新一波的AI
            startWave(wave + 1);
        }
    }

    public void startWave(int wave)
    {
        this.wave = wave;
        int num;
        if (difficulty == Diffculty.easy)
        {
            num = 1;
        }
        else if (difficulty == Diffculty.normal)
        {
            num = 3;
        }
        else
        {
            num = 5;
        }
        spawnAI(num);
    }

    public bool is_started()
    {
        return state == State.PLAYING;
    }

    public void do_gameover()
    {
        changeStateTo(State.LOSE);
    }

    public void do_victory()
    {
        changeStateTo(State.VICTORY);
    }
}
