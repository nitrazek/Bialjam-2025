using System.Threading;
using UnityEngine;
using System.Collections.Generic;
using static UnityEditor.PlayerSettings;

public class ChildSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject childObject;
    [SerializeField]
    public float spawnInterval = 10f;
    [SerializeField]
    public float lifetime = 5f;
    [SerializeField]
    private Sprite[] startSprites;
    [SerializeField]
    private RuntimeAnimatorController[] animatorControllers;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private float _left, _right, _bottom, _top, _width, _height;
    private float _timer;

    void Start()
    {
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

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            SpawnChild();
            _timer = spawnInterval;
        }
    }

    private void SpawnChild() {
        float randomX = Random.Range(_left + (_width / 2f), _right - (_width / 2f));
        float randomY = Random.Range(_bottom + (_height / 2f), _top - (_height / 2f));
        Vector3 spawnPosition = new Vector3(randomX, randomY, 2);

        GameObject spawnedObject = Instantiate(childObject, spawnPosition, Quaternion.identity);
        SpriteRenderer spriteRenderer = spawnedObject.GetComponent<SpriteRenderer>();
        Animator animator = spawnedObject.GetComponent<Animator>();

        int animatorIndex = Random.Range(0, animatorControllers.Length);
        animator.runtimeAnimatorController = animatorControllers[animatorIndex];
        spriteRenderer.sprite = startSprites[animatorIndex];

        spawnedObject.SetActive(true);
        Destroy(spawnedObject, lifetime);
    }
}
