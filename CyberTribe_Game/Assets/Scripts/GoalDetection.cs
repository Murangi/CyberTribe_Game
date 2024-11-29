using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class GoalDetection : MonoBehaviour
{
    private const float GOAL_LINE = 50f;
    private float NORTH_GOAL_DEPTH;
    private float SOUTH_GOAL_DEPTH;
 
    Vector3 position;
    float x_pos;
    float z_pos;
 
    // Start is called before the first frame update
    void Start()
    {
        
        NORTH_GOAL_DEPTH = GameObject.Find("North Wall 1").transform.position.z;
        SOUTH_GOAL_DEPTH = GameObject.Find("South Wall 1").transform.position.z;
       
    }
 
    // Update is called once per frame
    void Update()
    {
        GoalDetected();
    }
 
 
    private bool isPassedNorthGoal()
    {
        position = gameObject.transform.position;
 
        x_pos = position.x;
        z_pos = position.z;
 
        // return ((x_pos >= -GOAL_LINE && x_pos <= GOAL_LINE) && (z_pos <= NORTH_GOAL_DEPTH));
        return ((x_pos >= -GOAL_LINE && x_pos <= GOAL_LINE) && (z_pos <= NORTH_GOAL_DEPTH));
    }
 
    private bool isPassedSouthGoal()
    {
        position = gameObject.transform.position;
 
        x_pos = position.x;
        z_pos = position.z;
 
        return ((x_pos >= -GOAL_LINE && x_pos <= GOAL_LINE) && (z_pos >= SOUTH_GOAL_DEPTH));
    }
 
    public void GoalDetected()
    {
        if (isPassedNorthGoal())
        {
            Debug.Log($"{gameObject.name} passed north goal line.");
            Destroy(gameObject);
            // return true;
        }
        else if (isPassedSouthGoal())
        {
            Debug.Log($"{gameObject.name} passed south goal line.");
            Destroy(gameObject);
            // return true;
        }
        // return false;
    }
}