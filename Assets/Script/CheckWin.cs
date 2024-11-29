using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class CheckWin : MonoBehaviour
{
    public int index;
    public string Name;
    public int Achieved;

    AudioManager audioManager;


    void Start()
    {
        Achieved = PlayerPrefs.GetInt(Name);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Character"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.gameWin();
                audioManager.PlaySFX(audioManager.win);
            }

            //qua man choi
            if (Achieved == 0)
            {
                index++;
                Achieved++;
                PlayerPrefs.SetInt("highestLevel", index);
                PlayerPrefs.SetInt(Name, Achieved);
                PlayerPrefs.Save();
            }
        }

    }
}
