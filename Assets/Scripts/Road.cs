using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script renders the road in the background
public class Road : MonoBehaviour {

    public float scrollSpeed;
    private Vector2 offset;

    private void Update()
    {
        // Increase the offset y value by the scroll speed
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, Time.time * scrollSpeed); ;
    }
}
