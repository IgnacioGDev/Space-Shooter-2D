﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Scripts.Managers
{
    public class MainManu : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadGame()
        {
            SceneManager.LoadScene(1);
        }
    }

}
