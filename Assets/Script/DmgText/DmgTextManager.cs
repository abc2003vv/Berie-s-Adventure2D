using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DmgTextManager : MonoBehaviour
{
    private static DmgTextManager dmgTextManagerInstance;

    public static DmgTextManager MydmgTextManagerInstance
    {
        get
        {
            if (dmgTextManagerInstance == null)
            {
                dmgTextManagerInstance.GetComponent<DmgTextManager>();
            }
            return dmgTextManagerInstance;
        }
    }

    [SerializeField] GameObject dmgTextPrefabs;

    public void createText(Vector2 position, string text)
    {
        TMP_Text sct = Instantiate(dmgTextPrefabs, transform).GetComponent<TMP_Text>();
        sct.transform.position = position;
    }
}
