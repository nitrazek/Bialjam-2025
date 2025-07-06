using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeLimit = 30f;

    void Start() {
        StartCoroutine(ChangeSceneAfterDelay(timeLimit));
    }

    // Coroutine do zmiany sceny po opóŸnieniu
    private IEnumerator ChangeSceneAfterDelay(float delay) {
        Debug.Log($"Scena zmieni siê za {delay} sekund...");
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Chapter3");
    }
}
