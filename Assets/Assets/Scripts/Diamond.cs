using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public int gems = 1;

    //ontriggerenter2D to collect
    //check for player
    //add the value of the diamond to player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.AddGems(gems);
                Destroy(this.gameObject);
            }
        }
    }
}
