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

    private List<GameObject> _spawnedObjects = new List<GameObject>();
    private float _left, _right, _bottom, _top;
    private float _timer;

    void Start()
    {
        Camera cam = Camera.main;
        float dist = Mathf.Abs(transform.position.z - cam.transform.position.z);

        _left = cam.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        _right = cam.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        _bottom = cam.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        _top = cam.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
        Debug.Log($"Camera bounds: Left={_left}, Right={_right}, Bottom={_bottom}, Top={_top}");

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

        //for (int i = _spawnedObjects.Count - 1; i >= 0; i--)
        //{
        //    if (_spawnedObjects[i] == null)
        //    {
        //        _spawnedObjects.RemoveAt(i);
        //    }
        //}
    }

    private void SpawnChild() {
        float randomX = Random.Range(_left, _right);
        float randomY = Random.Range(_bottom, _top);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 3);

        GameObject spawnedObject = Instantiate(childObject, spawnPosition, Quaternion.identity);
        spawnedObject.SetActive(true);
        Destroy(spawnedObject, lifetime);
    }
}
