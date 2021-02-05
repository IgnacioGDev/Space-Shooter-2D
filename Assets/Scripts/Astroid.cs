using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Astroid : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Rotation();
        }

        private void Rotation()
        {
            transform.Rotate(0f, 0f, 0.5f, Space.Self);
        }
    }
}

