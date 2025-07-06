using TMPro;
using UnityEngine;

public class CurrentShoeDisplay : MonoBehaviour
{
    private Gameplay gameplay;
    private TextMeshProUGUI shoeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        gameplay = FindObjectOfType<Gameplay>();
        shoeText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        shoeText.text = $"{gameplay.currentShoeSize} {gameplay.currentShoeColor} {gameplay.currentShoeStyle}";
    }
}
