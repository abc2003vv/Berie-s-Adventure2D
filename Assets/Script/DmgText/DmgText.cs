using System.Collections;
using UnityEngine;
using TMPro;

public class DmgText : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    [SerializeField] TMP_Text tMP_Text;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    public IEnumerator FadeOut()
    {
        float startAlpha = tMP_Text.color.a;
        float rate = 1.0f / lifeTime;
        float progress = 0.0f;
        while (progress < 1.0f)
        {
            Color tmp = tMP_Text.color;
            tmp.a = Mathf.Lerp(startAlpha, 0, progress);
            tMP_Text.color = tmp;
            progress += rate * Time.deltaTime;
            yield return null;
        }
        // Ensure the text is fully transparent and destroy the game object
        Color finalColor = tMP_Text.color;
        finalColor.a = 0;
        tMP_Text.color = finalColor;
        Destroy(gameObject);
    }
}
