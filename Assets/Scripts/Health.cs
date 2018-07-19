using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Health : NetworkBehaviour
{
    public int maxHealth;

    [SyncVar(hook = "OnHealthChange")]
    public int currentHealth;
    public Text HealthScore;
        	
	void Start ()
    {
        HealthScore.text = currentHealth.ToString();
    }
	
    public void TakeDamage(int howMuch)
    {
        if (!isServer) { return; }

        var newHealth = currentHealth - howMuch;
        if (newHealth <= 0)
        {
            //Debug.Log("Dead");
            RpcDeath();
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth = newHealth;         
        }
    }

    void OnHealthChange(int updatedHealth)
    {
        HealthScore.text = updatedHealth.ToString();
    }

    /// <summary>
    /// Remote Procedure Call (Rpc) to deal with the Player's death.
    /// An Rpc is a method that's issued on the server, but executed on the clients.
    /// </summary>
    [ClientRpc]
    void RpcDeath()
    {
        if(isLocalPlayer)
        {
            //transform.position = Vector3.zero;
            var spawnPoints = FindObjectsOfType<NetworkStartPosition>();
            var chosenPoint = Random.Range(0, spawnPoints.Length);
            transform.position = spawnPoints[chosenPoint].transform.position;
        }
    }
}
