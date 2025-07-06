using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeLimit = 30f;

    void Start() {
        StartCoroutine(ChangeSceneAfterDelay(timeLimit));
    }

    // Coroutine do zmiany sceny po op�nieniu
    private IEnumerator ChangeSceneAfterDelay(float delay) {
        Debug.Log($"Scena zmieni si� za {delay} sekund...");
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Chapter3");
    }
}
