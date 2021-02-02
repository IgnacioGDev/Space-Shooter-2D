using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 4f;
        [SerializeField]
        private Vector3 _spawnPos;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);

            if (transform.position.y < -5)
            {
                transform.position = new Vector3(Random.Range(-9.4f, 9.4f), 7.5f, 0);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    player.Damage();
                    Destroy(gameObject);

                }

            }
            else if (other.gameObject.CompareTag("Laser"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);

            }

        }
    }

}
