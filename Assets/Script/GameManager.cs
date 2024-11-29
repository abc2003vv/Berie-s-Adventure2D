using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gamePauseUI;
    public GameObject gameWinUI;
    public GameObject gameOverUI;
    public GameObject gameSettingUI;
    public TMP_Text textTotal;
    public TMP_Text textTotalPigCoin;

    public static int totalCoins;
    public static int totalPigCoin;
    public static int totalDiamonds;

    private int currentSessionCoins;
    private int currentSessionPigCoins;

    AudioManager audioManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Không cần gọi LoadCoins nữa
        UpdateCoinTotal();
        UpdatePigCoinTotal();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void AddCoins(int amount)
    {
        currentSessionCoins += amount;
        UpdateCoinTotal();
    }

    public void AddPigCoins(int amount)
    {
        currentSessionPigCoins += amount;
        UpdatePigCoinTotal();
    }

    public void AddDiamonds(int amount)
    {
        totalDiamonds += amount; // Cập nhật tổng số kim cương
    }

    public void pauseGame()
    {
        audioManager.PlaySFX(audioManager.click);
        gamePauseUI.SetActive(true);
    }

    public void settingGame()
    {
        audioManager.PlaySFX(audioManager.click);
        gameSettingUI.SetActive(true);
    }

    public void reSume()
    {
        audioManager.PlaySFX(audioManager.click);
        gamePauseUI.SetActive(false);
        gameSettingUI.SetActive(false);
    }

    public void UpdateCoinTotal()
    {
        if (textTotal != null)
        {
            textTotal.text = "" + currentSessionCoins;
        }
    }

    public void UpdatePigCoinTotal()
    {
        if (textTotalPigCoin != null)
        {
            textTotalPigCoin.text = "" + currentSessionPigCoins;
        }
    }

    public void gameWin()
    {
        if (gameWinUI != null)
        {
            gameWinUI.SetActive(true);
            // Cộng tổng số tiền thu thập được vào tổng số tiền
            totalCoins += currentSessionCoins;
            totalPigCoin += currentSessionPigCoins;
            // Cập nhật UI
            UpdateCoinTotal();
            UpdatePigCoinTotal();
            // Reset currentSessionCoins và currentSessionPigCoins
            currentSessionCoins = 0;
            currentSessionPigCoins = 0;
            // Lưu tổng số tiền vào PlayerPrefs
            SaveCoins();
        }
    }

    public void nextLevel()
    {
        audioManager.PlaySFX(audioManager.click);
        SceneManager.LoadScene("MainLevel");
        // Reset currentSessionCoins và currentSessionPigCoins khi chuyển cảnh
        currentSessionCoins = 0;
        currentSessionPigCoins = 0;
    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void reStart()
    {
        audioManager.PlaySFX(audioManager.click);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Reset currentSessionCoins và currentSessionPigCoins khi chơi lại màn
        currentSessionCoins = 0;
        currentSessionPigCoins = 0;
    }

    public void mainMenu()
    {
        audioManager.PlaySFX(audioManager.click);
        SceneManager.LoadScene("MainMenu");
    }

    public void quitGame()
    {
        audioManager.PlaySFX(audioManager.click);
        Application.Quit();
    }

    public void SaveCoins()
    {
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.SetInt("TotalPigCoins", totalPigCoin);
        PlayerPrefs.Save();
    }

    private void LoadCoins()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalPigCoin = PlayerPrefs.GetInt("TotalPigCoins", 0);
        currentSessionCoins = 0;
        currentSessionPigCoins = 0;
    }
}
