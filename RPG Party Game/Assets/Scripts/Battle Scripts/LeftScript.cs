using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftScript : MonoBehaviour
{
    // Renders Character Sprite
    public Sprite character;

    // Access to sprite renderer for right script
    public SpriteRenderer spriteRenderer; 
    
    // Start is called before the first frame update
    void Start()
    {
        // Renders Sprite Input
        spriteRenderer.sprite = character;

        // Sets Sprite so it is lined up at y = 0
        float offSetSprite = spriteRenderer.bounds.size.y/2;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + offSetSprite, this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
