using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float timeToSurvie;

    public Text timerUi;

    public bool isWin;



    // Use this for initialization
    void Start()  
    {
        isWin = false;
        StartCoroutine(Timer());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StopAllCoroutines();
            SetWin();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetWin()
    {
        isWin = true;
        Debug.Log("Ganaste");
    }

    public void SetLost()
    {
        StopAllCoroutines();
        Debug.Log("Perdiste");
    }

    IEnumerator Timer()
    {
        for (float i = 0; i < timeToSurvie; i = +Time.fixedTime)
        {
            int fixedNum = Mathf.RoundToInt(i);
            timerUi.text = "" + fixedNum;
            yield return null;
        }

        Debug.Log("Perdiste");
    }
}

