using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA; // Điểm A
    public Transform pointB; // Điểm B
    public float speed = 3f; // Tốc độ di chuyển

    private Vector3 target; // Mục tiêu hiện tại

    void Start()
    {
        target = pointB.position; // Bắt đầu di chuyển về phía điểm B
    }

    void Update()
    {
        // Di chuyển platform về phía mục tiêu hiện tại
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Kiểm tra nếu platform đã tới điểm mục tiêu
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            // Đổi mục tiêu giữa điểm A và B
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
