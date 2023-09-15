using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages the civil cars by spawning them randomly in different lanes
public class CivilCarSpawner : MonoBehaviour 
{
    public int maxCarCount = 2;
    public bool pairSpawn = true;
    public float minCarSpawnDelay = 0.1f;
    public float maxCarSpawnDelay = 3f;
    public GameObject civilCar;

    private float _spawnDelay;
    private float[] _placesOnRoad;

    public GameObject topBlock;
    public GameObject bottomBlock;
    public Camera gameCamera;

    public int leftLaneSpeed = 300;
    public int rightLaneSpeed = 100;

    List<GameObject> spawnedCars;

    private void Start()
    {
        spawnedCars = new List<GameObject>();

        // Set the possible spawn places on the road
        _placesOnRoad = new float[4] { -1.7f, -0.6f, 0.6f, 1.7f };

        // Reset the car spawn delay
        _spawnDelay = Random.Range(minCarSpawnDelay, maxCarSpawnDelay);
    }

    private void Update()
    {
        _spawnDelay -= Time.deltaTime;

        // If the spawn delay has expired
        if (_spawnDelay <= 0)
        {
            // Choose spawn place
            int place = Random.Range(0, _placesOnRoad.Length);
            SpawnCar(place);

            if (pairSpawn && (Random.value <= 0.5))
            {
                int otherPlace = Random.Range(0, _placesOnRoad.Length - 1);
                if (otherPlace >= place)
                {
                    otherPlace++;
                }

                SpawnCar(otherPlace);
            }

            // Reset the car spawn delay
            _spawnDelay = Random.Range(minCarSpawnDelay, maxCarSpawnDelay);
        }
    }

    void SpawnCar(int place)
    {
        List<GameObject> newSpawnedCars = new();
        foreach (GameObject spawnedCar in spawnedCars)
        {
            if (spawnedCar != null)
            {
                newSpawnedCars.Add(spawnedCar);
            }
        }

        spawnedCars = newSpawnedCars;

        if (spawnedCars.Count < maxCarCount)
        {
            GameObject car;

            // If the spawn place is on the right lanes, the car should be slower and move upwards
            if (place < _placesOnRoad.Length / 2)
            {
                car = Instantiate(civilCar, new Vector3(_placesOnRoad[place], 20, 0), Quaternion.Euler(new Vector3(0, 0, 180)));

                car.GetComponent<CivilCarBehavior>().civilCarSpeed = leftLaneSpeed;
            }
            // The spawn place is on the left lanes, the car should move fast and downwards
            else
            {
                car = Instantiate(civilCar, new Vector3(_placesOnRoad[place], 20, 0), Quaternion.identity);

                car.GetComponent<CivilCarBehavior>().civilCarSpeed = rightLaneSpeed;
            }

            car.GetComponent<CivilCarBehavior>().gameCamera = gameCamera;

            Physics2D.IgnoreCollision(car.GetComponent<BoxCollider2D>(), topBlock.GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(car.GetComponent<BoxCollider2D>(), bottomBlock.GetComponent<BoxCollider2D>());

            spawnedCars.Add(car);
        }
    }
}
