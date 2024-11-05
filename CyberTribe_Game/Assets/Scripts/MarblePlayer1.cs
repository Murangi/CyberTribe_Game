// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class MarblePlayer1 : MonoBehaviour
// {
//     private GameObject MousePointA;
//     private GameObject MousePointB;
//     private GameObject arrow;
//     private GameObject circle;

//     // Current Distance Variables
//     private float CurrentDistance;
//     public const float MaxDistance = 90f;
//     private float SafeSpace = 0f;
//     private float ShootPower = 0f;

//     private Vector3 ShootDirection;

//     private void Awake()
//     {
//         MousePointA = GameObject.FindGameObjectWithTag("PointA");
//         MousePointB = GameObject.FindGameObjectWithTag("PointB");
//         arrow = GameObject.FindGameObjectWithTag("Arrow");
//         circle = GameObject.FindGameObjectWithTag("Circle");

//         // Disable arrow at the start
//         arrow.GetComponent<Renderer>().enabled = false;
//         circle.GetComponent<Renderer>().enabled = false;
//     }

//     private void OnMouseDown()
//     {
//         // Show the arrow when the mouse is clicked
//         // arrow.GetComponent<Renderer>().enabled = true;
//         arrow.transform.rotation = Quaternion.Euler(0, 180, 0);
//     }

//     private void OnMouseDrag()
//     {
//         // Get the mouse position in world coordinates
//         Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y - transform.position.y));

//         // Calculate the direction from the marble to the mouse position
//         Vector3 directionToMouse = (mousePosition - transform.position).normalized;

//         // Calculate the current distance from the marble to the mouse position
//         float distanceToMouse = Vector3.Distance(mousePosition, transform.position);

//         // Clamp MousePointA’s position to be within MaxDistance
//         if (distanceToMouse > MaxDistance)
//         {
//             // Set MousePointA’s position to the point at MaxDistance in the direction of the mouse position
//             MousePointA.transform.position = transform.position + directionToMouse * MaxDistance;
//             CurrentDistance = MaxDistance;
//         }
//         else
//         {
//             MousePointA.transform.position = mousePosition;
//             CurrentDistance = distanceToMouse;
//         }

//         // Set SafeSpace to the clamped distance
//         SafeSpace = CurrentDistance;

//         // Calculate shot power and direction
//         ShootPower = SafeSpace * 10;
//         Vector3 DimensionsXY = MousePointA.transform.position - transform.position;
//         float Difference = DimensionsXY.magnitude;

//         // Update MousePointB to follow the mouse within bounds
//         if (Difference != 0)  // Prevent division by zero
//         {
//             MousePointB.transform.position = transform.position + ((DimensionsXY / Difference) * SafeSpace * -1);

//             // Clamp MousePointB to MaxDistance if it exceeds it
//             float MousePointBDistance = Vector3.Distance(MousePointB.transform.position, transform.position);
//             if (MousePointBDistance > MaxDistance)
//             {
//                 MousePointB.transform.position = transform.position + ((DimensionsXY / Difference) * MaxDistance * -1);
//             }

//             // Restrict MousePointB to the marble’s y-position to keep it level
//             MousePointB.transform.position = new Vector3(MousePointB.transform.position.x, transform.position.y, MousePointB.transform.position.z);
//         }

//         // Calculate ShootDirection only in the x-z plane
//         ShootDirection = Vector3.Normalize(new Vector3(DimensionsXY.x, 0f, DimensionsXY.z));

//         Debug.Log($"CurrentDistance: {CurrentDistance}, MaxDistance: {MaxDistance}, SafeSpace: {SafeSpace}");
//         Debug.Log($"MousePointA Position: {MousePointA.transform.position}");
//         Debug.Log($"Marble Position: {transform.position}");
//         Debug.Log($"CurrentDistance: {CurrentDistance}");

//         // Call ArrowFunctionality to update positions and directions
//         ArrowFunctionality();
//     }

//     private void OnMouseUp()
//     {
//         // Hide the arrow when the mouse is released
//         arrow.GetComponent<Renderer>().enabled = false;
//         circle.GetComponent<Renderer>().enabled = false;

