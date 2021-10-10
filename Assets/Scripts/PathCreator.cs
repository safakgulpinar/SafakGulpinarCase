using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PathCreator : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<Vector3> points = new List<Vector3>(); // A list that holds the points of the line.
    public Action<IEnumerable<Vector3>> OnNewPathCreated = delegate { }; // A delegate that holds Vector3 values is created.
    [SerializeField] private Camera mainCam;  //Can be changed from editor with [SerializeField]
    private int clickCount = 0;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            points.Clear(); //It will clean the list every time you press. (Alternative for multiple drawing.)

        if (Input.GetMouseButton(0) && clickCount == 0)
        {
            //It will throw laserbeams as long as you hold the key.
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (DistanceToLastPoint(hitInfo.point) > 1f) //it holds the difference of throwing laser beam from the previous one and if it is bigger than 1f funciton will work and it will add it to the points array.
                {
                    points.Add(hitInfo.point);
                    lineRenderer.positionCount = points.Count;
                    lineRenderer.SetPositions(points.ToArray());
                }
            }

        }

        else if (Input.GetMouseButtonUp(0) && clickCount ==0) // when you release your finger from mouse button it will hold delegate. (It will order of operations with IENumarable)
        {
            clickCount++;
            OnNewPathCreated(points);

        }
    }

    private float DistanceToLastPoint(Vector3 point) // It checks are there any input and if there is it will return the difference between two points distance.
    {                                                
        if (!points.Any())
            return Mathf.Infinity;
        return Vector3.Distance(points.Last(), point);  
    }
}
