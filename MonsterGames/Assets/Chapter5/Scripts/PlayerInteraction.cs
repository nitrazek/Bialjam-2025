using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    public string childTag = "Child";
    private int threshold = 10;

    private void Start()
    {
        GameData.HuggingScore = 0;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log($"PlayerInteraction: OnTriggerEnter with {other.gameObject.name}");
        if(!other.CompareTag(childTag)) return;
        Destroy(other.gameObject);
        GameData.HuggingScore += 1;

        if (GameData.HuggingScore > 10)
            StartCoroutine(ChangeSceneAfterDelay(1));
    }

    private IEnumerator ChangeSceneAfterDelay(float delay)
    {
        Debug.Log($"Scena zmieni siê za {delay} sekund...");
        yield return new WaitForSeconds(delay);

        GameData.NextScreenId = 3;
        SceneManager.LoadScene(GameData.TRANSITION_SCREEN_ID);
    }
}
