using UnityEngine;
using UnityEngine.InputSystem;

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

    private int childNumber;
    private GameObject activeChild;

    void Start()
    {
        childNumber = Random.Range(0, childs.Length);
        activeChild = childs[childNumber];

        SetDialogActive("Dialog closed", true);
        activeChild.SetActive(true);
    }

    void Update()
    {
        float distChild = Vector3.Distance(player.transform.position, childPlaceHolder.transform.position);
        float distShelve = Vector3.Distance(player.transform.position, shelvePlaceHolder.transform.position);

        if (distChild <= distanceThreshold)
        {
            SetDialogActive("Dialog closed", false);
            SetDialogActive("Dialog open", true);
        }
        else
        {
            SetDialogActive("Dialog open", false);
            SetDialogActive("Dialog closed", true);
        }

        if (distShelve <= distanceThreshold)
        {
            shelveDialog.SetActive(true);
        }
        else
        {
            shelveDialog.SetActive(false);
        }
    }
    private void SetDialogActive(string dialogName, bool active)
    {
        if (activeChild == null) return;
        Transform t = activeChild.transform.Find(dialogName);
        if (t != null && t.gameObject.activeSelf != active)
            t.gameObject.SetActive(active);
    }
}
