using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Managers
{
    public class Player : MonoBehaviour
    {
        private static Player _instance;
        public static Player Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.Log("Player can't be null!!");
                }
                return _instance;
            }
        }

        //*******************PLAYER STATS VARIABLES********************
        [SerializeField]
        private float _speed;
        [SerializeField]
        private int _lives = 3;
        [SerializeField]
        private bool _isAlive = true;
        [SerializeField]
        private int _score;


        //*******************LASER/SHOT VARIABLES********************
        [SerializeField]
        private GameObject _laserPrefab;
        [SerializeField]
        private GameObject _tripleShotPref;
        [SerializeField]
        private Vector3 _laserOffset = new Vector3(0, 1.05f, 0);
        [SerializeField]
        private float _fireRate = 0.5f;
        private float _canFire = -1f;

        //******************POWERUPS*********************************
        [SerializeField]
        private bool _isTripleShotPowerupActive = false;
        [SerializeField]
        private bool _isSpeedPowerupActive = false;
        [SerializeField]
        private bool _isShieldPowerUpActive = false;
        [SerializeField]
        private GameObject _shieldPrefab;

        //****************ANIMATIONS/OTHERS**************************
        [SerializeField]
        private GameObject _damage;
        [SerializeField]
        private GameObject _damageCritical;



        private void Awake()
        {
            _instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            //take the curent position = new position (0,0,0)
            transform.position = Vector3.zero;
        }

        // Update is called once per frame
        void Update()
        {
            CalculateMovement();

            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
            {
                FireLaser();
            }

            ShowDamage();
        }

        private void CalculateMovement()
        {
            //Gets the values of X and Y axis into horizontalInput and verticalInpit respectively
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            //New vector 3 is created to capture the horizontal and vertical axes for use them later in transform.translate.
            //This variable has already the speed and Time.deltaTime values implemented.
            Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

            //Bounds to the Y axis. Using the Mathf.Clamp, it sets limits to the player's movement in the Y axis to -3.8 until 0.
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

            //Wraps X axis. If the player moves outside the screen to the left, then will reappear in the right of the screen.
            if (transform.position.x < -11.4f)
            {
                transform.position = new Vector3(11.4f, transform.position.y, 0);
            }
            else if (transform.position.x > 11.4f)
            {
                transform.position = new Vector3(-11.4f, transform.position.y, 0);
            }

            //SPEED POWER UP!!
            if (_isSpeedPowerupActive)
            {

                transform.Translate(direction * (_speed * 2) * Time.deltaTime);

            }
            else
            {
                transform.Translate(direction * _speed * Time.deltaTime);

            }
        }

        private void FireLaser()
        {
            _canFire = Time.time + _fireRate;


            if (_isTripleShotPowerupActive)
            {
                Instantiate(_tripleShotPref, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + _laserOffset, Quaternion.identity);
            }
        }

        public void Damage()
        {
            if (_isShieldPowerUpActive)
            {
                _isShieldPowerUpActive = false;
                _shieldPrefab.SetActive(false);
                return;
            }

            _lives--;
            Debug.Log("Amount of lives: " + _lives);

            if (_lives < 1)
            {
                _isAlive = false;
                GameManager.Instance.GameOver();
                Destroy(gameObject);
            }

            UI_Manager.Instance.GameOverText();
        }

        public bool PlayerStatus()
        {
            return _isAlive;
        }

        public void AcivateTripleShot()
        {
            _isTripleShotPowerupActive = true;
            StartCoroutine(TripleShotPowerupTimer());
        }

        public void ActivateSpeedPowerup()
        {
            _isSpeedPowerupActive = true;
            StartCoroutine(SpeedPowerupTimer());
        }

        public void ActivateShieldPowerup()
        {
            _isShieldPowerUpActive = true;
            _shieldPrefab.SetActive(true);
        }

        public void AddScore(int points)
        {
            _score += points;
        }

        private void ShowDamage()
        {
            switch (_lives)
            {
                case 0:
                    Debug.Log("PLAYER DESTROYED!");
                    break;
                case 1:
                    _damageCritical.SetActive(true);

                    break;
                case 2:
                    _damage.SetActive(true);
                    break;
            }

        }

        public int GetCurrentScore()
        {
            return _score;
        }

        public int GetPlayerLives()
        {
            return _lives;
        }

        IEnumerator TripleShotPowerupTimer()
        {
            yield return new WaitForSeconds(5f);
            _isTripleShotPowerupActive = false;
        }

        IEnumerator SpeedPowerupTimer()
        {
            yield return new WaitForSeconds(5f);
            _isSpeedPowerupActive = false;
        }

    }
}

