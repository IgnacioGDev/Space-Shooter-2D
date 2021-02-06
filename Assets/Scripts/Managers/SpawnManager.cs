using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts;

namespace Scripts.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        //Singleton
        private static SpawnManager _instance;
        public static SpawnManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.Log("SpawnManager can't be null!!");
                }
                return _instance;
            }
        }

        //*******************ENEMIES************************
        [SerializeField]
        private GameObject _enemyPrefab;
        [SerializeField]
        private GameObject _enemyContainer;
        [SerializeField]
        private float _randomValue = 9f;

        //******************POWERUPS************************
        [SerializeField]
        private GameObject[] _powerUps;

        private void Awake()
        {
            _instance = this;
        }


        // Start is called before the first frame update
        void Start()
        {
            
        }

        public void StartSpawning()
        {
            StartCoroutine(SpawnEnemy());
            StartCoroutine(SpawnPowerUp());
        }

        // Update is called once per frame
        void Update()
        {

        }

        private IEnumerator SpawnEnemy()
        {
            while (Player.Instance.PlayerStatus())
            {
                Vector3 posToSpawn = new Vector3(Random.Range(-_randomValue, _randomValue), 10, 0);
                GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);

                //Spawns enemies into the enemy container object in the hierarchy.
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(2f);
            }

        }

        private IEnumerator SpawnPowerUp()
        {
            while (Player.Instance.PlayerStatus())
            {
                int randomIndex = Mathf.RoundToInt(Random.Range(0, 3));
                yield return new WaitForSeconds(Random.Range(3,5));
                Vector3 postoSpawn = new Vector3(Random.Range(-_randomValue, _randomValue), 10, 0);
                GameObject powerUp = Instantiate(_powerUps[randomIndex], postoSpawn, Quaternion.identity);
                powerUp.transform.parent = gameObject.transform;

            }
        }
    }
}

