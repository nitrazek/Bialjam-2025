using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController[] childControllers;
    [SerializeField] private Sprite[] huggingSprites;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Sprite newSprite;
    [SerializeField] private MonoBehaviour playerMovementScript;

    private void Start() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameData.HuggingScore = 0;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log($"PlayerInteraction: OnTriggerEnter with {other.gameObject.name}");
        if(other.CompareTag("Child") || other.CompareTag("SexyAdult"))
        {
            if (other.CompareTag("Child"))
            {
                RuntimeAnimatorController controller = other.GetComponent<Animator>().runtimeAnimatorController;
                for (int i = 0; i < childControllers.Length; i++)
                {
                    if (controller == childControllers[i])
                    {
                        Debug.Log($"Child controller matched: {controller.name}");
                        newSprite = huggingSprites[i];
                        break;
                    }
                }
            }
            else if(other.CompareTag("SexyAdult"))
            {
                newSprite = huggingSprites[3];
            }

            Destroy(other.gameObject);
            StartCoroutine(StopAnimationAndChangeSprite());
            GameData.HuggingScore++;
            if (other.CompareTag("SexyAdult") && GameData.showSecret == false)
                GameData.showSecret = true;
        }
        else if (other.CompareTag("Adult"))
        {
            Destroy(other.gameObject);
            GameData.HuggingScore--;
        }
    }

    private IEnumerator StopAnimationAndChangeSprite() {
        if(IsInvoking(nameof(RestoreSpriteAndAnimation))) {
            yield break; // Exit if already in process
        }

        animator.enabled = false;
        Debug.Log("Animator stopped.");

        playerMovementScript.enabled = false;

        // 2. Change the sprite
        if(spriteRenderer != null && newSprite != null) {
            spriteRenderer.sprite = newSprite;
            Debug.Log("Sprite changed to new sprite.");
        }

        // 3. Wait for 1 second
        yield return new WaitForSeconds(1f); // Wait for the specified duration

        // 4. Restore original sprite and resume animation
        RestoreSpriteAndAnimation();
    }

    private void RestoreSpriteAndAnimation() {
        animator.enabled = true;
        playerMovementScript.enabled = true;
        Debug.Log("Animator resumed.");
    }
}
