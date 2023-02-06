using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour{
    public GameObject arrowPrefab;
    public Transform shootingPoint;
    public float shootingForce = 3;
    void Start(){
        gameObject.transform.SetParent(GameManager.instance.PlayerRef.bowPosition);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        ShowBow(false);
    }

    public void ShowBow(bool show){
        gameObject.SetActive(show);
    }

    public void Shoot(){
        GameObject arrow = Instantiate(arrowPrefab, shootingPoint.position, GameManager.instance.PlayerRef.transform.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.AddForce(GameManager.instance.PlayerRef.transform.forward * shootingForce, ForceMode.Impulse);
    }
}
