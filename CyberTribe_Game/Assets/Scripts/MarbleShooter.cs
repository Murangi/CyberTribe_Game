using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleShooter : MonoBehaviour
{
    private GameObject MousePointA;
    private GameObject MousePointB;
    private LineRenderer lineRenderer;
    // Scale factor for line length

    // Current Distance Variables
    private float CurrentDistance;
    public const float MaxDistance = 120f;
    private float SafeSpace = 0f;
    private float ShootPower = 0f;
    private Vector3 ShootDirection;

    //Thori Testing
    public bool isMoveMade = false;

    private void Awake()
    {
        MousePointA = GameObject.FindGameObjectWithTag("PointA");
        MousePointB = GameObject.FindGameObjectWithTag("PointB");

        // Initialize the LineRenderer
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false; // Hide it initially
        lineRenderer.positionCount = 2; // Start with two points for a basic line

        if (gameObject.GetComponent<TurnManager>() == null)
            gameObject.AddComponent<TurnManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        // Show the line when the mouse is clicked
        lineRenderer.enabled = true;
    }

    private void OnMouseDrag()
    {
        // Update current distance between MousePointA and the marble's position
        CurrentDistance = Vector3.Distance(MousePointA.transform.position, transform.position);

        if (CurrentDistance <= MaxDistance)
            SafeSpace = CurrentDistance;
        else
            SafeSpace = MaxDistance;

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
                MousePointB.transform.position = transform.position + ((DimensionsXY / Difference) * MaxDistance * -1);

            // Restrict MousePointB to the marble’s y-position to keep it level
            MousePointB.transform.position = new Vector3(MousePointB.transform.position.x, transform.position.y, MousePointB.transform.position.z);
        }

        // Calculate ShootDirection only in the x-z plane
        ShootDirection = Vector3.Normalize(new Vector3(DimensionsXY.x, 0f, DimensionsXY.z));

        // Update the line to point from the marble towards MousePointB and beyond, with reflections
        UpdateLine();
    }

    private void OnMouseUp()
    {
        // Hide the line when the mouse is released
        lineRenderer.enabled = false;

        // Apply force in the x-z plane only, restricting y
        Vector3 Push = new Vector3(ShootDirection.x, 0f, ShootDirection.z) * ShootPower * -1;
        GetComponent<Rigidbody>().AddForce(Push, ForceMode.Impulse);

        isMoveMade = true;
    }

    private void UpdateLine()
    {
        // Extension distance for line beyond MousePointB
        float extensionDistance = SafeSpace * 4f;

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
