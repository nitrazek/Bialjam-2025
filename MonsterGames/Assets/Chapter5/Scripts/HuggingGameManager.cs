using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HuggingGameManager : MonoBehaviour
{
    [SerializeField] private float timeLimit = 30f;

    void Start() {
        StartCoroutine(ChangeSceneAfterDelay(timeLimit));
    }

    private IEnumerator ChangeSceneAfterDelay(float delay) {
        Debug.Log($"Scena zmieni siê za {delay} sekund...");
        yield return new WaitForSeconds(delay);

        GameData.NextScreenId = 3;
        SceneManager.LoadScene(GameData.TRANSITION_SCREEN_ID);
    }
}
