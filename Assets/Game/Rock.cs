using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class Rock : Faller
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.Damage();
        }
        if (other.CompareTag("Border") || other.CompareTag("Player"))
        {
            GameManager.Instance.SpawnRock();
            Destroy(gameObject);
        }
    }
}
