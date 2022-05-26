using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 1f;

    DeathHandler deathHandler;


    void Start()
    {
        deathHandler = FindObjectOfType<DeathHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateSpeed, 0, 0);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Bot")
        {
            deathHandler.InstantDeath();
        }
    }
}
