using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movimiento : MonoBehaviour {

    private Transform goal;

    public Transform[] goals;

    private NavMeshAgent agent;

    private bool isPatrullando;


    void Start()
    {
        isPatrullando = false;
        StartPatrullar();
    }

    public void StartPatrullar()
    {
        agent = GetComponent<NavMeshAgent>();
        SetGoal();
        StartPath();
        isPatrullando = true;
    }

    public void StopPatrullar()
    {
        isPatrullando = false;
    }

    void SetGoal ()
    {
        int tempRandom = Random.Range(0, goals.Length);

        goal = goals[tempRandom];
    }

    void StartPath()
    {

        agent.SetDestination(goal.position);
    }

    void CheckPosition()
    {
        Vector3 positionGoal = goal.position;
        Vector3 positionPlayer = transform.position;
        //Debug.Log("distance" + Vector3.Distance(positionGoal, positionPlayer));
        if (Vector3.Distance(positionGoal, positionPlayer) < 3f)
        {
           // agent.Stop();
            SetGoal();
            StartPath();
        
        }
    }

    // Update is called once per frame
    void Update () {
        if(isPatrullando)
            CheckPosition();
	}
}
