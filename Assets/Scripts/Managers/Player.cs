using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private Vector3 _laserOffset = new Vector3(0, 0.8f, 0);
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;

    //STATS
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private bool _isAlive = true;

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
    }

    private void CalculateMovement()
    {
        //Gets the values of X and Y axis into horizontalInput and verticalInpit respectively
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //New vector 3 is created to capture the horizontal and vertical axes for use them later in transform.translate.
        //This variable has already the speed and Time.deltaTime values implemented.
        Vector3 direction = new Vector3(horizontalInput, verticalInput) * _speed * Time.deltaTime;
        transform.Translate(direction);

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
    }

    private void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + _laserOffset, Quaternion.identity);
    }

    public void Damage()
    {
        _lives--;
        Debug.Log("Amount of lives: " + _lives);

        if (_lives < 1)
        {
            _isAlive = false;
            Destroy(gameObject);
        }
    }

    public bool PlayerStatus()
    {
        return _isAlive;
    }

}
