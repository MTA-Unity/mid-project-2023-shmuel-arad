using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A manager class that is in charge of randomly spawning bonuses on screen
public class BonusSpawner : MonoBehaviour 
{
    public GameObject[] bonuses;
    public int minDelay;
    public int maxDelay;

    private float _delay;

    private void Start()
    {
        // Randomly select a delay by which to spawn the next bonus
        _delay = Random.Range(minDelay, maxDelay);
    }

    private void Update()
    {
        _delay -= Time.deltaTime;

        // If the delay expired
        if (_delay <= 0)
        {
            // Randomly select a delay by which to spawn the next bonus
            _delay = Random.Range(minDelay, maxDelay);

            // Create a random bonus in a random horizontal location at the top of the road
            Instantiate(bonuses[(int)Random.Range(0, bonuses.Length)], new Vector3(Random.Range(-2.4f, 2.4f), 6f, 0), Quaternion.identity);
        }
    }
}
