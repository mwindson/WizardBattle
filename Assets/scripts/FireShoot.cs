﻿using UnityEngine;
using System.Collections;

// 用来控制AI/玩家发射火球的脚本
public class FireShoot : MonoBehaviour
{
    // 发射的火球的类型
    public GameObject fireballType;
    // 火球发射的速度
    public float fireballSpeed;
    // 火球发射的间隔时间
    public float fireInterval;

    // private bool locked = false;

    private float nextFire = 0.0f;

    // 上一次发射火球的时间, -1表示曾为发射过火球
    private float lastShotTime = -1;

    private Vector3 target;
    private Vector3 direction;

    public void Shoot(Vector3 target)
    {
        if (Time.time > nextFire && lastShotTime == -1)
        {
            lastShotTime = Time.time;
            nextFire = Time.time + fireInterval;
            target.y = 0;
            this.target = target;
            direction = (target - transform.position).normalized;
        }

    }
    void Update()
    {
        // locked = GetComponent<WizardCommon>().locked;
        // Debug.Log ("lastShotTime" + lastShotTime);
        if (lastShotTime != -1 && lastShotTime + 0.5f < Time.time) // 这里的0.5表示"动作前摇"
        {
            lastShotTime = -1;

            Vector3 spawnPos = transform.position + 2f * direction; // 火球生成的位置
            spawnPos.y += 1f + 0.1f; // 往上提升火球半径的距离
            GameObject shot = Instantiate(fireballType, spawnPos, new Quaternion()); // 生成火球
            shot.GetComponent<Rigidbody>().velocity = direction * fireballSpeed; // 设置速度

            // 获取攻击者的攻击力, 记录到火球的power中
            int wizardAttack = GetComponent<WizardCommon>().attack;
            shot.GetComponent<FireballPower>().setPower(wizardAttack);

            GetComponent<animationS>().isFiring = false;
        }
        else if (lastShotTime != -1)
        {
            direction = (target - transform.position).normalized;
            GetComponent<animationS>().isFiring = true;
            GetComponent<animationS>().fireDirection = direction;
        }
    }

}
