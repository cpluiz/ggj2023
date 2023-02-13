using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpinner : MonoBehaviour
{
    [SerializeField][Range(10f, 400f)] private float spinSpeed;
    [SerializeField] private bool spinClockwise = true;
    void Start(){
        
    }
    void Update(){
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime * (spinClockwise ? 1 : -1));
    }
}
