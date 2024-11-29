using UnityEngine;

public class DepLao : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D _depLao;
    private Vector2 direction;
    private int damage = 5;


    private void Start()
    {
        _depLao = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(float dir)
    {
        direction = new Vector2(dir, 0);
        Move();
    }

    private void Move()
    {
        _depLao.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage();
            }
            Destroy(gameObject); // Destroy the dép after hitting the enemy
        }

        if (other.CompareTag("Boss"))
        {
            BossController boss = other.GetComponent<BossController>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
            Destroy(gameObject);

        }

        if (other.CompareTag("StarFish"))
        {
            StarFishController starFish = other.GetComponent<StarFishController>();
            if (starFish != null)
            {
                starFish.IncrementHitCount(); // Thông báo rằng dép đã va chạm
            }
            Destroy(gameObject); // Hủy dép sau khi va chạm
        }

        if (other.CompareTag("isGround"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Box"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Boom"))
        {
            Destroy(gameObject);
        }

    }
}
