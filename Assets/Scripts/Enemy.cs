using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Actor
{

    private Transform goal;

    public Vector3 runGoal;

    public Transform[] goals;

    public NavMeshAgent agent;

    private bool isPatrullando;

    private float moveRadius = 50f;

    public bool isFollowing;


    public Transform playerTransform;

    private MeshRenderer render;

    private int tempRandom;


    void Awake()
    {
        isFollowing = false;
        isPatrullando = true;
        GameManager.Instance.OnEndGame += StopPlaying;
        agent = GetComponent<NavMeshAgent>();

        StartCoroutine(FindPlayer());
        render = GetComponent<MeshRenderer>();
        render.material.color = baseColor;

        if (isFollowing)
        {
            StartFollowing();
        }
        else
        {
            isPatrullando = true;
            isFollowing = false;
            StartPatrullar();
        }
    }


    public void StopPlaying()
    {
        StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bala")
        {
            other.gameObject.SetActive(false);
            StopCoroutine(FindPlayer());
            StartCoroutine(StunEnemy());
        }
       
    }

    public void StartFollowing()
    {

        int i = 0;


        while (i <= 0)
        {
            tempRandom = Random.Range(0, GameManager.Instance.transformActors.Length - 1);
            if (tempRandom != GameManager.Instance.ultimoTagged && tempRandom != id)
                i++;
        }


        playerTransform = GameManager.Instance.GetNewTarget(tempRandom);

        isPatrullando = false;
        isFollowing = true;
        render.material.color = taggedColor;
        //StartCoroutine(FindPlayer());
    }

    public void StartPatrullar()
    {
        render.material.color = baseColor;
        SetGoal();
        isPatrullando = true;
    }

    protected Vector3 GetTargetLocation()
    {
        Vector3 result = transform.position;

        Vector3 randomDirection = Random.insideUnitSphere * moveRadius;
        randomDirection += transform.position;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, moveRadius, 1))
        {
            result = hit.position;
        }

        return result;
    }


    void SetGoal()
    {
        // tempRandom = Random.Range(0, goals.Length);


        runGoal = GetTargetLocation();
        StartPath();
        // goal = goals[tempRandom];
    }

    void StartPath()
    {

        agent.SetDestination(runGoal);
    }

    void CheckPosition()
    {
        Vector3 positionGoal = runGoal;
        Vector3 positionPlayer = transform.position;
        //Debug.Log("distance" + Vector3.Distance(positionGoal, positionPlayer));
        if (Vector3.Distance(positionGoal, positionPlayer) < 4f)
        {
            // agent.Stop();
            SetGoal();


        }
    }



    public void SetTarget()
    {
        agent.SetDestination(playerTransform.position);
    }

    // Update is called once per frame
    void Update()
    {

        /* if (isPatrullando)
         {
             CheckPosition();
         }*/


    }

    private void CheckTargetTagged()
    {
        float distanceTarget = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceTarget < 2f)
        {
            if (playerTransform.gameObject.tag == "Enemy")
            {
                if (isFollowing)
                {
                    //StopAllCoroutines();
                    isFollowing = false;

                    StartPatrullar();
                    GameManager.Instance.NextTagged(id, playerTransform.gameObject.GetComponent<Actor>().id, playerTransform.gameObject);
                    render.material.color = baseColor;
                }
            }

            if (playerTransform.gameObject.tag == "Player")
            {
                if (isFollowing)
                {
                    //StopAllCoroutines();


                    isFollowing = false;

                    StartPatrullar();
                    render.material.color = baseColor;
                    GameManager.Instance.NextTagged(id, playerTransform.gameObject.GetComponent<Actor>().id);
                    playerTransform.gameObject.GetComponent<ActorController>().SetTagged(true);
                }
            }
        }
    }


    IEnumerator StunEnemy()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(FindPlayer());
    }

    IEnumerator FindPlayer()
    {
        for (int i = 0; i >= 0; i++)
        {

            yield return new WaitForSeconds(0.1f);

            if (isPatrullando)
            {
                CheckPosition();
                agent.SetDestination(runGoal);
            }
            else
            {
                CheckTargetTagged();
                SetTarget();
            }


        }

    }
}