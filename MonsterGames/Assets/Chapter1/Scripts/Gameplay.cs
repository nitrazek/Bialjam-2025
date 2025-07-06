using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

class Customer
{
    public GameObject Child { get; set; }
    public string ShoeSize { get; set; }
    public string ShoeColor { get; set; }
    public string ShoeStyle { get; set; }
}

public class Gameplay : MonoBehaviour
{
    [Header("Lista mo¿liwych dzieci")]
    public GameObject[] children;
    [Header("Miejsca wyzwalania")]
    public GameObject childPlaceHolder;
    public GameObject shelvePlaceHolder;
    public GameObject shelveDialog;
    [Header("Player i próg odleg³oœci")]
    public GameObject player;
    public float distanceThreshold = 1.65f;

    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float lifetime = 5f;

    private string[] possibleShoeSizes = { "Small", "Medium", "Large" };
    private string[] possibleShoeColors = { "Red", "Blue", "Green", "Purple" };
    private string[] possibleShoeStyles = { "Cool", "Hot", "Lame" };

    private List<Customer> activeCustomers = new();
    private float _left, _right;
    private float _timer;
    public string? currentShoeSize = "Small";
    public string? currentShoeColor = "Red";
    public string? currentShoeStyle = "Cool";

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

        for (int i = 0; i < activeCustomers.Count; i++)
        {
            Customer customer = activeCustomers[i];
            if (customer.Child == null || !customer.Child.activeSelf)
            {
                activeCustomers.RemoveAt(i);
                continue;
            }

            float distChild = Vector3.Distance(player.transform.position, customer.Child.transform.position);
            if(distChild <= distanceThreshold) {
                if(currentShoeSize == customer.ShoeSize &&
                   currentShoeColor == customer.ShoeColor &&
                   currentShoeStyle == customer.ShoeStyle)
                {
                    GameData.ShoesScore++;
                    Destroy(customer.Child);
                }

                SetDialogActive(customer.Child, "Dialog closed", false);
                SetDialogActive(customer.Child, "Dialog open", true);
            } else {
                

                SetDialogActive(customer.Child, "Dialog open", false);
                SetDialogActive(customer.Child, "Dialog closed", true);
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
        int childIndex = Random.Range(0, children.Length);
        Vector3 spawnPosition = new Vector3(randomX, children[0].transform.position.y, 0.5f);

        GameObject spawnedObject = Instantiate(children[childIndex], spawnPosition, Quaternion.identity);
        spawnedObject.SetActive(true);
        SetDialogActive(spawnedObject, "Dialog closed", true);

        Customer newCustomer = new Customer
        {
            Child = spawnedObject,
            //ShoeSize = possibleShoeSizes[Random.Range(0, possibleShoeSizes.Length)],
            //ShoeColor = possibleShoeColors[Random.Range(0, possibleShoeColors.Length)],
            //ShoeStyle = possibleShoeStyles[Random.Range(0, possibleShoeStyles.Length)]
            ShoeSize = "Small",
            ShoeColor = "Red",
            ShoeStyle = "Cool"
        };
        activeCustomers.Add(newCustomer);

        Destroy(spawnedObject, lifetime);
    }

    private void SetDialogActive(GameObject child, string dialogName, bool active)
    {
        Transform t = child.transform.Find(dialogName);
        if (t != null && t.gameObject.activeSelf != active)
            t.gameObject.SetActive(active);
    }
}
