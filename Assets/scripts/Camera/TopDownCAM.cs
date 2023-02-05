using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCAM : MonoBehaviour
{

    public float velocidade = 3.0f;
    public Transform target;
    Vector3 mousePos;
    public Vector3 offset = Vector3.up;
    private Vector3 originalOffset;
    public Camera cam;
    public bool LookAt = false;

    void Start(){
        target = GameManager.instance.PlayerRef.transform;
        originalOffset = offset;
    }

    // Update is called once per frame
    void Update(){
        transform.position = Vector3.Lerp(transform.position, target.position + offset, velocidade * Time.deltaTime);

        if (LookAt){ 
            transform.LookAt(target.position);
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void ChangeCamera(Vector3 newOffset){
        offset = newOffset;
    }

    public void RestoreCamera(){
        offset = originalOffset;
    }
}
