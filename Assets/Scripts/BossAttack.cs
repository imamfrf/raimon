using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{

    private Animator anim;

    public Transform playerCheck;
    public float playerCheckRadius;
    public LayerMask whatIsPlayer;
    private bool nearToPlayer=false;
    public int damageToGive;

    public AudioClip punch;
    public float vol;
    AudioSource audios;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        audios = GetComponent<AudioSource>();

    }

    void FixedUpdate()
    {
        nearToPlayer = Physics2D.OverlapCircle(transform.position, playerCheckRadius,1<< LayerMask.NameToLayer("Player"));
        if (nearToPlayer==true)
        {
            Debug.Log("y");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (nearToPlayer)
        {
            if (!audios.isPlaying)
            {
                audios.PlayOneShot(punch, vol);
            }
            anim.SetBool("nearToPlayer", true);
            HealthManager.playerHealth -= damageToGive;
        }
        else
        {
            anim.SetBool("nearToPlayer", false);
        }

        if (anim.GetBool("nearToPlayer"))
        {
            if (!audios.isPlaying)
            {
                audios.PlayOneShot(punch, vol);
            }
            HealthManager.playerHealth -= damageToGive;
            anim.SetBool("Punch", false);
        }
    }
}

    
