using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    // Update is called once per frame
    void Update()
    {
        LaserMovement();
        DestroyLaser();
        
    }

    void LaserMovement()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    void DestroyLaser()
    {
        if (transform.position.y > Screen.height/100)
        {
            //checks if the laser has a parent gameobject
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject, 0.5f);
        }
    }
}
