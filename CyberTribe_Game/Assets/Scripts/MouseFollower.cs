// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class MouseFollower : MonoBehaviour
// {

//     private float Offset = -0.8f;
//     private Vector3 TemperorayPosition;
//     // Start is called before the first frame update
//     void Start()
//     {
//         transform.position = new Vector3(0,10, 80);
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         TemperorayPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 10, Input.mousePosition.z));
//         transform.position = new Vector3(TemperorayPosition.x, Offset, TemperorayPosition.z);

//     }
// }  

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class MouseFollower : MonoBehaviour
// {
//     private float yOffset = -0.8f;  // Desired y offset
//     private Vector3 temporaryPosition;

//     // Start is called before the first frame update
//     void Start()
//     {
//         // Set the initial position of the object at a desired point
//         transform.position = new Vector3(0, yOffset, 80);
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         // Get the mouse position in screen space and convert it to world space with a fixed z distance
//         temporaryPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

//         // Update position to follow the mouse on x and z axes, with a fixed y offset
//         transform.position = new Vector3(temporaryPosition.x, yOffset, temporaryPosition.z);
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    private float yOffset = 10f;  // Desired y offset
    private float smoothSpeed = 5f; // Speed at which the object will follow the mouse
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

        // Adjust targetPosition to include a fixed y offset
        targetPosition = new Vector3(targetPosition.x, yOffset, targetPosition.z);

        // Smoothly move towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
