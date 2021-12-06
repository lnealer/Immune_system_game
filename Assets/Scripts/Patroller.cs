using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    public List<Transform> points; //waypoints
    public float speed;
    [SerializeField] private int nextId = 0; // index of next point
    public float scale;
    private int idChangeValue = 1;

    void Start()
    {
          scale = transform.localScale.y;
    }

        void Reset()
    {
        // create root object
        GameObject root = new GameObject(name + "_Root");
        root.transform.position = transform.position; // move root to enemy pos
        transform.SetParent(root.transform);
        GameObject waypoints = new GameObject("Waypoints"); // make waypoints object
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;
        GameObject p1 = new GameObject("Point1");
        p1.transform.SetParent(waypoints.transform);
        p1.transform.position = root.transform.position;
        GameObject p2 = new GameObject("Point2");
        p2.transform.SetParent(waypoints.transform);
        p2.transform.position = root.transform.position;

        // init points list
        points = new List<Transform>();
        points.Add(p1.transform);
        points.Add(p2.transform);
    }

    void Update()
    {
        MoveToNextPoint();
    }

    
    void MoveToNextPoint()
    {
        // get the next point transform
        Transform goal = points[nextId];
        // flip the enemy transform to look to point direction
        if (goal.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1,1,1)*scale;
        }
        else
        {
            transform.localScale = new Vector3(-1,1,1)*scale;   
        }
        
        // move enemy towards goal
        transform.position = Vector2.MoveTowards(transform.position, goal.position, speed*Time.deltaTime);
        //check the distance between enemy and goal point to trigger next point
        if (Vector2.Distance(transform.position, goal.transform.position) < 1f)
        {
            // Check if you are at the last point, then make delta -1 (otherwise +1)
            if (nextId == points.Count-1)
            {
                nextId -= idChangeValue;
            }
            else 
            {
                nextId += idChangeValue;
            }
        }
    }
}
