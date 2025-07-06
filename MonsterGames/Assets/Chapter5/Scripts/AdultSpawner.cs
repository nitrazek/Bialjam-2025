using UnityEngine;

public class AdultSpawner : MonoBehaviour
{
    [SerializeField] public GameObject adultObject;
    [SerializeField] public GameObject sexyAdultObject;
    [SerializeField] public float spawnInterval = 5f;
    [SerializeField] public float lifetime = 10f;
    [SerializeField] private Sprite[] adultSprites;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float _left, _right, _bottom, _top, _width, _height;
    private float _timer;
    private GameObject spawnedObject;

    void Start() {
        _width = spriteRenderer.bounds.size.x;
        _height = spriteRenderer.bounds.size.y;

        Camera cam = Camera.main;
        float dist = Mathf.Abs(transform.position.z - cam.transform.position.z);

        _left = cam.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        _right = cam.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        _bottom = cam.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        _top = cam.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        _timer = spawnInterval;
    }

    void Update() {
        _timer -= Time.deltaTime;
        if(_timer <= 0) {
            SpawnAdult();
            _timer = spawnInterval;
        }
    }

    private void SpawnAdult() {
        float randomX = Random.Range(_left + (_width / 2f), _right - (_width / 2f));
        float randomY = Random.Range(_bottom + (_height / 2f), _top - (_height / 2f));
        Vector3 spawnPosition = new Vector3(randomX, randomY, 2);

        int choice = Random.Range(0, 2);
        if(choice < 1)
        {
            spawnedObject = Instantiate(adultObject, spawnPosition, Quaternion.identity);
            SpriteRenderer spriteRenderer = spawnedObject.GetComponent<SpriteRenderer>();
            int adultIndex = Random.Range(0, adultSprites.Length);
            spriteRenderer.sprite = adultSprites[adultIndex];
        }
        else
        {
            spawnedObject = Instantiate(sexyAdultObject, spawnPosition, Quaternion.identity);
        }

        spawnedObject.SetActive(true);
        Destroy(spawnedObject, lifetime);
    }
}
