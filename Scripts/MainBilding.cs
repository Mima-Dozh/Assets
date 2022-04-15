using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MainBilding : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float MaxHealth;
    private float CurrentHealth;
    public float DamageForceThreshold = 1f;
    public float DamageForceScale = 5f;
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        var force = other.relativeVelocity.magnitude;
        if (force > DamageForceThreshold) 
        {
            CurrentHealth -= (int)((force - DamageForceThreshold) * DamageForceScale);
            CurrentHealth = Mathf.Max(0, CurrentHealth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
