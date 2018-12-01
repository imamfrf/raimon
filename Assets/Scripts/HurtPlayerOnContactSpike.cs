using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerOnContactSpike : MonoBehaviour {

    public int damageToGive;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            //if (Input.GetKey(KeyCode.Space))
            //{
            //    HealthManager.HurtPlayer(0);
            //}
            //else
            //{
            //    HealthManager.HurtPlayer(damageToGive);
            //    var player = other.GetComponent<PlayerController>();
            //    player.knockbackCount = player.knockbackLength;
            //    if (other.transform.position.x < transform.position.x)
            //        player.knockFromRight = true;
            //    else
            //        player.knockFromRight = false;
            //}
            HealthManager.HurtPlayer(damageToGive);

             var player = other.GetComponent<PlayerController>();
             player.knockbackCount = player.knockbackLength;
            HealthManager.playerHealth = 0;
             if (other.transform.position.x < transform.position.x)
                 player.knockFromRight = true;
             else
                 player.knockFromRight = false;
        }
    }
}
