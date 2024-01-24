using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //variable to determine if the damage function can be called
    private bool canHit = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.name);

        IDamageable hit = other.GetComponent<IDamageable>();
        if (hit != null)
        {
            //if can attack
            if (canHit)
            {
                hit.Damage();
                //set the variable to false
                canHit = false;

                StartCoroutine(resetHit());
            }
        }
    }

    //coroutine to reset variable after 0.5f
    IEnumerator resetHit()
    {
        yield return new WaitForSeconds(0.5f);
        canHit = true;
    }
}
