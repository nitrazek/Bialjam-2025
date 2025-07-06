using UnityEngine;

public class BowlingPinMovement : MonoBehaviour
{
    void OnDisable()
    {
        Destroy(gameObject);
    }
}
