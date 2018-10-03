using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy3 : MonoBehaviour {

    private Transform goal;

    public Transform[] goals;

    public NavMeshAgent agent;

    private bool isPatrullando;

    public bool isFollowing;

    public Transform playerTransform;

    void Start()
    {

        isFollowing = true;
        StartCoroutine(FindPlayer());

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(isFollowing)
            {
                StopAllCoroutines();
                isPatrullando = true;
                isFollowing = false;
                StartPatrullar();
                
            }else
            {
                isPatrullando = false;
                isFollowing = true;
                StartCoroutine(FindPlayer());
            }
        }
    }

    public void StartPatrullar()
    {
        SetGoal();
        StartPath();
        isPatrullando = true;
    }


    void SetGoal()
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

    public void SetTarget()
    {
        agent.SetDestination(playerTransform.position);
    }

    // Update is called once per frame
    void Update()
    {

        if (isPatrullando)
        {
            CheckPosition();
        }
        else
        {
            if(!isFollowing)
            {
                isFollowing = true;
                StartCoroutine(FindPlayer());
            }
            else
            {

            }
        }

    }

    IEnumerator FindPlayer()
    {
        for (int i = 0; i >= 0; i++)
        {

            yield return new WaitForSeconds(1f);

            SetTarget();

        }

    }
}
