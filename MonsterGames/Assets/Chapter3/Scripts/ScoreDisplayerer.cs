using TMPro;
using UnityEngine;

public class ScoreDisplayerer : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    void Start()
    {

    }

    void Update()
    {
        scoreText.text = $"Points: {GameData.BowlingScore}";
    }
}
