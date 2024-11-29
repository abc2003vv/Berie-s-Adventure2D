using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class SelectionLevelManager : MonoBehaviour
{
    public Button[] levelButtons;
    public Image Lock;
    public Image Done;
    int highestLevel;
    AudioManager audioManager;


    void Start()
    {
        highestLevel = PlayerPrefs.GetInt("highestLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelNum = i + 1;
            if (levelNum > highestLevel)
            {
                levelButtons[i].interactable = false; //chua mo khoa
                levelButtons[i].GetComponent<Image>().sprite = Lock.sprite;
                levelButtons[i].GetComponentInChildren<TMP_Text>().text = "";
            }
            else
            {
                levelButtons[i].interactable = true;
                levelButtons[i].GetComponentInChildren<TMP_Text>().text = "" + levelNum;
                levelButtons[i].GetComponent<Image>().sprite = Done.sprite;
            }
        }


        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    void Update()
    {

    }

    public void LoadLevel(int levelNum)
    {
        audioManager.PlaySFX(audioManager.click);
        SceneManager.LoadScene("Level_" + levelNum);
    }

    public void Reset()
    {
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public void onClickBackGame()
    {
        audioManager.PlaySFX(audioManager.click);
        SceneManager.LoadScene("MainMenu");
    }
}
