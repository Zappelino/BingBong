using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;

    //Checking the Distance between the current waypoint and the current postition of the Object
    //Changing to new Waypoint if the Distance is smaller than .1f
    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            //Going to the next wayoint by adding +1
            currentWaypointIndex++;

            //Cycle back after the last waypoint
            if (currentWaypointIndex >= waypoints.Length) 
            {
                currentWaypointIndex = 0; 
            }

        }

    //Making the Object move towards the current waypoint
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}
