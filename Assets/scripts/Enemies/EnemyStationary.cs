using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class EnemyStationary : EnemyMovement{

    new void Awake(){
        base.Awake();
        ChangeDirection();
    }

    new protected void Update(){
        base.Update();
    }

    private void ChangeDirection(){
        targetPoint = Vector3.forward + new Vector3(
            Random.Range(transform.position.x - turnSpeed * 30, transform.position.x + turnSpeed * 30), // target X
            0, // target Y
            Random.Range(transform.position.z - turnSpeed * 30, transform.position.z + turnSpeed * 30) // target Z
        );
    }
}
