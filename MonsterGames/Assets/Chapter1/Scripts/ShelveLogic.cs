using UnityEngine;

public class ShelveLogic : MonoBehaviour
{
    public void OnRCoolClicked()
    {
        Gameplay gameplay = FindObjectOfType<Gameplay>();
        if (gameplay != null)
        {
            gameplay.currentShoeStyle = "Cool";
            gameplay.currentShoeColor = "Red";
        }
    }
    public void OnREpicClicked() {
        Gameplay gameplay = FindObjectOfType<Gameplay>();
        if(gameplay != null) {
            gameplay.currentShoeStyle = "Epic";
            gameplay.currentShoeColor = "Red";
        }
    }
    public void OnRLameClicked() {
        Gameplay gameplay = FindObjectOfType<Gameplay>();
        if(gameplay != null) {
            gameplay.currentShoeStyle = "Lame";
            gameplay.currentShoeColor = "Red";
        }
    }
    public void OnGCoolClicked() {
        Gameplay gameplay = FindObjectOfType<Gameplay>();
        if(gameplay != null) {
            gameplay.currentShoeStyle = "Cool";
            gameplay.currentShoeColor = "Green";
        }
    }
    public void OnGEpicClicked() {
        Gameplay gameplay = FindObjectOfType<Gameplay>();
        if(gameplay != null) {
            gameplay.currentShoeStyle = "Epic";
            gameplay.currentShoeColor = "Green";
        }
    }
    public void OnGLameClicked() {
        Gameplay gameplay = FindObjectOfType<Gameplay>();
        if(gameplay != null) {
            gameplay.currentShoeStyle = "Lame";
            gameplay.currentShoeColor = "Green";
        }
    }
    public void OnBCoolClicked() {
        Gameplay gameplay = FindObjectOfType<Gameplay>();
        if(gameplay != null) {
            gameplay.currentShoeStyle = "Cool";
            gameplay.currentShoeColor = "Blue";
        }
    }
    public void OnBEpicClicked() {
        Gameplay gameplay = FindObjectOfType<Gameplay>();
        if(gameplay != null) {
            gameplay.currentShoeStyle = "Epic";
            gameplay.currentShoeColor = "Blue";
        }
    }
    public void OnBLameClicked() {
        Gameplay gameplay = FindObjectOfType<Gameplay>();
        if(gameplay != null) {
            gameplay.currentShoeStyle = "Lame";
            gameplay.currentShoeColor = "Blue";
        }
    }
    public void OnPCoolClicked() {
        Gameplay gameplay = FindObjectOfType<Gameplay>();
        if(gameplay != null) {
            gameplay.currentShoeStyle = "Cool";
            gameplay.currentShoeColor = "Purple";
        }
    }
    public void OnPEpicClicked() {
        Gameplay gameplay = FindObjectOfType<Gameplay>();
        if(gameplay != null) {
            gameplay.currentShoeStyle = "Epic";
            gameplay.currentShoeColor = "Purple";
        }
    }
    public void OnPLameClicked() {
        Gameplay gameplay = FindObjectOfType<Gameplay>();
        if(gameplay != null) {
            gameplay.currentShoeStyle = "Lame";
            gameplay.currentShoeColor = "Purple";
        }
    }
    public void OnSClicked() {
        Gameplay gameplay = FindObjectOfType<Gameplay>();
        if(gameplay != null) {
            gameplay.currentShoeSize = "Small";
            Debug.Log("Shoe size set to Small");
        }
    }
    public void OnMClicked() {
        Gameplay gameplay = FindObjectOfType<Gameplay>();
        if(gameplay != null) {
            gameplay.currentShoeSize = "Medium";
        }
    }
    public void OnLClicked() {
        Gameplay gameplay = FindObjectOfType<Gameplay>();
        if(gameplay != null) {
            gameplay.currentShoeSize = "Large";
        }
    }
}
