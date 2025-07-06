using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Gameplay : MonoBehaviour
{
    [Header("Lista mo¿liwych dzieci")]
    public GameObject[] childs;
    [Header("Miejsca wyzwalania")]
    public GameObject childPlaceHolder;
    public GameObject shelvePlaceHolder;
    public GameObject shelveDialog;
    [Header("Player i próg odleg³oœci")]
    public GameObject player;
    public float distanceThreshold = 1.65f;

    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float lifetime = 5f;

    private List<GameObject> activeChildren = new();
    private float _left, _right;
    private float _timer;

    void Start()
    {
        Camera cam = Camera.main;
        float dist = Mathf.Abs(transform.position.z - cam.transform.position.z);

        _left = cam.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        _right = cam.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if(_timer <= 0) {
            SpawnChild();
            _timer = spawnInterval;
        }

        for (int i = 0; i < activeChildren.Count; i++)
        {
            GameObject child = activeChildren[i];
            if (child == null || !child.activeSelf)
            {
                activeChildren.RemoveAt(i);
                continue;
            }

            float distChild = Vector3.Distance(player.transform.position, child.transform.position);
            if(distChild <= distanceThreshold) {
                SetDialogActive(child, "Dialog closed", false);
                SetDialogActive(child, "Dialog open", true);
            } else {
                SetDialogActive(child, "Dialog open", false);
                SetDialogActive(child, "Dialog closed", true);
            }
        }

        float distShelve = Vector3.Distance(player.transform.position, shelvePlaceHolder.transform.position);
        if(distShelve <= distanceThreshold) {
            shelveDialog.SetActive(true);
        } else {
            shelveDialog.SetActive(false);
        }
    }

    private void SpawnChild()
    {
        float randomX = Random.Range(_left, _right);
        int childIndex = Random.Range(0, childs.Length);
        Vector3 spawnPosition = new Vector3(randomX, childs[0].transform.position.y, 0.5f);

        GameObject spawnedObject = Instantiate(childs[childIndex], spawnPosition, Quaternion.identity);
        spawnedObject.SetActive(true);
        SetDialogActive(spawnedObject, "Dialog closed", true);
        activeChildren.Add(spawnedObject);

        Destroy(spawnedObject, lifetime);
    }

    private void SetDialogActive(GameObject child, string dialogName, bool active)
    {
        Transform t = child.transform.Find(dialogName);
        if (t != null && t.gameObject.activeSelf != active)
            t.gameObject.SetActive(active);
    }
}
