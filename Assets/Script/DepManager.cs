using UnityEngine;

public class DepManager : MonoBehaviour
{
    public SpriteRenderer depSpriteRenderer; // Tham chiếu đến SpriteRenderer cho dép
    private string depResourcePath = "Deplao/"; // Đường dẫn đến hình ảnh dép trong thư mục Resources

    void Start()
    {
        LoadSelectedDep();
    }

    public void LoadSelectedDep()
    {
        string selectedDepId = PlayerPrefs.GetString("SelectedDep", string.Empty);

        if (!string.IsNullOrEmpty(selectedDepId))
        {
            // Tải sprite dép từ Resources
            Sprite depSprite = Resources.Load<Sprite>(depResourcePath + selectedDepId);

            if (depSprite != null)
            {
                depSpriteRenderer.sprite = depSprite;
            }
        }
    }
}
