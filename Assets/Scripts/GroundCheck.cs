using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    PlayerMovement playerMovement;

    bool isOnGround = true;
    public bool IsOnGround { get { return isOnGround; } }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void SetGroundedFalse()
    {
        isOnGround = false;
    }

    public void SetGroundedTrue()
    {
        isOnGround = true;
    }

    void OnTriggerEnter(Collider other)
    {
        SetGroundedTrue();
    }
}
