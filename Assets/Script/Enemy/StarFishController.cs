using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFishController : MonoBehaviour
{
    public Transform attackRange; // Vùng tấn công
    public float attackRangeRadius = 2f; // Bán kính của vùng tấn công
    public LayerMask playerLayer; // Layer của nhân vật
    public bool isFacingRight = true; // Biến xác định hướng quay
    public int damage = 5; // Số máu bị trừ khi tấn công
    private int hitCount = 0; // Số lần bị ném trúng
    private bool isDead = false; // Kiểm tra trạng thái chết
    private bool isAttacking = false; // Kiểm tra trạng thái tấn công
    public Animator animator;
    public EnemyVFX enemyVFX;

    public int maxHealth = 2;
    private int currentHealth;

    public float moveSpeed = 3f; // Tốc độ di chuyển
    private Vector2 targetPosition; // Vị trí mục tiêu di chuyển
    private bool movingTowardsTarget = true; // Kiểm tra hướng di chuyển

    void Start()
    {
        currentHealth = maxHealth;
        // Đặt vị trí mục tiêu ban đầu là vị trí hiện tại
        targetPosition = transform.position;
    }

    void Update()
    {
        if (isDead) return;

        // Kiểm tra xem nhân vật có nằm trong vùng tấn công không
        Collider2D player = Physics2D.OverlapCircle(attackRange.position, attackRangeRadius, playerLayer);
        if (player != null)
        {
            // Nếu có nhân vật trong vùng tấn công, chuyển sang trạng thái Attack và thực hiện tấn công
            if (!isAttacking)
            {
                isAttacking = true;
                animator.SetBool("IsAttacking", true);
                Attack(player.gameObject);
            }
            Move();
            FacePlayer(player.transform);
        }
        else
        {
            // Nếu không có nhân vật trong vùng tấn công, chuyển về trạng thái Idle
            if (isAttacking)
            {
                isAttacking = false;
                animator.SetBool("IsAttacking", false);
            }
        }
    }

    // Hàm để di chuyển con ngôi sao biển
    private void Move()
    {
        // Di chuyển đến vị trí mục tiêu
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Kiểm tra xem đã đến vị trí mục tiêu chưa
        if ((Vector2)transform.position == targetPosition)
        {
            // Chuyển hướng mục tiêu
            movingTowardsTarget = !movingTowardsTarget;
            targetPosition = movingTowardsTarget ? new Vector2(transform.position.x + 3f, transform.position.y) : new Vector2(transform.position.x - 3f, transform.position.y);
        }
    }

    // Hàm để tăng số lần bị ném trúng
    public void IncrementHitCount()
    {
        hitCount++;
        if (hitCount >= 2)
        {
            Die();
        }
    }

    // Hàm để xử lý khi chết
    private void Die()
    {
        if (enemyVFX != null)
        {
            enemyVFX.PlayVFX(transform.position);
        }
        isDead = true;
        animator.SetTrigger("isDeath");
        gameObject.SetActive(false);
    }

    // Hàm để quay con sò về phía nhân vật
    void FacePlayer(Transform player)
    {
        // Tính toán hướng quay của con sò
        Vector3 direction = player.position - transform.position;
        direction.z = 0; // Giữ lại chuyển động chỉ trên mặt phẳng 2D

        // Xác định xem nhân vật nằm bên trái hay bên phải
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

    // Hàm để quay trái/phải
    void Flip()
    {
        // Đảo ngược hướng
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

    // Phương thức sự kiện được gọi khi sự kiện animation kích hoạt
    public void OnAttackHit()
    {
        // Kiểm tra xem có nhân vật trong vùng tấn công không
        Collider2D player = Physics2D.OverlapCircle(attackRange.position, attackRangeRadius, playerLayer);
        if (player != null)
        {
            // Nếu có nhân vật trong vùng tấn công, giảm máu của nhân vật
            Attack(player.gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackRange == null)
            return;

        Gizmos.color = Color.red;

        // Vẽ hình tròn đại diện cho vùng tấn công
        Gizmos.DrawWireSphere(attackRange.position, attackRangeRadius);
    }
}
