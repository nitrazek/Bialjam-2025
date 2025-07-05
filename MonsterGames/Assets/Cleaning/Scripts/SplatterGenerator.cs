using UnityEngine;

public class SplatterGenerator : MonoBehaviour
{
    [SerializeField] private GameObject splatterObject;
    [SerializeField] private Sprite[] splatterSprites;
    [SerializeField] private float width = 140f;
    [SerializeField] private float height = 140f;
    [SerializeField] private int splatterAmount = 30;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < splatterAmount; i++)
        {
            GenerateSplatter();
        }
    }

    private void GenerateSplatter() {
        float randomX = Random.Range(-width / 2f, width / 2f);
        float randomZ = Random.Range(-height / 2f, height / 2f);
        
        GameObject spawnedSplatter = Instantiate(splatterObject, new Vector3(randomX, 0, randomZ), Quaternion.Euler(-90f, 0, 0));
        SpriteRenderer spriteRenderer = spawnedSplatter.GetComponent<SpriteRenderer>();

        int splatterIndex = Random.Range(0, splatterSprites.Length);
        spriteRenderer.sprite = splatterSprites[splatterIndex];
        spawnedSplatter.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
