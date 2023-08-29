using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages the civil cars by spawning them randomly in different lanes
public class CivilCarSpawner : MonoBehaviour 
{
    public float carSpawnDelay = 2f;
    public GameObject civilCar;

    private float _spawnDelay;
    private float[] _placesOnRoad;

    private void Start()
    {
        // Set the possible spawn places on the road
        _placesOnRoad = new float[4] { -2.11f, -0.76f, 0.76f, 2.11f };

        // Reset the car spawn delay
        _spawnDelay = carSpawnDelay;
    }

    private void Update()
    {
        _spawnDelay -= Time.deltaTime;

        // If the spawn delay has expired
        if (_spawnDelay <= 0)
        {
            SpawnCar();

            // Reset the car spawn delay
            _spawnDelay = carSpawnDelay;
        }
    }

    void SpawnCar()
    {
        // Choose spawn place
        int place = Random.Range(0, _placesOnRoad.Length);

        // If the spawn place is on the right lanes, the car should be slower and move upwards
        if (place < _placesOnRoad.Length / 2)
        {
            GameObject car = Instantiate(civilCar, new Vector3(_placesOnRoad[place], 6, 0), Quaternion.Euler(new Vector3(0, 0, 180)));

            car.GetComponent<CivilCarBehavior>().direction = 1;
            car.GetComponent<CivilCarBehavior>().civilCarSpeed = 12;
        }
        // The spawn place is on the left lanes, the car should move fast and downwards
        else
        {
            Instantiate(civilCar, new Vector3(_placesOnRoad[place], 6, 0), Quaternion.identity);
        }
    }
}
