using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    [SerializeField] private Queue<Vector3> pathPoints = new Queue<Vector3>();

    [SerializeField] private GameObject robotParticle;    

    private void Awake() 
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        FindObjectOfType<PathCreator>().OnNewPathCreated += SetPoints; // It will add and sort "SetPoints" function from the pathcreator script related functions data from the list.
    }

    private void SetPoints (IEnumerable <Vector3> points)
    {
        pathPoints = new Queue<Vector3>(points);
    }

    private void Update()
    {
        UptadePathing();
    }
        

    private void UptadePathing() // Updates the list and sort the new process.
    {
        if (ShouldSetDestination())
            navMeshAgent.SetDestination(pathPoints.Dequeue());
    }

    private bool ShouldSetDestination() //for manage to next point
    {
        if (pathPoints.Count == 0) //if there is no target point we don't want to direct it anywhere
            return false;

        if (navMeshAgent.hasPath == false || navMeshAgent.remainingDistance < 0.5f) //if it doesn't have a path or the remaining distance is smaller then 0.5f returns true.
            return true;

        return false;
    }

    private void OnTriggerEnter(Collider other) // if robot touch that obstacle tagged object, particle system will be work and object will destroyed.
    {                                           
        if (other.tag == "Obstacle")
        {
            robotParticle.gameObject.SetActive(true);
            Destroy(gameObject,0.3f);
        }
    }
}