//         // Apply force in the x-z plane only, restricting y
//         Vector3 Push = new Vector3(ShootDirection.x, 0f, ShootDirection.z) * ShootPower * -1;
//         GetComponent<Rigidbody>().AddForce(Push, ForceMode.Impulse);
//     }

//     private void ArrowFunctionality()
//     {
//         float arrowOffsetDistance = 0.5f; // Adjust this offset value as needed
//         arrow.transform.rotation = Quaternion.Euler(0, 180, 0);

//         Vector3 arrowDirection;

//         if (CurrentDistance <= MaxDistance)
//         {
//             // Set the arrow's position based on MousePointB's mirrored position, with an added offset
//             arrow.transform.position = new Vector3(
//                 (2 * transform.position.x) - MousePointB.transform.position.x,
//                 arrow.transform.position.y,
//                 (2 * transform.position.z) - MousePointB.transform.position.z);

//             // Offset the arrow along the direction of MousePointB
//             arrowDirection = (arrow.transform.position - transform.position).normalized;
//             arrow.transform.position += arrowDirection * arrowOffsetDistance;
//         }
//         else
//         {
//             Vector3 DimensionsXY = MousePointA.transform.position - transform.position;
//             float Difference = DimensionsXY.magnitude;

//             // Place arrow at max distance along the direction from MousePointA with offset
//             arrow.transform.position = transform.position + ((DimensionsXY / Difference) * MaxDistance * -1);
//             arrowDirection = (arrow.transform.position - transform.position).normalized;
//             arrow.transform.position += arrowDirection * arrowOffsetDistance;
//         }

//         // Update arrow rotation to point towards MousePointA
//         Vector3 Direction = MousePointA.transform.position - transform.position;
//         float RotAngle = Vector3.SignedAngle(Vector3.forward, new Vector3(Direction.x, 0, Direction.z), Vector3.up);

//         arrow.transform.eulerAngles = new Vector3(0, RotAngle, 0);


           
//      }



// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarblePlayer1 : MonoBehaviour
{
    private GameObject MousePointA;
    private GameObject MousePointB;
    private GameObject arrow;
    private GameObject circle;

    private LineRenderer lineRenderer;

    // Current Distance Variables
    private float CurrentDistance;
    public const float MaxDistance = 90f;
    private float SafeSpace = 0f;
    private float ShootPower = 0f;

    private Vector3 ShootDirection;

    private void Awake()
    {
        MousePointA = GameObject.FindGameObjectWithTag("PointA");
        MousePointB = GameObject.FindGameObjectWithTag("PointB");
        arrow = GameObject.FindGameObjectWithTag("Arrow");
        circle = GameObject.FindGameObjectWithTag("Circle");

        // Disable arrow at the start
        arrow.GetComponent<Renderer>().enabled = false;
        circle.GetComponent<Renderer>().enabled = false;

        // Initialize the LineRenderer
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false; // Hide it initially
        lineRenderer.positionCount = 2; // Start with two points for a basic line
    }

    private void OnMouseDown()
    {
        // Show the arrow and line when the mouse is clicked
        arrow.GetComponent<Renderer>().enabled = true;
        lineRenderer.enabled = true;

        arrow.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    private void OnMouseDrag()
    {
        // Update current distance between MousePointA and the marble's position
        CurrentDistance = Vector3.Distance(MousePointA.transform.position, transform.position);

        if (CurrentDistance <= MaxDistance)
        {
            SafeSpace = CurrentDistance;
        }
        else
        {
            SafeSpace = MaxDistance;
        }

        // Calculate shot power and direction
        ShootPower = Mathf.Abs(SafeSpace) * 10;
        Vector3 DimensionsXY = MousePointA.transform.position - transform.position;
        float Difference = DimensionsXY.magnitude;

        // Update MousePointB to follow the mouse within bounds
        if (Difference != 0)  // Prevent division by zero
        {
            MousePointB.transform.position = transform.position + ((DimensionsXY / Difference) * SafeSpace * -1);

            // Clamp MousePointB to MaxDistance if it exceeds it
            float MousePointBDistance = Vector3.Distance(MousePointB.transform.position, transform.position);
            if (MousePointBDistance > MaxDistance)
            {
                MousePointB.transform.position = transform.position + ((DimensionsXY / Difference) * MaxDistance * -1);
            }

            // Restrict MousePointB to the marble’s y-position to keep it level
            MousePointB.transform.position = new Vector3(MousePointB.transform.position.x, transform.position.y, MousePointB.transform.position.z);
        }

        // Calculate ShootDirection only in the x-z plane
        ShootDirection = Vector3.Normalize(new Vector3(DimensionsXY.x, 0f, DimensionsXY.z));

        // Update the line to point from the marble towards MousePointB and beyond, with reflections
        UpdateLine();

        // Call ArrowFunctionality to update positions and directions
        ArrowFunctionality();
    }

    private void OnMouseUp()
    {
        // Hide the arrow and line when the mouse is released
        arrow.GetComponent<Renderer>().enabled = false;
        lineRenderer.enabled = false;

        // Apply force in the x-z plane only, restricting y
        Vector3 Push = new Vector3(ShootDirection.x, 0f, ShootDirection.z) * ShootPower * -1;
        GetComponent<Rigidbody>().AddForce(Push, ForceMode.Impulse);
    }

    private void ArrowFunctionality()
    {
        float arrowOffsetDistance = 0.5f; // Adjust this offset value as needed
        arrow.transform.rotation = Quaternion.Euler(0, 180, 0);

        Vector3 arrowDirection;

        if (CurrentDistance <= MaxDistance)
        {
            // Set the arrow's position based on MousePointB's mirrored position, with an added offset
            arrow.transform.position = new Vector3(
                (2 * transform.position.x) - MousePointB.transform.position.x,
                arrow.transform.position.y,
                (2 * transform.position.z) - MousePointB.transform.position.z);

            // Offset the arrow along the direction of MousePointB
            arrowDirection = (arrow.transform.position - transform.position).normalized;
            arrow.transform.position += arrowDirection * arrowOffsetDistance;
        }
        else
        {
            Vector3 DimensionsXY = MousePointA.transform.position - transform.position;
            float Difference = DimensionsXY.magnitude;

            // Place arrow at max distance along the direction from MousePointA with offset
            arrow.transform.position = transform.position + ((DimensionsXY / Difference) * MaxDistance * -1);
            arrowDirection = (arrow.transform.position - transform.position).normalized;
            arrow.transform.position += arrowDirection * arrowOffsetDistance;
        }

        // Update arrow rotation to point towards MousePointA
        Vector3 Direction = MousePointA.transform.position - transform.position;
        float RotAngle = Vector3.SignedAngle(Vector3.forward, new Vector3(Direction.x, 0, Direction.z), Vector3.up);

        arrow.transform.eulerAngles = new Vector3(0, RotAngle, 0);
    }

    private void UpdateLine()
    {
        // Extension distance for line beyond MousePointB
        float extensionDistance = SafeSpace * 3;

        // Calculate the extended position 3 times the distance beyond MousePointB
        Vector3 directionToExtend = (MousePointB.transform.position - transform.position).normalized;
        Vector3 extendedPosition = MousePointB.transform.position + directionToExtend * extensionDistance;

        // Line reflection settings
        int maxReflections = 3; // Maximum number of reflections
        float maxReflectionDistance = extensionDistance; // Maximum distance for each reflection

        // Set initial line positions
        Vector3 lineStart = transform.position;
        Vector3 lineEnd = extendedPosition;

        lineRenderer.positionCount = 1; // Reset position count
        lineRenderer.SetPosition(0, lineStart); // Set starting position

        int reflectionCount = 0;
        Ray ray = new Ray(lineStart, directionToExtend); // Cast a ray in the extension direction
        RaycastHit hit;

        // Loop for reflections
        while (reflectionCount < maxReflections && Physics.Raycast(ray, out hit, maxReflectionDistance))
        {
            reflectionCount++;
            lineRenderer.positionCount++; // Increase line segment count
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point); // Add hit point to line

            // Reflect the ray direction based on hit surface normal
            directionToExtend = Vector3.Reflect(ray.direction, hit.normal);
            ray = new Ray(hit.point, directionToExtend); // Cast a new ray from the hit point

            // Extend the line further along the new reflected direction
            lineEnd = hit.point + directionToExtend * maxReflectionDistance;
        }

        // Add final extended position if no reflection
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, lineEnd);
    }
}



