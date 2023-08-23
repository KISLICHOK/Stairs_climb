using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Sprite Walk1;
    [SerializeField] Sprite Walk2;
    [SerializeField] Sprite Dash;
    [SerializeField] AudioSource soundSource;
    [SerializeField] private AudioClip stepSound;

    public void ChangeSpriteWalk(){
        Sprite temp = gameObject.GetComponent<SpriteRenderer>().sprite;
        gameObject.GetComponent<SpriteRenderer>().sprite = (temp == Walk1)? Walk2 : Walk1;
    }
    public void FallPlayer(){
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.5f, -4f));
        gameObject.GetComponent<Rigidbody2D>().AddTorque(10f);
    }

    public void MakeStepSound(){
        soundSource.PlayOneShot(stepSound);
    }
}
