using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageController : MonoBehaviour
{
    [SerializeField] private float garbageCollectionSpeed;
    private void OnTriggerEnter(Collider other)

    {
        if(other.tag == "Robot")
        {
            Destroy(gameObject, garbageCollectionSpeed);          
        }
    }
}
