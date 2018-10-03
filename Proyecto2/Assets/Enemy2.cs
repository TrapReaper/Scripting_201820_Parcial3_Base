using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour {

    public NavMeshAgent agent;

    public Transform playerTransform;

    private void Start()
    {
        StartCoroutine(FindPlayer());
    }

    public void SetTarget()
    {
        agent.SetDestination(playerTransform.position);
    }


    IEnumerator FindPlayer()
    {
        for(int i =0; i>=0;i++)
        {
            
            yield return new WaitForSeconds(1f);

            SetTarget();
        }
       
    }

}
