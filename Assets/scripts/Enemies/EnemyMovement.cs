using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] protected bool followPlayer;
    [Range(20, 500)]
    public float aggroRange;
    [Range(0.1f, 4f)]
    public float turnSpeed, moveSpeed = 1;
    [SerializeField]
    protected Vector3 lookAt, targetPoint;

    protected void Awake(){
        lookAt = Vector3.forward;
        targetPoint = Vector3.forward;
    }
    protected void Update(){
        followPlayer = PlayerIsOnAggroRange();
        if(followPlayer){FollowPlayerMovement();}else{DefaultMovement();}
    }

    protected bool PlayerIsOnAggroRange(){
        if(GameManager.instance.PlayerRef == null) return false;
        bool isFollowing = Vector3.Distance(this.transform.position, GameManager.instance.PlayerRef.transform.position) <= aggroRange;
        return isFollowing;
    }

    protected void MoveEnemy(){
        transform.position += transform.forward * Time.deltaTime * moveSpeed;    
    }

    protected void DefaultMovement(){
        lookAt = targetPoint - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt), turnSpeed/2 * Time.deltaTime);
    }

    protected void FollowPlayerMovement(){
        lookAt = GameManager.instance.PlayerRef.transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt), turnSpeed * Time.deltaTime);
    }
}
