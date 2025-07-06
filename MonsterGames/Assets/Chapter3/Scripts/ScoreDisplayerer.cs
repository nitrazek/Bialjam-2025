using TMPro;
using UnityEngine;

public class ScoreDisplayerer : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"Points: {GameData.BowlingScore}";
    }
}
