// Author: Arnold Holler
// Date: 2018.05.29

using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField] protected float drawRadius = 1.0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, drawRadius);
    }
}
