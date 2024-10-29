using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    private float yOffset = 10f;  // Desired y offset
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial position of the object at a desired point
        transform.position = new Vector3(0, yOffset, 80);
    }

    // Update is called once per frame
    void Update()
    {
        // Convert the mouse position from screen space to world space with a fixed z distance
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

        // Directly set the position to follow the mouse exactly, with a fixed y offset
        transform.position = new Vector3(targetPosition.x, yOffset, targetPosition.z);
    }
}

