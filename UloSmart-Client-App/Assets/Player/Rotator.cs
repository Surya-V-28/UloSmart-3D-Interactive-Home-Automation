using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace PlayerSystem
{
    public class Rotator : MonoBehaviour
    {
        private void Start()
        {
            camera = GetComponentInChildren<CinemachineVirtualCamera>().gameObject;
        }

        private void LateUpdate()
        {
            Vector3 rotationAmount = new Vector3(-input.y, input.x) * rotationSpeed * 0.5f;

            camera.transform.eulerAngles += new Vector3(rotationAmount.x, 0.0f, 0.0f);
            transform.eulerAngles += new Vector3(0.0f, rotationAmount.y, rotationAmount.z);
            input = Vector2.zero;
        }

        public void AddInput(Vector2 value)
        {
            input += value;
        }


        private Vector2 input = Vector2.zero;

        [SerializeField]
        private float rotationSpeed = 30.0f;
        
        private new GameObject camera = null;
    }
}
