using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenCollect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}
