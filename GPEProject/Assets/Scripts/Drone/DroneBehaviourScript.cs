using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class DroneBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private GameObject m_player;

    //Distance in which you get caught
    [SerializeField]
    private float distanceToLose;

    // decides if agent is waiting on points
    [SerializeField]
    bool waitingAtWaypoints;

    //the time the agent would wait
    [SerializeField]
    private float waitTime = 3f;

    //List of all Patrolpoints to visit
    [SerializeField]
    List<Waypoint> m_patrolPoints;


    UnityAction mStopPursuitListener = null;

    private void OnEnable()
    {
        mStopPursuitListener = new UnityAction(DisablePursuit);
        EventManager.StartListening("Event_Enemies_Stop_Pursuit", mStopPursuitListener);
        //Debug.Log("Listening");
    }

    private void OnDisable()
    {
        EventManager.StopListening("Event_Enemies_Stop_Pursuit", mStopPursuitListener);
    }
    private void DisablePursuit()
    {
        pursue = false;
        lastPlayerPosition = m_patrolPoints[0].transform.position;
        //Debug.Log("CancelPursue");
    }

    private Vector3 lastPlayerPosition;
    private NavMeshAgent m_agent;
    private int currentPoint;
    bool waiting;
    bool travelling;
    private bool enemyInSight;
    float waitTimer;
    int counter = 0;

    private bool pursue = false;
    private bool searching;

    public GameObject GetPlayer()
    {
        return m_player;
    }


    public void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();

        if (m_agent == null)
        {
            Debug.LogError("NavMeshAgent is not attached to " + gameObject.name);
        }
        else
        {
            if (m_patrolPoints != null && m_patrolPoints.Count >= 2)
            {
                currentPoint = 0;
                SetDestination();
            }
            else
            {
                Debug.Log("Insufficient patrol points for patrolling.");
            }
        }
    }

    public void Update ()
    {
        enemyInSight = gameObject.GetComponent<SightCone>().GetEnemyInSight();

        if (enemyInSight)
        {
            pursue = true;
        }

        if (pursue)
        {
            PursueEnemy();
        }
        else
        {
            Patrolling();
        }
    }

    private void PursueEnemy()
    {
        float dist = Vector3.Distance(m_player.transform.position, transform.position);
        
        if (dist < distanceToLose)
        {
            // Debug.Log("Player Lost!");
            // Player LOST
            EventManager.TriggerEvent("Event_Manual_Fade_Out");

        }

        if (LineOfSight(m_player))
        {
            lastPlayerPosition = m_player.transform.position;
        }
        else
        {
            SearchEnemy();
        }
        m_agent.SetDestination(lastPlayerPosition);
    }

    private void SearchEnemy()
    {
        if (!m_agent.hasPath)
        {
            if (counter < 360)
            {
                counter++;
                Vector3 rotation = new Vector3(0f, 1f, 0f);
                gameObject.transform.Rotate(rotation);
            }
            else
            {
                counter = 0;
                pursue = false;
            }
        }

        
    }

    private bool LineOfSight(GameObject gameObject)
    {
        RaycastHit hit;
        Vector3 position = gameObject.transform.position;
        position.y += 1; //to not hit ground
        var rayDirection = position - transform.position;
        if (Physics.Raycast(transform.position, rayDirection, out hit))
        {
            if (hit.transform == m_player.transform)
            {
                return true;
            }
        }
        return false;
    }

    private void Patrolling()
    {
        //Debug.Log(" Pos[" + currentPoint + " remainingDist: " + m_agent.remainingDistance);
        if (travelling && m_agent.remainingDistance <= 1.5f)
        {
            travelling = false;

            if (waitingAtWaypoints)
            {
                waiting = true;
                waitTimer = 0f;
            }
            else
            {
                SetDestination();
            }
        }

        if (waiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                waiting = false;
                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        if (m_patrolPoints != null)
        {
            Vector3 targetVec = m_patrolPoints[currentPoint].transform.position;
            m_agent.SetDestination(targetVec);
            currentPoint = (currentPoint + 1) % m_patrolPoints.Count;
            travelling = true;
        }
    }


}
