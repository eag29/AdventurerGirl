using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basamak : MonoBehaviour
{

    private BoxCollider2D karaktercollider;
    [SerializeField]
    private BoxCollider2D basamakcollider;
    [SerializeField]
    private BoxCollider2D basamakTrigger;

    void Start()
    {
        karaktercollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(basamakcollider, basamakTrigger, true);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(basamakcollider, karaktercollider, true);
        }
    }
     void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(basamakcollider, karaktercollider, false);
        }
    }
}
