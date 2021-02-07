using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Managers;

namespace Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 4f;
        [SerializeField]
        private Vector3 _spawnPos;
        [SerializeField]
        private int _pointsValue;
        [SerializeField]
        private Animator _anim;
        [SerializeField]
        private BoxCollider2D _boxCollider2D;
        [SerializeField]
        private GameObject _laserEnemy;
        [SerializeField]
        private float _fireRate;
        [SerializeField]
        private float _canFire = -1;

        // Start is called before the first frame update
        void Start()
        {
            _anim = GetComponent<Animator>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        // Update is called once per frame
        void Update()
        {
            Movement();
            Shot();
        }

        private void Movement()
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);

            if (transform.position.y < -5)
            {
                transform.position = new Vector3(Random.Range(-9.4f, 9.4f), 7.5f, 0);
            }
        }

        private void Shot()
        {
            if (_canFire < Time.deltaTime)
            {
                AudioManager.Instance.PlayLaserSound();
                _fireRate = Random.RandomRange(3f, 7f);
                _canFire = Time.deltaTime + _fireRate;
                GameObject laser = Instantiate(_laserEnemy, transform.position, Quaternion.identity);
                Laser[] lasers = laser.GetComponentsInChildren<Laser>();

                for (int i = 0; i < lasers.Length; i++)
                {
                    lasers[i].AssignEnemyLaser();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {


            if (other.gameObject.CompareTag("Player"))
            {
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    AudioManager.Instance.PlayExplosionSound();
                    player.Damage();
                    _anim.SetTrigger("OnEnemyDeath");
                    _boxCollider2D.enabled = false;
                    _speed /= 2;
                    Destroy(gameObject, 2.8f);

                }

            }
            else if (other.gameObject.CompareTag("Laser"))
            {
                AudioManager.Instance.PlayExplosionSound();
                Destroy(other.gameObject);
                Player.Instance.AddScore(_pointsValue);
                _anim.SetTrigger("OnEnemyDeath");
                _boxCollider2D.enabled = false;
                _speed /= 2;
                Destroy(gameObject, 2.8f);

            }

        }
    }

}
