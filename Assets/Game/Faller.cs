using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Faller : MonoBehaviour
{
    [SerializeField] GameObject pickUpParticle;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = GameManager.Instance.ObjectSpeed * Vector2.down;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance.IsGameOver == true) return;
        var particle = Instantiate(pickUpParticle,transform.position,Quaternion.identity);
        Destroy(particle, 2f);
        
    }


}
