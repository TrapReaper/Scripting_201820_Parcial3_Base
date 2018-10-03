using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private ActorController[] players;

    private float gameTime = 25F;
    public Text time;
    private int timer;
    public int ActiveAI;

    public GameObject AI;
    public Transform[] spawn = new Transform[4];
    public GameObject enemigo;

    public float CurrentGameTime { get; private set; }

    // Use this for initialization
    private IEnumerator Start()
    {
        CurrentGameTime = gameTime;

        // Sets the first random tagged player
        players = FindObjectsOfType<ActorController>();

        yield return new WaitForSeconds(0.5F);

        players[Random.Range(0, players.Length)].onActorTagged(true);
    }

    private void Awake()
    {
        if(ActiveAI > 3)
        {
            ActiveAI = 3;
            Debug.Log("Solo se pueden 5");
        }
         for (int i = 0; i <= ActiveAI; i++)
         {
             AI = Instantiate(enemigo, spawn[i].position, spawn[i].rotation);
         }      
        
    }

    private void Update()
    {
        if (CurrentGameTime > 0F)
        {
            CurrentGameTime -= Time.deltaTime;
            timer = (int)CurrentGameTime;
            time.text = timer.ToString();
        }
        

        if (CurrentGameTime <= 0F)
        {
            Debug.Log("Se acabo perra");
            time.text = "00";
            Time.timeScale = 0;
            //TODO: Send GameOver event.
        }
    }
}