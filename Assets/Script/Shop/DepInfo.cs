using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DepInfo : MonoBehaviour
{
    [System.Serializable]
    public class DepLaoInfo
    {
        public Sprite depLaoImage;
        public string depLaoName;
        //public string depLaoDetails;
        public string depLaoPrice;
    }

    public Image depLaoImageUI;
    public TMP_Text depLaoNameUI;
    //public TMP_Text depLaoDetailsUI;
    public TMP_Text depLaoPriceUI;
    public DepLaoInfo[] depLao;

    public void UpdateShoeInfo(int shoeIndex)
    {
        DepLaoInfo selectedDepLao = depLao[shoeIndex];
        depLaoImageUI.sprite = selectedDepLao.depLaoImage;
        depLaoNameUI.text = selectedDepLao.depLaoName;
        //depLaoDetailsUI.text = selectedDepLao.depLaoDetails;
        depLaoPriceUI.text = selectedDepLao.depLaoPrice;
    }

}
