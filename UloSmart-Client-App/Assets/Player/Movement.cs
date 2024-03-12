using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSystem
{
    public class Movement : MonoBehaviour
    {
        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Vector3 movementAmount = new Vector3(input.x, 0.0f, input.y);
            Vector3 offset = transform.rotation * movementAmount * movementSpeed * Time.deltaTime;
            rigidbody.MovePosition(transform.position + offset);

            input = Vector2.zero;
        }


        public void AddInput(Vector2 value)
        {
            input += value;
            input = new Vector2(Mathf.Clamp(input.x, -1.0f, 1.0f), Mathf.Clamp(input.y, -1.0f, 1.0f));
        }


        private Vector2 input = Vector2.zero;

        [SerializeField]
        private float movementSpeed = 5.0f;

        private new Rigidbody rigidbody;
    }
}