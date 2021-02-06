using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Managers;

namespace Scripts
{
    public class Powerup : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        [SerializeField]
        private int _powerUpID;
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
            transform.position += new Vector3(0, -1, 0) * Time.deltaTime * _speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                AudioManager.Instance.PlayPowerUpSound();

                switch (_powerUpID)
                {
                    case 0:
                        Player.Instance.AcivateTripleShot();
                        break;
                    case 1:
                        Player.Instance.ActivateSpeedPowerup();
                        break;
                    case 2:
                        Player.Instance.ActivateShieldPowerup();
                        break;
                    default:
                        Debug.Log("DEFAULT VALUE!");
                        break;
                }
                Destroy(gameObject);
            }

        }
    }

}
