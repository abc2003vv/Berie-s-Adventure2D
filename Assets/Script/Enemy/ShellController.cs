using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public Transform attackRange; // Vùng tấn công của con sò
    public float attackRangeRadius = 2f; // Bán kính của vùng tấn công
    public LayerMask playerLayer; // Layer của nhân vật
    public bool isFacingRight = true; // Biến xác định hướng quay của con sò
    public int damage = 5; // Số máu bị trừ khi tấn công
    private int hitCount = 0; // Số lần chạm từ trên
    private bool isDead = false; // Kiểm tra trạng thái chết
    private bool isAttacking = false; // Kiểm tra trạng thái tấn công
    public Animator animator; // Animator của con sò
    public EnemyVFX enemyVFX;

    void Update()
    {
        if (isDead) return;

        // Kiểm tra xem nhân vật có nằm trong vùng tấn công không
        Collider2D player = Physics2D.OverlapCircle(attackRange.position, attackRangeRadius, playerLayer);
        if (player != null)
        {
            // Nếu có nhân vật trong vùng tấn công, chuyển sang trạng thái Attack
            if (!isAttacking)
            {
                isAttacking = true;
                animator.SetBool("isAttacking", true);
            }
            FacePlayer(player.transform);
        }
        else
        {
            // Nếu không có nhân vật trong vùng tấn công, chuyển về trạng thái Idle
            if (isAttacking)
            {
                isAttacking = false;
                animator.SetBool("isAttacking", false);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu nhân vật chạm vào con sò và nếu con sò đang tấn công
        if (collision.gameObject.CompareTag("Character") && isAttacking)
        {
            // Kiểm tra xem nhân vật có chạm từ trên không
            if (collision.transform.position.y > transform.position.y)
            {
                hitCount++;
                if (hitCount >= 3)
                {
                    // Kích hoạt VFX
                    if (enemyVFX != null)
                    {
                        enemyVFX.PlayVFX(transform.position);
                    }
                    // Nếu chạm từ trên 3 lần, thực hiện hành động chết
                    isDead = true;

                    // Thực hiện các hành động khi chết, ví dụ: ẩn con sò
                    gameObject.SetActive(false);
                }
            }
        }
    }

    // Hàm để quay con sò về phía nhân vật
    void FacePlayer(Transform player)
    {
        // Tính toán hướng quay của con sò
        Vector3 direction = player.position - transform.position;
        direction.z = 0; // Giữ lại chuyển động chỉ trên mặt phẳng 2D

        // Xác định xem nhân vật nằm bên trái hay bên phải con sò
        bool playerIsOnRight = direction.x > 0;

        // Cập nhật biến isFacingRight dựa trên vị trí của nhân vật
        if (playerIsOnRight && !isFacingRight)
        {
            Flip(); // Quay trái
        }
        else if (!playerIsOnRight && isFacingRight)
        {
            Flip(); // Quay phải
        }
    }

    // Hàm để quay trái/phải con sò
    void Flip()
    {
        // Đảo ngược hướng của con sò
        isFacingRight = !isFacingRight;

        // Lật con sò theo trục X
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    // Hàm để tấn công nhân vật
    void Attack(GameObject player)
    {
        // Giảm máu của nhân vật
        CharacterController playerHealth = player.GetComponent<CharacterController>();
        if (playerHealth != null)
        {
            playerHealth.TriggerHitEffect();
            playerHealth.TakeDamage(damage);
        }
    }

    //cắn
    public void OnBite()
    {
        // Kiểm tra xem có nhân vật trong vùng tấn công không
        Collider2D player = Physics2D.OverlapCircle(attackRange.position, attackRangeRadius, playerLayer);
        if (player != null)
        {

            Attack(player.gameObject); // Gọi hàm tấn công
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackRange == null)
            return;

        Gizmos.color = Color.red;

        // Vẽ hình tròn để hiển thị vùng tấn công
        Gizmos.DrawWireSphere(attackRange.position, attackRangeRadius);
    }
}
