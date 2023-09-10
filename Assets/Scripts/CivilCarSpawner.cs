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

    public GameObject topBlock;
    public GameObject bottomBlock;

    public int leftLaneSpeed = 300;
    public int rightLaneSpeed = 100;

    private void Start()
    {
        // Set the possible spawn places on the road
        _placesOnRoad = new float[4] { -1.7f, -0.6f, 0.6f, 1.7f };

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
        GameObject car;

        // If the spawn place is on the right lanes, the car should be slower and move upwards
        if (place < _placesOnRoad.Length / 2)
        {
            car = Instantiate(civilCar, new Vector3(_placesOnRoad[place], 6, 0), Quaternion.Euler(new Vector3(0, 0, 180)));

            car.GetComponent<CivilCarBehavior>().civilCarSpeed = leftLaneSpeed;
        }
        // The spawn place is on the left lanes, the car should move fast and downwards
        else
        {
            car = Instantiate(civilCar, new Vector3(_placesOnRoad[place], 6, 0), Quaternion.identity);

            car.GetComponent<CivilCarBehavior>().civilCarSpeed = rightLaneSpeed;
        }

        Physics2D.IgnoreCollision(car.GetComponent<BoxCollider2D>(), topBlock.GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(car.GetComponent<BoxCollider2D>(), bottomBlock.GetComponent<BoxCollider2D>());
    }
}
