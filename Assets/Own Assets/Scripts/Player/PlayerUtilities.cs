using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUtilities : MonoBehaviour
{
    [SerializeField] int money;

    public int GetMoneyAmount()
    {
        return money;
    }

    public void addMoney(int value)
    {
        money += value;
    }

    public void subtractMoney(int value)
    {
        if (money >= value) money -= value;
    }

    private void Start()
    {
        money = 0;
    }
}
