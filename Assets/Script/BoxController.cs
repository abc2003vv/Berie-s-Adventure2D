using UnityEngine;

public class BoxController : MonoBehaviour
{
    private Animator anim;
    private bool isDestroyed = false;
    AudioManager audioManager;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    public void DestroyBox()
    {
        if (!isDestroyed)
        {
            isDestroyed = true;
            anim.SetTrigger("Destroy");
            audioManager.PlaySFX(audioManager.box);
            Destroy(gameObject, 0.5f);
        }
    }


    // Xử lý khi chân nhân vật chạm vào đỉnh của Box
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Character"))
        {
            DestroyBox(); // Phá hủy Box
        }
    }

}
