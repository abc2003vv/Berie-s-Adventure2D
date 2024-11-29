using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarManager : MonoBehaviour
{
    public GameObject[] yellowStars; // Ngôi sao màu vàng
    public GameObject[] grayStars; // Ngôi sao màu xám

    void Start()
    {
        int levelIndex = 1; // Chỉ số cấp độ hiện tại
        int stars = PlayerPrefs.GetInt("Level" + levelIndex + "Stars", 0);
        UpdateStars(stars);
    }

    void UpdateStars(int stars)
    {
        for (int i = 0; i < yellowStars.Length; i++)
        {
            if (i < stars)
            {
                yellowStars[i].SetActive(true);
                grayStars[i].SetActive(false);
            }
            else
            {
                yellowStars[i].SetActive(false);
                grayStars[i].SetActive(true);
            }
        }
    }
}
