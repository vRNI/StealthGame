// Author: Arnold Holler
// Date: 2018.05.30

using UnityEngine;
using UnityEngine.AI;

public class DroneTilt : MonoBehaviour {

    [SerializeField]
    float tiltAngle = 15f;

    private NavMeshAgent m_agent;
    private Rigidbody m_body;
    private float forwardTilt = 0.0f;
    private float sidewardTilt = 0.0f;
    
    void Start () {
        m_agent = GetComponent<NavMeshAgent>();
        m_body = GetComponent<Rigidbody>();

        if (m_agent == null)
        {
            Debug.LogError("NavMeshAgent is not attached to " + gameObject.name);
        }

        if (m_body == null)
        {
            Debug.LogError("Rigidbody is not attached to " + gameObject.name);
        }
    }
	
	void Update () {
	    Vector3 velocity = m_agent.velocity;
	    var tilt = transform.InverseTransformDirection(velocity);
	    tilt = tilt.normalized;
	    tilt *= tiltAngle;
        forwardTilt = smoothRotation(forwardTilt, tilt.z) ;
        sidewardTilt = smoothRotation(sidewardTilt, tilt.x);
        gameObject.transform.Rotate(new Vector3(forwardTilt, sidewardTilt), Space.Self);
    }

    float smoothRotation(float currentTilt, float desiredTilt)
    {
        float deviation = 1.0f;
        float multiplicator = 10f;

        if (desiredTilt > currentTilt + deviation)
        {
            currentTilt += Time.deltaTime * multiplicator;
        }
        else if (desiredTilt < currentTilt - deviation)
        {
            currentTilt -= Time.deltaTime * multiplicator;
        }
        else
        {
            currentTilt = desiredTilt;
        }
        return currentTilt;
    }
}
