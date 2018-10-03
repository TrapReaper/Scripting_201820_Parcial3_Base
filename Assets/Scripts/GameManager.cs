using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;


    public event Action OnEndGame;

    public int[] scorePlayer = new int[5];

    public Transform[] transformActors;

    public Text[] scoreUI;

    public int ultimoTagged;

    public int actualTagged;

    public Text winner;


    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        actualTagged = 1;


        UpdateUi();
    }

    private void Start()
    {
        for (int i = 0; i < transformActors.Length; i++)
        {
            transformActors[i].gameObject.GetComponent<Actor>().id = i;
        }
        transformActors[1].gameObject.GetComponent<Enemy>().StartFollowing();
    }

    public void NextTagged(int selfID, int otherID, GameObject newTagget)
    {

        ultimoTagged = actualTagged;
        actualTagged = otherID;
        newTagget.GetComponent<Enemy>().StartFollowing();

        scorePlayer[otherID]++;
        UpdateUi();
    }

    public void NextTagged(int selfID, int otherID)
    {

        ultimoTagged = actualTagged;
        actualTagged = otherID;

        scorePlayer[otherID]++;
        UpdateUi();
    }

    public void FinJuego()
    {
        OnEndGame();
        SetWinner();
    }

    public Transform GetNewTarget(int indexTarget)
    {
        return transformActors[indexTarget];

    }

    public void UpdateUi()
    {
        for (int i = 0; i < scorePlayer.Length; i++)
        {
            scoreUI[i].text = "Player " + i + "Tagged " + scorePlayer[i];
        }
    }

    private void SetWinner()
    {
        int tempWiner = 0;

        int countEmpate = 0;

        for (int i = 1; i < scorePlayer.Length; i++)
        {
            if (scorePlayer[i] < scorePlayer[tempWiner])
            {
                tempWiner = i;
            }
        }

        for (int i = 0; i < scorePlayer.Length; i++)
        {
            if (scorePlayer[i] == scorePlayer[tempWiner])
            {
                countEmpate++;
            }
        }

        if (countEmpate > 1)
        {
            winner.text = "Empate";
        }
        else
        {
            winner.text = "Jugador " + tempWiner + " Gana";
        }
    }




}