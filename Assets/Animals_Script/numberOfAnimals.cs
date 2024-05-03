using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject animalPrefab; // Reference to the animal prefab to spawn
    public int numberOfAnimals = 5; // Number of animals to spawn

    void Start()
    {
        // Spawn the specified number of animals
        for (int i = 0; i < numberOfAnimals; i++)
        {
            SpawnAnimal();
        }
    }

    void SpawnAnimal()
    {
        // Randomly spawn the animal within a specified range
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
        Instantiate(animalPrefab, spawnPosition, Quaternion.identity);
    }
}
