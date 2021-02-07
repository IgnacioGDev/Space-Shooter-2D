using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("Game Manager can't be NULL!!");
                }
                return _instance;
            }
        }

        [SerializeField]
        private bool _isGameOver;

        private void Awake()
        {
            _instance = this;
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            RestartGame();
            CloseApp();
        }

        private void RestartGame()
        {
            if (_isGameOver && Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(1);
            }
        }

        public void GameOver()
        {
            _isGameOver = true;
        }

        private void CloseApp()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}

