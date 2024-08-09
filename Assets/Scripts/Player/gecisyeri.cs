using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gecisyeri : MonoBehaviour
{
    [SerializeField]
    private GameObject kapi;
    [SerializeField]
    private GameObject kapi2;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player" && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            kapi.SetActive(false);
            kapi2.SetActive(false);
            Application.LoadLevel("level2");
        }
    }
}
