using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalBehavior : MonoBehaviour
{
    [SerializeField] private Text victoryText;
    // Start is called before the first frame update
    void Start()
    {
        victoryText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            victoryText.enabled = true;
            PlayerMovement playerMovement = col.gameObject.GetComponentInChildren<PlayerMovement>();
            playerMovement.movementEnabled = false;
        }
    }
}
