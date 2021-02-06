using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Managers
{
    public class LaserEnemy : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Direction();
            DestroyLaser();
        }

        void Direction()
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Player.Instance.Damage();
                Destroy(gameObject);
            }
        }

        void DestroyLaser()
        {
            if (transform.position.y < -8)
            {
                Destroy(gameObject);
            }
        }
    }
}

