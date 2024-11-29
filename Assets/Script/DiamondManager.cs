using System;
using System.Collections;
using UnityEngine;

public class DiamondManager : MonoBehaviour
{
    public GameObject DT;
    public float speed = 5f;
    public Transform targetTransform;
    public int value = 1;
    public float delayBeforeShowingDT = 0.3f; // Delay before showing the diamond
    public float delayBeforeMoving = 0f; // Delay before starting the move

    void OnTriggerEnter2D(Collider2D collision)
    {
        //CharacterController characterController = collision.GetComponent<CharacterController>();
        if (collision.gameObject.CompareTag("Character"))
        {
            StartCoroutine(HandleDiamond());
            GameManager.instance.AddDiamonds(value); // Cập nhật số lượng kim cương
        }
    }

    private IEnumerator HandleDiamond()
    {
        yield return new WaitForSeconds(delayBeforeShowingDT);

        DT.SetActive(true);

        yield return new WaitForSeconds(0);

        StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        while (Vector3.Distance(transform.position, targetTransform.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetTransform.position, speed * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
