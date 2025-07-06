using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScreen : MonoBehaviour
{
    public float displaySeconds = 2f;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = $"Points: {GameData.Score}";
        StartCoroutine(LoadTargetAfterDelay());
    }

    IEnumerator LoadTargetAfterDelay()
    {
        yield return new WaitForSeconds(displaySeconds);

        AsyncOperation op = SceneManager.LoadSceneAsync(GameData.NextScreenId);
        op.allowSceneActivation = true;
        while (!op.isDone)
            yield return null;
    }
}
