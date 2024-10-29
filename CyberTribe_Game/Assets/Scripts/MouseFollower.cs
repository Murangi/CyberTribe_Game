using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    private float yOffset = 10f;  // Fixed y offset
    private Vector3 TemporaryPosition;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Convert mouse position to world space with a fixed z distance from the camera
        TemporaryPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y - yOffset));

        // Set the object's position to match the mouse's position on x and z axes, with a fixed y offset
        transform.position = new Vector3(TemporaryPosition.x, yOffset, TemporaryPosition.z);
    }
}
