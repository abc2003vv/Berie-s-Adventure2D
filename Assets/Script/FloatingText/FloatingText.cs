using UnityEngine;
using TMPro; // Nếu bạn dùng TextMeshPro

public class FloatingText : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private Animator animator;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
    }

    public void SetText(string text)
    {
        textMesh.text = text;
    }

    // Gọi hàm này từ sự kiện Animation để hủy đối tượng sau khi hiệu ứng kết thúc
    public void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }
}
