using UnityEngine;

public class RandomizePosition : MonoBehaviour
{
    public float leftBound = -0.75f;
    public float rightBound = 0.75f;

    private void Start()
    {
        float randomX = Random.Range(leftBound, rightBound);
        transform.position = new Vector3(randomX, transform.position.y, transform.position.z);
    }
}
