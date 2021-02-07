using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Managers;

namespace Scripts
{
    public class Laser : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        private bool _isEnemyLaser = false;

        // Update is called once per frame
        void Update()
        {
            if (_isEnemyLaser == false)
            {
                MoveUp();

            }
            else
            {
                MoveDown();

            }
        }

        void MoveUp()
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);

            if (transform.position.y > Screen.height / 100)
            {
                //checks if the laser has a parent gameobject
                if (transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }
                Destroy(gameObject, 0.5f);
            }
        }

        void MoveDown()
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);

            if (transform.position.y < -8)
            {
                Destroy(gameObject);
            }
        }

        public void AssignEnemyLaser()
        {
            _isEnemyLaser = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && _isEnemyLaser == true)
            {
                Player.Instance.Damage();
                Destroy(gameObject);
            }
        }

    }

}
