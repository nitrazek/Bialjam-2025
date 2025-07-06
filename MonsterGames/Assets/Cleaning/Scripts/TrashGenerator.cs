using UnityEngine;

public class TrashGenerator : MonoBehaviour
{
    [SerializeField] private GameObject trashObject;
    [SerializeField] private Sprite[] trashSprites;
    [SerializeField] private float width = 140f;
    [SerializeField] private float height = 140f;
    [SerializeField] private int trashAmount = 30;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        for(int i = 0; i < trashAmount; i++) {
            GenerateSplatter();
        }
    }

    private void GenerateSplatter() {
        float randomX = Random.Range(-width / 2f, width / 2f);
        float randomZ = Random.Range(-height / 2f, height / 2f);

        GameObject spawnedTrash = Instantiate(trashObject, new Vector3(randomX, 0, randomZ), Quaternion.identity);
        spawnedTrash.SetActive(true);
    }
}
