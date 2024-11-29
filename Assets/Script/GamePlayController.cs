using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    public Transform playerTransform; // Gán nhân vật chính qua Inspector

    void Start()
    {
        string purchasedShoe = PlayerPrefs.GetString("PurchasedShoe", "Deplao");
        ApplyPurchasedShoe(purchasedShoe);
    }

    void ApplyPurchasedShoe(string shoeName)
    {
        // Lấy prefab dép từ Resources
        GameObject shoePrefab = Resources.Load<GameObject>("" + shoeName);
        if (shoePrefab != null)
        {
            // Tạo đối tượng dép mới và gán nó cho nhân vật
            GameObject newShoe = Instantiate(shoePrefab, playerTransform.position, Quaternion.identity);
            newShoe.transform.SetParent(playerTransform); // Làm con của nhân vật chính
            DepLao depLao = newShoe.GetComponent<DepLao>();

        }
    }
}
