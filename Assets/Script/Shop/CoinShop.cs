using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinShop : MonoBehaviour
{
    public static CoinShop coinShopInstance;
    public TMP_Text textTotal;
    public TMP_Text textTotalDiamonds;

    void Awake()
    {
        if (coinShopInstance == null)
        {
            coinShopInstance = this;
        }
    }

    void OnEnable()
    {
        UpdateCoinTotal();
        UpdateDiamondTotal();
    }

    public void UpdateCoinTotal()
    {
        if (textTotal != null)
        {
            textTotal.text = "" + GameManager.totalCoins;
        }
    }

    public void UpdateDiamondTotal()
    {
        if (textTotalDiamonds != null)
        {
            textTotalDiamonds.text = "" + GameManager.totalDiamonds; // Hiển thị số lượng kim cương
        }
    }

    public bool SpendCoins(int amount)
    {
        if (GameManager.totalCoins >= amount)
        {
            GameManager.totalCoins -= amount;
            UpdateCoinTotal();
            return true;
        }
        return false;
    }
}
