using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] int startHealth = 10;
    [SerializeField] Transform respawnPoint;
    [SerializeField] GameObject deadBody;
    [SerializeField] GameObject explosion;
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ParticleSystem fireParticle;
    [SerializeField] ParticleSystem smokeParticle;
    [SerializeField] Vector3 respawnOffset = new Vector3(0, 0.625f, 0);
    [SerializeField] GameObject deathText;

    int currentHealth;
    public int CurrentHealth { get { return currentHealth; } }

    Rigidbody rb;
    PlayerMovement playerMovement;
    Animator anim;
    AudioSource explosionAudioSource;
    UI_Handler uiHandler;

    Vector3 respawnPosition;


    bool isDead = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        explosionAudioSource = explosion.GetComponent<AudioSource>();
        uiHandler = GetComponent<UI_Handler>();

        respawnPosition = respawnPoint.position + respawnOffset;
        RespawnPlayer(respawnPosition);
    }

    void Update()
    {
        SelfDestruct();
        // "Q" to take damage = current health

        DeathEvent();
        // "Space" to respawn after death
        // spawns dead body at player pos

        //DEBUG();
        // "P" for 5 DamagePoints
        // "L" for Respawn
        // "O" for harmless Explosion
    }



    #region DeathMechanic
    private void DeathEvent()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
            deathText.SetActive(true);
            DisablePlayerControll();

            if (Input.GetKeyUp(KeyCode.Space))
            {
                StopParticles();
                SpawnDeadBody();
                RespawnPlayer(respawnPosition);
            }
        }
    }

    void DisablePlayerControll()
    {
        anim.SetTrigger("Death");
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.None;
        playerMovement.enabled = false;
        rb.drag = 1;
    }

    public void RespawnPlayer(Vector3 respawnPosition)
    {
        deathText.SetActive(false);
        uiHandler.DisableUI();
        StopParticles();
        explosion.SetActive(false);
        anim.ResetTrigger("Death");
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        rb.freezeRotation = true;
        transform.position = respawnPosition;
        transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1));
        currentHealth = startHealth;
        playerMovement.enabled = true;
        rb.drag = 0;
        isDead = false;
    }

    void StopParticles()
    {
        explosionParticle.Stop();
        fireParticle.Stop();
        smokeParticle.Stop();
    }

    void SpawnDeadBody()
    {
        Instantiate(deadBody, transform.position, transform.rotation);
    }
    #endregion

    #region SelfDestruction
    void SelfDestruct()
    {
        if(Input.GetKey(KeyCode.Q) && !isDead)
        {
            InstantDeath();
        }
    }

    void ExplodePlayer()
    {
        explosion.SetActive(true);
        explosionParticle.Play();
        fireParticle.Play();
        smokeParticle.Play();
        explosionAudioSource.Play();
    }

    #endregion

    #region DEBUG
    private void DEBUG()
    {
        //for Debug
        if (Input.GetKeyDown(KeyCode.P))
        {
            DamagePlayer(5);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            RespawnPlayer(respawnPosition);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ExplodePlayer();
        }
    }
    #endregion

    #region GameEnd

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "End")
        {
            uiHandler.HandleEndscreen(true);
        }
    }

    #endregion

    public void DamagePlayer(int damage)
    {
        currentHealth -= damage;
    }

    public void HealPlayer(int healing)
    {
        currentHealth -= healing;
    }

    public void InstantDeath()
    {
        ExplodePlayer();
        DamagePlayer(currentHealth);
    }

    
}
