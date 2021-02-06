using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Managers;

namespace Scripts
{
    public class Astroid : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _rotationSpeed;
        [SerializeField]
        private GameObject _explosionPref;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Movement();
        }

        private void Movement()
        {
            //transform.Translate(Vector3.down * _speed *  Time.deltaTime);
            transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Laser"))
            {
                AudioManager.Instance.PlayExplosionSound();
                GameObject explosion =  Instantiate(_explosionPref, transform.position, Quaternion.identity);
                Destroy(other.gameObject);
                Destroy(gameObject, 0.25f);
                Destroy(explosion, 2f);
                SpawnManager.Instance.StartSpawning();
            }
        }
    }
}

