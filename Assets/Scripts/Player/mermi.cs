using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class mermi : MonoBehaviour
{
    private Rigidbody2D rgb;

    [SerializeField]
    private float mermiHizi;

    private Vector2 mermiYonu;
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rgb.velocity = mermiYonu * mermiHizi;
    }
    public void Initialize(Vector2 mermiYonu)
    {
        this.mermiYonu = mermiYonu;
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
