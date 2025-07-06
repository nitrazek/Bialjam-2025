using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeLimit = 60f;

    void Start() {
        GameData.resetScore();
        StartCoroutine(ChangeSceneAfterDelay(timeLimit));
    }

    private IEnumerator ChangeSceneAfterDelay(float delay) {
        Debug.Log($"Scena zmieni siê za {delay} sekund...");
        yield return new WaitForSeconds(delay);

        GameData.NextScreenId = 1;
        SceneManager.LoadScene(GameData.TRANSITION_SCREEN_ID);
    }
}
