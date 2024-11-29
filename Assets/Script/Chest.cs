using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool isNearChest = false;
    private bool isChestOpened = false;

    public string question;
    public string[] answers = new string[4];
    public int correctAnswerIndex;

    public Animator chestAnimator;
    public GameObject coinPrefab;
    public int numberOfCoins = 10;

    AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    void Update()
    {
        if (isNearChest && Input.GetKeyDown(KeyCode.V) && !isChestOpened)
        {
            if (PuzzleUI.Instance != null)
            {
                PuzzleUI.Instance.ShowPuzzle(this, question, answers, correctAnswerIndex);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            isNearChest = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            isNearChest = false;
        }
    }

    public void OpenChest()
    {
        isChestOpened = true;
        Destroy(gameObject);
    }

    public void OpenChestWithAnimation()
    {
        chestAnimator.SetTrigger("Open");
        isChestOpened = true;

        StartCoroutine(DropCoins());
        audioManager.PlaySFX(audioManager.coinChest);

        CharacterController player = FindObjectOfType<CharacterController>();
        if (player != null)
        {
            int chestCoins = 100;
            player.collecCoin(chestCoins);
            GameManager.instance.AddCoins(chestCoins);
            GameManager.instance.UpdateCoinTotal(); // Cập nhật UI
        }
    }

    private IEnumerator DropCoins()
    {
        float maxHeight = 0.5f;
        Vector3 startPos = transform.position;

        for (int i = 0; i < numberOfCoins; i++)
        {
            Vector3 coinStartPosition = new Vector3(
                startPos.x + Random.Range(-1f, 1f),
                startPos.y + maxHeight,
                startPos.z);

            GameObject coin = Instantiate(coinPrefab, coinStartPosition, Quaternion.identity);
            Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
                float forceMagnitude = Random.Range(3f, 7f);

                rb.AddForce(direction * forceMagnitude, ForceMode2D.Impulse);

                Destroy(coin, 1f);
            }

            yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));
        }
    }

}
