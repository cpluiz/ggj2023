using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]

public class CameraTrigger : MonoBehaviour{
    public bool restoreCameraToOriginal;
    public Vector3 newCameraOffset;
    private void OnTriggerEnter(Collider other){
        if(restoreCameraToOriginal){
            GameManager.instance.FollowCamRef.RestoreCamera();
        }else{
            GameManager.instance.FollowCamRef.ChangeCamera(newCameraOffset);
        }
    }
}
