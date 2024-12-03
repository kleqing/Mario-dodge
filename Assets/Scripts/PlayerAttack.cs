using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    
    private Animator animator;
    private Player_Movement _playerMovement;
    private float cooldownTimer = Mathf.Infinity; // * Set to infinity to avoid attacking at the start of the game
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        _playerMovement = GetComponent<Player_Movement>();
    }
    
    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && _playerMovement.canAttack())
        {
            Attack();
        }
        
        //* Increase in every frame by time
        cooldownTimer += Time.deltaTime;
    }
    
    private void Attack()
    {
        animator.SetTrigger("Attack");
        cooldownTimer = 0;
        
        //* pool fireballs
        fireballs[FindFireBall()].transform.position = firePoint.position;
        fireballs[FindFireBall()].GetComponent<Fireball_Explode>().Direction(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireBall()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        
        return 0;
    }
}
