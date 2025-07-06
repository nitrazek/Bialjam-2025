using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HuggingGameManager : MonoBehaviour
{
    [SerializeField] private float timeLimit = 30f;
    public GameObject secretDialog;

    void Start() {
        StartCoroutine(ChangeSceneAfterDelay(timeLimit));
    }

    private IEnumerator ChangeSceneAfterDelay(float delay) {
        Debug.Log($"Scena zmieni siê za {delay} sekund...");
        yield return new WaitForSeconds(delay);

        if (GameData.showSecret)
        {
            GameData.showSecret = false;
            secretDialog.SetActive(true);
            yield return new WaitForSeconds(2);
            secretDialog.SetActive(false);
        }

        GameData.NextScreenId = 3;
        SceneManager.LoadScene(GameData.TRANSITION_SCREEN_ID);
    }
}
