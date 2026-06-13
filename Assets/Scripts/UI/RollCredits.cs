using UnityEngine;

public class RollCredits : MonoBehaviour
{
    public float scrollSpeed = 40f;
    private RectTransform rectTransform;

    private void Start()
    {
        Time.timeScale = 1f;
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    { rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime); }
}
