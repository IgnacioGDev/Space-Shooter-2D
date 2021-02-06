using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts;

namespace Scripts.Managers
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;
        public static AudioManager Instance
        {
            get 
            {
                if (_instance == null)
                {
                    Debug.LogError("AudioManager can't be null!!");
                }
                return _instance;
            }
        }

        //**********AUDIO CLIPS*************
        private AudioSource _aSource;
        [SerializeField]
        private AudioClip _explosion;
        [SerializeField]
        private AudioClip _powerup;
        [SerializeField]
        private AudioClip _laser;


        private void Awake()
        {
            _instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            _aSource = GetComponent<AudioSource>();
        }

        public void PlayExplosionSound()
        {
            _aSource.PlayOneShot(_explosion);
        }

        public void PlayPowerUpSound()
        {
            _aSource.PlayOneShot(_powerup); 
        }

        public void PlayLaserSound()
        {
            _aSource.PlayOneShot(_laser);
        }
    }
}

