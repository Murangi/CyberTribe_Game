using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarblePlayer1 : MonoBehaviour
{
    private GameObject MousePointA;
    private GameObject MousePointB;
    private GameObject arrow;
    private GameObject circle;

    // Current Distance Variables
    private float CurrentDistance;
    public float MaxDistance = 3f;
    private float SafeSpace;
    private float ShootPower;

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
    }

    private void OnMouseDown()
    {
        // Show the arrow when the mouse is clicked
        arrow.GetComponent<Renderer>().enabled = true;

        arrow.transform.rotation = Quaternion.Euler(0,180, 0);
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
        ShootPower = Mathf.Abs(SafeSpace) * 300;
        Vector3 DimensionsXY = MousePointA.transform.position - transform.position;
        float Difference = DimensionsXY.magnitude;

        // Update MousePointB to follow the mouse within bounds
        MousePointB.transform.position = transform.position + ((DimensionsXY / Difference) * SafeSpace * -1);

        // Restrict MousePointB to the marble’s y-position to keep it level
        MousePointB.transform.position = new Vector3(MousePointB.transform.position.x, transform.position.y, MousePointB.transform.position.z);

        // Calculate ShootDirection only in the x-z plane
        ShootDirection = Vector3.Normalize(new Vector3(DimensionsXY.x, 0f, DimensionsXY.z));

        // Call ArrowFunctionality to update positions and directions
        ArrowFunctionality();
    }

    private void OnMouseUp()
    {
        // Hide the arrow when the mouse is released
        arrow.GetComponent<Renderer>().enabled = false;

        // Apply force in the x-z plane only, restricting y
        Vector3 Push = new Vector3(ShootDirection.x, 0f, ShootDirection.z) * ShootPower * -1;
        GetComponent<Rigidbody>().AddForce(Push, ForceMode.Impulse);
    }

    private void ArrowFunctionality()
    {
        float arrowOffsetDistance = 0.5f; // Adjust this offset value as needed

        arrow.transform.rotation = Quaternion.Euler(0,180, 0);

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
}




