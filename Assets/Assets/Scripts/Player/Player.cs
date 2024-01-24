using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    //get handle to rigidbody2D
    private Rigidbody2D rb;
    //variable for jumpforce
    [SerializeField]
    private float _jumpForce = 5.0f;
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private LayerMask _groundLayer;
    private bool resetJump = false;
    private bool _grounded = false;
    //handle for PlayerAnimation
    private PlayerAnimation _playerAnimation;
    private SpriteRenderer _playerFlipSprite;
    private SpriteRenderer _flipSwordArc;
    private GameManager _gameManager;
    private Animator _jumpAnim;

    //variable for amount of diamonds
    public int diamondAmount;

    public int Health { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        //assign handle of rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        //assign handle of PlayerAnimation
        _playerAnimation = GetComponent<PlayerAnimation>();
        _gameManager = GetComponent<GameManager>();

        _playerFlipSprite = GetComponentInChildren<SpriteRenderer>();
        _flipSwordArc = transform.GetChild(1).GetComponent<SpriteRenderer>();

        _jumpAnim = GetComponentInChildren<Animator>();
        //playerVector3 = GetComponentInChildren<Vector3>();
        Health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        //if left click && IsGrounded
        //attack
        if (CrossPlatformInputManager.GetButtonDown("A_Button") && IsGrounded())
        {
            _playerAnimation.Attack();
        }
    }

    void Movement()
    {
        //horizontal input for left/right
        float move = CrossPlatformInputManager.GetAxis("Horizontal"); //Input.GetAxisRaw("Horizontal");

        _grounded = IsGrounded();

        if (move > 0)
        {
            Flip(true);
        }
        else if (move < 0)
        {
            Flip(false);
        }

        if ((CrossPlatformInputManager.GetButtonDown("B_Button") && IsGrounded() == true) || (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true))
        {
            //Debug.Log("Jump");
            rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            //tell animator to jump
            _playerAnimation.Jump(true);
        }

        //current velocity = new velocity (horizontal input, current velocity.y)
        rb.velocity = new Vector2(move * _speed, rb.velocity.y);

        _playerAnimation.Move(move);

        /*if (Input.GetMouseButtonDown(0) && IsGrounded())
        {
            _playerAnimation.Attack();
        }*/
    }

    void Flip(bool FaceRight)
    {
        if (FaceRight)
        {
            _playerFlipSprite.flipX = false;
            _flipSwordArc.flipX = false;
            _flipSwordArc.flipY = false;

            Vector3 newPos = _flipSwordArc.transform.localPosition;
            newPos.x = 1.01f;
            _flipSwordArc.transform.localPosition = newPos;
        }
        else if (!FaceRight)
        {
            _playerFlipSprite.flipX = true;
            _flipSwordArc.flipX = true;
            _flipSwordArc.flipY = true; 
            
            Vector3 newPos = _flipSwordArc.transform.localPosition;
            newPos.x = -1.01f;
            _flipSwordArc.transform.localPosition = newPos;
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);
        //Debug.DrawRay(transform.position, Vector2.down, Color.green);

        if(hitInfo.collider != null)
        {
            if(resetJump == false)
            {
                //set animator bool to false
                _playerAnimation.Jump(false);
                return true;
            }
        }

        return false;
    }

    IEnumerator ResetJumpRoutine()
    {
        resetJump = true;
        yield return new WaitForSeconds(0.1f);
        resetJump = false;
    }

    public void Damage()
    {
        if (Health < 1)
        {
            return;
        }

        Debug.Log("Player: Damage()");
        //remove 1 health
        Health -= 1;
        //update UI display
        UIManager.Instance.UpdateLives(Health);
        //check for dead
        //play death animation
        if (Health < 1)
        {
            _playerAnimation.Death();
        }
    }

    public void AddGems(int amount)
    {
        diamondAmount += amount;
        UIManager.Instance.UpdateGemCount(diamondAmount);
    }
}
