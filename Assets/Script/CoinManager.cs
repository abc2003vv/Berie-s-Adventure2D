using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public float speed = 5f;
    public Transform targetTransform;
    public int value = 1;  // Giá trị của vàng này
    // VFX coin
    public CoinVFX coinVFX;

    AudioManager audioManager;

    void Start()
    {
        // Thử tìm đối tượng có tag "CoinTarget" nếu chưa gán targetTransform
        GameObject targetObject = GameObject.FindGameObjectWithTag("CoinTarget");
        if (targetObject != null)
        {
            targetTransform = targetObject.transform;
        }

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            CharacterController characterController = collision.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.collecCoin(value);
                characterController.UpdateCoinUI();
                GameManager.instance.AddCoins(value); // Thêm dòng này
            }

            audioManager.PlaySFX(audioManager.collectCoin);

            // Kích hoạt VFX
            if (coinVFX != null)
            {
                coinVFX.PlayVFX(transform.position);
            }
            StartCoroutine(MoveToTarget());
        }
    }

    private IEnumerator MoveToTarget()
    {
        while (Vector3.Distance(transform.position, targetTransform.position) > 3f)
        {
            transform.position = Vector3.Lerp(transform.position, targetTransform.position, speed * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }
}
