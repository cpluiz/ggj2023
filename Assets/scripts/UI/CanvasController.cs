using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour{
    [SerializeField] private Image lifeImage;
    [SerializeField] private Sprite[] lifeSprites;

    public void SetLifes(int lifes){
        lifeImage.sprite = lifeSprites[Mathf.Clamp(lifes, 0, 5)];
    }
}
