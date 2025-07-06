using TMPro;
using UnityEngine;

public class HuggingScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"Points: {GameData.HuggingScore}";
    }
}
