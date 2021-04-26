using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalBehavior : MonoBehaviour
{
    [SerializeField] private Text victoryText;
    private bool wasReached = false;
    // Start is called before the first frame update
    void Start()
    {
        victoryText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(wasReached && Input.GetButtonDown("Restart"))
        {
            wasReached = false;
            var players = GameObject.FindGameObjectsWithTag("Player");
            if (players != null)
                foreach (GameObject player in players)
                {
                    PlayerMovement playerMovement = player.GetComponentInChildren<PlayerMovement>();
                    playerMovement.movementEnabled = true;
                    playerMovement.resetPosition();
                }
        }
            
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            victoryText.enabled = true;
            wasReached = true;
            PlayerMovement winningPlayer = col.gameObject.GetComponentInChildren<PlayerMovement>();
            var players = GameObject.FindGameObjectsWithTag("Player");
            if (players != null)
                foreach (GameObject player in players)
                    player.GetComponentInChildren<PlayerMovement>().movementEnabled = false;
            if (winningPlayer.isPlayerOne)
                victoryText.text = "Player 1 ";
            else
                victoryText.text = "Player 2 ";
            victoryText.text += "won. Yay. Press R to restart the game.";
        }
    }
}
