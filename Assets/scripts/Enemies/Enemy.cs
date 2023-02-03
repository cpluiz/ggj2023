using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyMovement))]

public class Enemy : MonoBehaviour{
    Animator enemyAnimator;

    void Awake(){
        if(enemyAnimator == null){enemyAnimator = gameObject.GetComponent<Animator>();}
    }

    void Start(){
        enemyAnimator.Play("Idle");
    }
}
