using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Combat : NetworkBehaviour {

    public const int maxHealth = 100;
    public bool destroyOnDeath;

    [SyncVar]
    public int health = maxHealth;

    public void TakeDamage(int amount)
    {
        if (!isServer) return;

        health -= amount;
        if (health <= 0)
        {
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {
                health = maxHealth;
                RpcRespawn();
            }
            
        }
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            transform.position = new Vector3(0,.5f,0);            
        }
    }
}
