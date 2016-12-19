using UnityEngine;
using System.Collections;

public class WizardCommon : MonoBehaviour {
	public float initialHP;
	public int speed;
	private float damageRate=0.3F;

	private float nextDamageTime = 0.0f;
	public float HP;
    private float[] probs;
    public GameObject[] potion_objs;
    public bool locked;
	public Texture2D blood_red;
	public Texture2D blood_black;
    public GameObject potionHP, potionSpeed, potionDMG;

	private bool dead;

	void Start(){
		HP = initialHP;
		locked = false;
		dead = false;
        probs = new float[3] { 0.5F, 0.4F, 0.2F };
        potion_objs = new GameObject[3] { potionHP, potionSpeed, potionDMG };
    }

	void FixedUpdate(){
		if ((transform.position - new Vector3 (0, 0, 0)).magnitude > 15) {
			if (Time.time > nextDamageTime) {
				nextDamageTime = Time.time + damageRate;
				minusHP(1);
			}
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("fireball")) {
			minusHP(1);
			// Debug.Log("HP is now " + HP);
			locked = true;
		}
	}

	void OnCollisionExit(Collision other)
	{
		if (other.gameObject.CompareTag("fireball")) {
			locked = false;
		}
	}

	public void move(Vector3 movement){
		if(!locked)
			GetComponent<Rigidbody> ().velocity = speed * movement;
	}

	public void resetHP(){
		HP = initialHP;
	}
    
    public int Choose(float[] probs)
    {
        float total = 0;

        for (int i=0;i<probs.Length;i++)
        {
            total += probs[i];
        }

        float randomPoint = Random.value * total;

        for (int j = 0; j < probs.Length; j++)
        {
            if (randomPoint < probs[j])
                return j;
            else
                randomPoint -= probs[j];
        }

        return probs.Length - 1;
    }
    public void minusHP(int minus){
		HP -= minus;
		if (HP < 0 ) HP = 0;
		if (HP == 0 && !dead && this.CompareTag ("AI")) { // 如果一个AI死亡, 则执行game中的rival_dead
			dead = true;
            if (Random.value <= 0.8)
            {
                Instantiate(potion_objs[Choose(probs)], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            }
			Game game = GameObject.FindWithTag ("GameController").GetComponent<Game> ();
			game.rival_dead (1);
		}
	}

	void OnGUI()
	{
		//得到NPC头顶在3D世界中的坐标
		//默认NPC坐标点在脚底下，所以这里加上npcHeight它模型的高度即可
		Vector3 worldPosition = new Vector3 (transform.position.x , transform.position.y + 4F,transform.position.z);
		//根据NPC头顶的3D坐标换算成它在2D屏幕中的坐标
		Vector2 position = Camera.main.WorldToScreenPoint (worldPosition);
		//得到真实NPC头顶的2D坐标 
		position = new Vector2 (position.x, Screen.height - position.y);
		//注解2
		//计算出血条的宽高
		Vector2 bloodSize = GUI.skin.label.CalcSize (new GUIContent(blood_red));
		
		//通过血值计算红色血条显示区域
		float blood_width = (blood_red.width * HP/initialHP);
		//先绘制黑色血条
		GUI.DrawTexture(new Rect(position.x - (bloodSize.x/2),position.y - bloodSize.y ,bloodSize.x,bloodSize.y),blood_black);
		//在绘制红色血条
		GUI.DrawTexture(new Rect(position.x - (bloodSize.x/2),position.y - bloodSize.y ,blood_width,bloodSize.y),blood_red);
		
		//注解3
		//计算NPC名称的宽高
		Vector2 nameSize = GUI.skin.label.CalcSize (new GUIContent(name));
		//设置显示颜色为黄色
		GUI.color  = Color.green;
		//绘制NPC名称
		GUI.Label(new Rect(position.x - (nameSize.x/2),position.y - nameSize.y - bloodSize.y ,nameSize.x,nameSize.y), name);
		string bloodString = HP + "/" + initialHP;
		Vector2 bloodStringSize = GUI.skin.label.CalcSize (new GUIContent(bloodString));

		GUI.Label(new Rect(position.x-bloodStringSize.x/2F , position.y - bloodStringSize.y/2F-bloodSize.y/2f,bloodStringSize.x, bloodStringSize.y), bloodString);
		
	}

}
