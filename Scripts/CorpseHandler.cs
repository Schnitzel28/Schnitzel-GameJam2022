using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseHandler : MonoBehaviour
{
    [SerializeField] int corpseMaxHP = 2;
    [SerializeField] int damageToCorpse = 1;

    [SerializeField] int corpseCurrentHP;


    void Awake()
    {
        corpseCurrentHP = corpseMaxHP;
    }

    void Update()
    {
        DestroyCorpse();
    }

    void DestroyCorpse()
    {
        if (corpseCurrentHP < 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void DamageCorpse(int damage)
    {
        corpseCurrentHP -= damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "CorpseDamager")
        {
            DamageCorpse(damageToCorpse);
        }
    }
}
