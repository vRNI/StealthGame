using NodeCanvas.Framework;
using UnityEngine;

public class SightCone : MonoBehaviour
{
    [SerializeField]
    private float coneFOV = 70.0f;
    
    private bool enemyInSight = false;
    private float halfFOV;
    private float coneRange;

    private GameObject player;

    public bool GetEnemyInSight()
    {
        return enemyInSight;
    }

    void Start ()
    {
        halfFOV = coneFOV / 2.0f;
        coneRange = gameObject.GetComponent<SphereCollider>().radius;
        player = gameObject.GetComponent<DroneBehaviourScript>().GetPlayer();
    }
	
	void Update () {

    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("collision with: " + other.gameObject.name);
        if (other.gameObject.name == player.name)
        {
            
            if (AngleTest(other.gameObject))
            {
               //
               // player is in Collision Sphere and FOV
                enemyInSight = true;
            }
            else
            {
                enemyInSight = false;
            }
        }
        else
        {
            enemyInSight = false;
        }

            
    }


    private bool AngleTest(GameObject character)
    {
        Vector3 targetDir = character.transform.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);
        if (Mathf.Abs(angle) < halfFOV)
        {

            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
       
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        
        Gizmos.DrawRay(transform.position, leftRayDirection * coneRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * coneRange);
    }
}
