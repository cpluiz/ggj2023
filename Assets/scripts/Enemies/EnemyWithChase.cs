using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class EnemyWithChase : EnemyMovement{
    new void Awake(){
        base.Awake();
        ChangeDirection();
        StartCoroutine(nameof(MaybeChangeDirection));
    }

    new protected void Update(){
        base.Update();
        base.MoveEnemy();
    }

    private void ChangeDirection(){
        targetPoint = Vector3.forward + new Vector3(
            Random.Range(transform.position.x - turnSpeed * 30, transform.position.x + turnSpeed * 30), // target X
            0, // target Y
            Random.Range(transform.position.z - turnSpeed * 30, transform.position.z + turnSpeed * 30) // target Z
        );
    }

    protected IEnumerator MaybeChangeDirection(){
        yield return new WaitForSeconds(Random.Range(2, 5));
        if(Random.Range(0, 101) > 30){
            ChangeDirection();
        }
        StartCoroutine(nameof(MaybeChangeDirection));
    }
}
