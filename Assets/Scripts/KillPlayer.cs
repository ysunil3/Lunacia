﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pubsub;

public class KillPlayer : MonoBehaviour
{
    public GameObject player;
    public static Vector2 respawnLocation; // this will always spawn the player at (0,0) when first starting up the scene
    private static bool addedMessageBroker = false;
   
    void Awake() {
        if (!addedMessageBroker) { 
            MessageBroker.Instance.playerDeath += consumePlayerDeathEvent;
            addedMessageBroker = true;
        }
        player.transform.position = respawnLocation;
    }
    static void consumePlayerDeathEvent(object sender, PlayerDeathEventArguments death) {
		print(death.deathMessage);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Respawned at: " + respawnLocation);
	}
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Hazard")
        {
            MessageBroker.Instance.Raise(new PlayerDeathEventArguments("Player died!"));
        }
    }

}
