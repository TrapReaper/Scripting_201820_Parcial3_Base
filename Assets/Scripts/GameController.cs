using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private ActorController[] players;

    private float gameTime = 5F;
    public Text time;
    private int timer;

    public int numb;
    public float CurrentGameTime { get; private set; }

    // Use this for initialization
    private void start()
    {
        numb = 3;
    }
    private IEnumerator Start()
    {
        CurrentGameTime = gameTime;

        // Sets the first random tagged player
        players = FindObjectsOfType<ActorController>();

        yield return new WaitForSeconds(0.5F);

        players[Random.Range(0, players.Length)].onActorTagged(true);
    }

    private void Update()
    {
        if(CurrentGameTime > 0F)
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
    /*
    public void Addplayer()
    {
        numb += 1;
        NumPlayers(numb);
    }
    public void Restplayer()
    {
        numb -= 1;
        NumPlayers(numb);
    }

    public void NumPlayers(int n)
    {
        if(n<= 3)
        {
            n = 3;
        }
        players = new ActorController[n];
    }
    */
}