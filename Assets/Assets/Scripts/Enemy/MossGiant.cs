using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    public int Health { get; set; }

    //use for initialization
    public override void Init()
    {
        base.Init();

        Health = base.health;
    }

    public override void Movement()
    {
        base.Movement();
    }

    public void Damage()
    {
        if (isDead)
        {
            return;
        }

        Debug.Log("MossGiant::Damage()");
        //subtract 1 from health
        Health -= 1;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);

        //if health is less than 1
        //destroy the object
        if (Health < 1)
        {
            anim.SetTrigger("Death");
            isDead = true;
            //spawn a diamond
            //change the value of diamond whatever my gem count is
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }
}