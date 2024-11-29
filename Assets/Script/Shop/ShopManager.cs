using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject uiDetail;
    public GameObject exchangeUI;
    public GameObject checkYesNo;
    public GameObject messageUI;
    AudioManager audioManager;

    //Exchange
    public TMP_Text text; // mặc định
    public TMP_InputField inputField; // đầu vào input để đổi
    private int number, number1, count;
    private int coin = 0;
    public bool check = false;
    private GameManager gameManager;
    private CoinShop coinShop;
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void OnpenExChange()
    {
        exchangeUI.SetActive(true);
    }

    public void CloseExchange()
    {
        exchangeUI.SetActive(false);
    }

    public void OpnenMessage()
    {
        messageUI.SetActive(true);
    }

    public void CloseMessage()
    {
        SceneManager.LoadScene("ShopManager");
    }

    public void CloseCheckYesNo()
    {
        checkYesNo.SetActive(false);
    }
    public void OpenCheckYesNo()
    {
        checkYesNo.SetActive(true);
        if (check)
        {
            Exchange();
            count = number - number1;

        }
        string check1 = count + "";
        inputField.text = check1;

    }


    public void comBackBtn()
    {
        audioManager.PlaySFX(audioManager.click);
        SceneManager.LoadScene("MainMenu");
    }

    public void openDetailDep()
    {
        audioManager.PlaySFX(audioManager.click);
        uiDetail.SetActive(true);
    }

    public void Exchange()
    {
        string inputValue = inputField.text; // Lấy giá trị văn bản từ TMP_InputField

        if (int.TryParse(inputValue, out number1)) // Chuyển đổi văn bản sang số nguyên
        {
            if (number1 >= 0)
            {
                coin = number1 * 100;
                GameManager.totalDiamonds -= number1;
                GameManager.totalCoins += coin;
                //gameManager.AddCoins(coin);
                coinShop.UpdateCoinTotal();
                check = true;
            }
        }
    }
}
