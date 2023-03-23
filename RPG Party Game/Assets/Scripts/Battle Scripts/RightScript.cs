using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightScript : MonoBehaviour
{
    // Renders Character Sprite
    public Sprite character;

    // Access to sprite renderer for right script
    public SpriteRenderer spriteRenderer;

    // Variable to see if combat is between two players
    public bool isAnotherPlayer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = character;
        float offSetSprite = spriteRenderer.bounds.size.y/2;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + offSetSprite, this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
