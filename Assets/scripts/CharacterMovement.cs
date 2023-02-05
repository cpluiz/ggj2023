using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Range(1f,20f)]
    public float velocity = 10.0f;
    [Range(5f,50f)]
    public float turnVelocity = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        // float mouseX = Input.GetAxis("Mouse x");
        transform.Translate(transform.right * -x * velocity * Time.deltaTime);
        transform.Translate(transform.forward * y * velocity * Time.deltaTime);
        if (x != 0 || y != 0)
        {
            transform.forward = Vector3.Normalize(new Vector3(x, 0f, y));
        }
    }
}
