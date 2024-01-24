using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //handle to animator component
    public Animator _anim;
    //reference to sword animation
    private Animator _swordAnim;
    // Start is called before the first frame update
    void Start()
    {
        //assign the handle for animator
        _anim = GetComponentInChildren<Animator>();

        _swordAnim = transform.GetChild(1).GetComponent<Animator>();
    }

    public void Move(float move)
    {
        //anim set float Move, move
        _anim.SetFloat("Move", Mathf.Abs (move));
    }

    public void Jump(bool jumping)
    {
        _anim.SetBool("Jumping", jumping);
    }

    //attack method
    public void Attack()
    {
        _anim.SetTrigger("Attack");
        //play sword animation
        _swordAnim.SetTrigger("SwordAnimation");
    }

    public void Death()
    {
        _anim.SetTrigger("Death");
    }
}
