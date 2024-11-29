using UnityEngine;
using TMPro;

public class DepInShop : MonoBehaviour
{
    public IdDep idDep; // IdDep scriptable object containing depIds and idDepPrice
    public bool isUnlocked;
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI deselectButtonText; // Văn bản của nút "Bỏ dùng"
    public GameObject deselectButton; // Nút "Bỏ dùng"

    private void Awake()
    {
        CheckDepStatus();
        UpdateAllButtons();
    }

    private void CheckDepStatus()
    {
        if (PlayerPrefs.GetInt(idDep.depIds.ToString()) == 1)
        {
            isUnlocked = true;
            UpdateButtonText();
        }
    }

    public void OnBtnPress()
    {
        if (isUnlocked)
        {
            // Nếu dép đã được mở khóa
            string selectedDep = PlayerPrefs.GetString("SelectedDep", "Deplao");

            if (selectedDep == idDep.depIds.ToString())
            {
                // Nếu dép hiện tại đã được chọn, hủy chọn nó
                PlayerPrefs.SetString("SelectedDep", "Deplao");
            }
            else
            {
                // Chọn dép hiện tại
                PlayerPrefs.SetString("SelectedDep", idDep.depIds.ToString());
            }

            UpdateAllButtons(); // Cập nhật trạng thái của tất cả các dép
        }
        else
        {
            // Buy and unlock dép
            if (CoinShop.coinShopInstance != null)
            {
                if (CoinShop.coinShopInstance.SpendCoins(idDep.idDepPrice))
                {
                    PlayerPrefs.SetInt(idDep.depIds.ToString(), 1);
                    PlayerPrefs.SetString("SelectedDep", idDep.depIds.ToString());
                    CheckDepStatus();
                    UpdateAllButtons();
                }
            }
        }
    }

    public void OnDeselectBtnPress()
    {
        // Set default dép
        PlayerPrefs.SetString("SelectedDep", "Deplao");
        //UpdateButtonText();
        UpdateAllButtons();
    }

    private void UpdateButtonText()
    {
        string selectedDep = PlayerPrefs.GetString("SelectedDep", "Deplao");

        // Update button text based on the selected dép
        //buttonText.text = (selectedDep == idDep.depIds.ToString()) ? "Sử dụng" : "Bỏ dùng";
        if (!isUnlocked)
        {
            // Nếu dép chưa được mở khóa
            buttonText.text = "Mua";
            deselectButton.SetActive(false);
        }
        else if (selectedDep == idDep.depIds.ToString())
        {
            // Nếu dép là dép hiện tại được chọn
            buttonText.text = "Bỏ dùng";
            deselectButton.SetActive(true);
        }
        else
        {
            // Nếu dép đã được mở khóa nhưng chưa được chọn
            buttonText.text = "Sử dụng";
            deselectButton.SetActive(false);
        }

        // // Update "Bỏ dùng" button visibility and text
        // if (selectedDep == idDep.depIds.ToString())
        // {

        //     deselectButton.SetActive(true); // Hiển thị nút "Bỏ dùng"
        // }
        // else
        // {
        //     deselectButton.SetActive(false); // Ẩn nút "Bỏ dùng"
        // }
    }
    private void UpdateAllButtons()
    {
        // Tìm tất cả các nút dép trong scene và cập nhật trạng thái của chúng
        DepInShop[] allDepButtons = FindObjectsOfType<DepInShop>();
        foreach (DepInShop depButton in allDepButtons)
        {
            depButton.UpdateButtonText();
        }
    }
}
