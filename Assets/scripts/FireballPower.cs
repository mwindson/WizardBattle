using UnityEngine;
using System.Collections;



public class FireballPower : MonoBehaviour
{

    private int power;
    public int Power
    {
        get
        {
            return this.power;
        }
    }

    public void setPower(int power)
    {
        this.power = power;
    }
}
