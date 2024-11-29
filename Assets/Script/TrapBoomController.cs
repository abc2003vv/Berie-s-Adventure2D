using UnityEngine;
using System.Collections;

public class TrapBoomController : MonoBehaviour
{
    public int explosionDamage = 10; // Sát thương gây ra bởi vụ nổ đến các nhân vật
    public float explosionRadius = 2f; // Bán kính của hiệu ứng vụ nổ
    public LayerMask targetLayer; // LayerMask để lọc các mục tiêu bị ảnh hưởng bởi vụ nổ
    private Animator anim; // Thành phần Animator để xử lý các hoạt ảnh

    private bool hasExploded = false; // Cờ để kiểm tra xem vụ nổ đã xảy ra chưa
    private bool isTriggered = false; // Cờ để kiểm tra xem bẫy đã bị kích hoạt chưa

    AudioManager audioManager; // Tham chiếu đến AudioManager để phát âm thanh

    void Start()
    {
        // Lấy thành phần Animator gắn liền với GameObject
        anim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu bẫy chưa được kích hoạt và đối tượng va chạm là nhân vật
        if (!isTriggered && collision.gameObject.CompareTag("Character"))
        {
            // Đánh dấu bẫy đã bị kích hoạt
            isTriggered = true; // Bỏ dòng này nếu bạn không cần đánh dấu
            StartCoroutine(BoomDelay()); // Bắt đầu coroutine để trì hoãn vụ nổ
        }
    }

    public void TriggerExplosion()
    {
        // Kiểm tra nếu vụ nổ chưa được kích hoạt
        if (!hasExploded)
        {
            hasExploded = true; // Đặt cờ để tránh kích hoạt lại vụ nổ
            StartCoroutine(BoomDelay()); // Bắt đầu coroutine để trì hoãn vụ nổ
        }
    }

    IEnumerator BoomDelay()
    {
        // Kích hoạt hoạt ảnh vụ nổ nếu có Animator
        if (anim != null)
        {
            anim.SetTrigger("Explode"); // Kích hoạt hoạt ảnh vụ nổ
        }

        // Phát âm thanh vụ nổ
        audioManager.PlaySFX(audioManager.boom);

        yield return new WaitForSeconds(1f); // Chờ 1 giây trước khi thực hiện vụ nổ

        ExecuteBoom(); // Thực hiện vụ nổ
    }

    // Phương thức được gọi bởi sự kiện hoạt ảnh
    public void ExecuteBoom()
    {
        // Phát hiện tất cả các collider trong bán kính vụ nổ phù hợp với lớp mục tiêu
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, targetLayer);

        // Lặp qua các collider được phát hiện
        foreach (Collider2D col in colliders)
        {
            // Hủy các đối tượng gán tag là "Box" hoặc "Enemy"
            if (col.CompareTag("Box") || col.CompareTag("Enemy"))
            {
                Destroy(col.gameObject); // Hủy box hoặc enemy
            }
            // Gây sát thương cho các đối tượng gán tag là "Character"
            if (col.CompareTag("Character"))
            {
                CharacterController character = col.GetComponent<CharacterController>();
                if (character != null)
                {
                    character.TriggerHitEffect();
                    character.TakeDamage(explosionDamage); // Giảm sức khỏe của nhân vật theo explosionDamage
                }
            }
        }

        isTriggered = false; // Đặt lại cờ isTriggered sau khi vụ nổ xảy ra

        // Hủy GameObject của bẫy sau vụ nổ
        Destroy(gameObject, 0.5f); // Điều chỉnh độ trễ để cho phép hoạt ảnh phát nếu cần
    }

    // Vẽ bán kính vụ nổ trong Unity Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Đặt màu cho Gizmos
        Gizmos.DrawWireSphere(transform.position, explosionRadius); // Vẽ hình cầu wireframe
    }
}
