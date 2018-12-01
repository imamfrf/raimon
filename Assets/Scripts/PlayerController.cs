using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    private float moveVelocity;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;

    private bool doubleJumped;
    private Animator anim;

    public Transform firePoint;
    public GameObject ninjaStar;
    public GameObject ninjaStar2;

    public float shotDelay;
    public float shotDelayCounter;

    public float punchDelay;
    public float punchDelayCounter;

    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockFromRight;
    // Use this for initialization

    private bool ready;

    private bool punchReady = true;

    private bool right = true;

    public int damageToGive;

    public AudioClip nyakar;
    public AudioClip lompat;
    public AudioClip jalan;
    public float vol;
    AudioSource audios;


    void Start()
    {
        anim = GetComponent<Animator>();
        audios = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    // Update is called once per frame
    void Update()
    {


        if (grounded)
        {
            doubleJumped = false;
            anim.SetBool("Grounded", true);
            anim.SetBool("Jump", false);
        }
        else
        {
            anim.SetBool("Grounded", false);
            anim.SetBool("Jump", true);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            anim.SetBool("Grounded", false);
            jump();
            doubleJumped = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !grounded && !doubleJumped)
        {
            jump();
            doubleJumped = true;
        }



        moveVelocity = 0f;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y );
            if (!audios.isPlaying)
            {
                audios.clip = jalan;
                audios.Play();
            }
            moveVelocity = moveSpeed;
            right = true;

        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            audios.Stop();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            if (!audios.isPlaying)
            {
                audios.clip = jalan;
                audios.Play();
            }
            moveVelocity = -moveSpeed;
            right = false;
        }

        if (knockbackCount <= 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            if (knockFromRight)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-knockback, knockback);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(knockback, knockback);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            knockbackCount -= Time.deltaTime;
        }


        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));

        if (GetComponent<Rigidbody2D>().velocity.x > 0 || knockFromRight == true)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            knockFromRight = false;
        }
        else if (GetComponent<Rigidbody2D>().velocity.x < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        if (ready)
        {
            if (right == true)
            {
                if (Input.GetKey(KeyCode.Return))
                {
                    if (shotDelayCounter <= 0)
                    {
                        Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
                        shotDelayCounter = shotDelay;
                        ready = false;
                    }
                }
            }
            else if (right == false)
            {
                if (Input.GetKey(KeyCode.Return))
                {
                    if (shotDelayCounter <= 0)
                    {
                        Instantiate(ninjaStar2, firePoint.position, firePoint.rotation);
                        shotDelayCounter = shotDelay;
                        ready = false;
                    }
                }

            }

        }
        else
        {
            if (shotDelayCounter <= 0)
            {
                ready = true;
            }
            else if (shotDelayCounter > 0)
            {
                shotDelayCounter -= Time.deltaTime;
            }
        }

        if (anim.GetBool("Punch"))
        {
            anim.SetBool("Punch", false);
        }
        if (punchReady)
        {

            if (Input.GetKey(KeyCode.Space))
            {
                if (punchDelayCounter <= 0)
                {
                    audios.PlayOneShot(nyakar, vol);
                    anim.SetBool("Punch", true);
                    punchDelayCounter = punchDelay;
                    punchReady = false;
                }
            }
        }
        else
        {
            if (punchDelayCounter <= 0)
            {
                punchReady = true;
            }
            else if (punchDelayCounter > 0)
            {
                punchDelayCounter -= Time.deltaTime;
            }
        }



        //if (Input.GetKey(KeyCode.Space))
        //{
        //    anim.SetBool("Punch", true);
        //}
    }

    void jump()
    {

        audios.PlayOneShot(lompat, vol);
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                anim.SetBool("Punch", true);
                other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
            }
        }
    }
}
