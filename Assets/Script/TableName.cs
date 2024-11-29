using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableName : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject NameLevelUI;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            NameLevelUI.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            NameLevelUI.SetActive(false);
        }
    }

}
