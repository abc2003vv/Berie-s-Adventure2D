using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingTrap : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform character;
    public float activationDistance = 5f;
    private bool isFalling = false; // Đánh dấu khi bẫy bắt đầu rơi
    AudioManager audioManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    void Update()
    {
        // Kiểm tra nếu nhân vật vẫn tồn tại trước khi lấy vị trí của nó
        if (character != null && !isFalling && Vector2.Distance(transform.position, character.position) < activationDistance)
        {
            rb.gravityScale = 1.5f; // Bật trọng lực để bẫy rơi
            isFalling = true; // Đánh dấu bẫy đã bắt đầu rơi
            Invoke("DestroyTrap", 2f); // Hủy bẫy sau 2 giây
            audioManager.PlaySFX(audioManager.fallingTrap);
        }
    }

    void DestroyTrap()
    {
        Destroy(gameObject); // Hủy bẫy sau 2 giây
    }

    void OnDrawGizmosSelected()
    {
        // Vẽ khoảng cách kích hoạt bẫy trong Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, activationDistance);
    }
}
