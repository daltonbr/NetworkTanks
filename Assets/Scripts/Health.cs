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
            Debug.Log("Dead");
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
}
