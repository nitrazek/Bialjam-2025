using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public string childTag = "Child";

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log($"PlayerInteraction: OnTriggerEnter with {other.gameObject.name}");
        if(!other.CompareTag(childTag)) return;
        Destroy(other.gameObject);
    }
}
