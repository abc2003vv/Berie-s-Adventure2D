using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuGame : MonoBehaviour
{
    public TMP_Text textTotal;  // Reference to the UI Text element to display the coin count
    public TMP_Text textPigTotal;
    AudioMenu audioMenu;
    public GameObject SettingUI;
    public GameObject PanelHDC;
    public GameObject BXH;

    //public ParticleSystem particalLeaf;


    void Start()
    {
        UpdateCoinTotal();
        UpdatePigCoinTotal();
        audioMenu = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioMenu>();
        //particalLeaf.Play();
    }


    public void optionbtn()
    {
        audioMenu.PlayChosse(audioMenu.click);
        SettingUI.SetActive(true);
    }

    public void saveSettingbtn()
    {
        audioMenu.PlayChosse(audioMenu.click);
        SettingUI.SetActive(false);
    }

    public void playGame()
    {
        audioMenu.PlayChosse(audioMenu.click);
        SceneManager.LoadScene("MainLevel");
    }

    public void shop()
    {
        if (audioMenu != null)
        {
            audioMenu.PlayChosse(audioMenu.click);
        }
        SceneManager.LoadScene("ShopManager");
    }

    public void quitGame()
    {
        audioMenu.PlayChosse(audioMenu.click);
        Application.Quit();
    }

    public void panelHDC()
    {
        audioMenu.PlayChosse(audioMenu.click);
        PanelHDC.SetActive(true);
    }

    public void comBackHome()
    {
        audioMenu.PlayChosse(audioMenu.click);
        PanelHDC.SetActive(false);
        BXH.SetActive(false);
    }

    public void Rank()
    {
        BXH.SetActive(true);
    }


    public void UpdateCoinTotal()
    {
        if (textTotal != null)
        {
            textTotal.text = "" + GameManager.totalCoins; // Sử dụng instance của GameManager để lấy tổng số tiền
        }
    }

    public void UpdatePigCoinTotal()
    {
        if (textPigTotal != null)
        {
            textPigTotal.text = "" + GameManager.totalPigCoin; // Sử dụng instance của GameManager để lấy tổng số tiền
        }
    }

}
