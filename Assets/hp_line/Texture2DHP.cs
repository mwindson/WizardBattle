using UnityEngine;
using System.Collections;

public class Texture2DHP: MonoBehaviour
{
	//前景贴图
	public Texture ForwardTexture;
	//背景贴图
	public Texture BackwardTexture;
	//目标对象
	public Transform Target;
	//水平偏移量
	public float OffSetX = 0.05F;
	//垂直偏移量
	public float OffSetY = 0.05F;
	
	//最大血量
	public int MaxHP = 100;
	//当前血量
	public int HP = 100;
	//血条宽度
	public int mWidth = 64;
	//血条高度
	public int mHeight = 5;
	
	//前景
	private Transform Forward;
	//背景
	private Transform Backward;
	
	void Start ()
	{
		//获得前景和背景
		Forward = transform.Find ("HPForward");
		Backward = transform.Find ("HPBackward");
		//设置前景、背景贴图
		Forward.GetComponent<GUITexture> ().texture = ForwardTexture;
		Backward.GetComponent<GUITexture> ().texture = BackwardTexture;
		//根据目标对象初始化血条位置
		UpdateLocation (Target, OffSetX, OffSetY);
	}
	
	void Update ()
	{
		UpdateLocation (Target, OffSetX, OffSetY);
		UpdateHP (HP);
	}
	
	//更新位置
	private void UpdateLocation (Transform mTransform, float mOffSetX, float mOffSetY)
	{
		//获取目标对象高度
		float mHight = Target.GetComponent<Collider> ().bounds.size.y;
		float mScale = Target.transform.localScale.y;
		mHight = mHight * mScale;
		//将三维坐标转化为二维坐标
		Vector3 mPos3d = new Vector3 (mTransform.position.x, mTransform.position.y + mHight, mTransform.position.z);
		Vector2 mPos2d = Camera.main.WorldToScreenPoint (mPos3d);
		//更新贴图的位置
		Forward.position = new Vector3 (mPos2d.x / Screen.width + mOffSetX, mPos2d.y / Screen.height + mOffSetY, 0);
		Backward.position = new Vector3 (mPos2d.x / Screen.width + mOffSetX, mPos2d.y / Screen.height + mOffSetY, 0);
	}
	
	//更新血量
	public void UpdateHP (int mValue)
	{
		if (mValue < 0 || mValue > MaxHP)
			return;
		SetGUITextureWidth (Forward.GetComponent<GUITexture> (),
		                   (int)(mWidth * (mValue / (double)MaxHP)));	
	}
	
	//设置贴图宽度
	private void SetGUITextureWidth (GUITexture mTexture, int mValue)
	{
		mTexture.pixelInset = new Rect (mTexture.pixelInset.x, mTexture.pixelInset.y,
		                             mValue, mTexture.pixelInset.height);
	}
}