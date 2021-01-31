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


        [SerializeField]
        private GameObject _enemyPrefab;
        [SerializeField]
        private GameObject _enemyContainer;
        [SerializeField]
        private float _randomValue = 9f;

        private void Awake()
        {
            _instance = this;
        }


        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(SpawnEnemy());
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
    }
}

