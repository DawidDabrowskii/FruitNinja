using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] fruitPrefabs;
    public GameObject bombPrefab; // other bomb prefab because it will have different random chance than fruits

    [Range(0f, 1f)] // slider
    [SerializeField] private float bombChance = 0.05f;

    [SerializeField] private float minSpawnDelay = 0.25f;
    [SerializeField] private float maxSpawnDelay = 1f;

    [SerializeField] private float minAngle = -15f;
    [SerializeField] private float maxAngle = 15f;

    [SerializeField] private float minForce = 18f;
    [SerializeField] private float maxForce = 22f;

    [SerializeField] private float maxLifetime = 5f;

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn() // pause execution of function and wait for something to happen before we continue
    {
        yield return new WaitForSeconds(2f);

        while (enabled)
        {
            GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)]; // random chosen prefab

            if (Random.value < bombChance) // bomb spawner
            {
                prefab = bombPrefab;
            }

            Vector3 position = new Vector3(); // random spawn area in collider's bounds
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            Quaternion rotation = Quaternion.Euler(0f,0f,Random.Range(minAngle,maxAngle)); // random rotation in Z axes

            GameObject fruit = Instantiate(prefab, position, rotation); // prefab spawn
            Destroy(fruit, maxLifetime); // prefab destroy after spec.time

            float force = Random.Range(minForce, maxForce); // random force between min and max
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse); // adding force up*, direction based on Z axes by impulse in this case

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay)); // random time between min and max
        }
    }
}
