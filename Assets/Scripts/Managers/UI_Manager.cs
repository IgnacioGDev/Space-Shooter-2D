using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Managers
{
    public class UI_Manager : MonoBehaviour
    {
        private static UI_Manager _instance;
        public static UI_Manager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("UI_Manager can't be NULL!!");
                }
                return _instance;
            }
        }


        [SerializeField]
        private Text _scoreText;
        [SerializeField]
        private Sprite[] _livesSprites;
        [SerializeField]
        private Image _livesUI;

        [SerializeField]
        private Text _gameOverText;
        [SerializeField]
        private Text _restartInstructions;

        public float timer = 3;

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
            _scoreText.text = "Score: " + Player.Instance.GetCurrentScore().ToString();
            PlayerLives();
        }

        void PlayerLives()
        {
            _livesUI.sprite = _livesSprites[Player.Instance.GetPlayerLives()];
        }

        public void GameOverText()
        {
            if (!Player.Instance.PlayerStatus())
            {
                _gameOverText.gameObject.SetActive(true);
                StartCoroutine(GameOverTextFlicker());

            }
        }

        IEnumerator GameOverTextFlicker()
        {
            _restartInstructions.gameObject.SetActive(true);
            while (timer > 0)
            {
                yield return new WaitForSeconds(0.5f);
                _gameOverText.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                _gameOverText.gameObject.SetActive(true);

                timer -= 1;
            }

        }
    }
}

