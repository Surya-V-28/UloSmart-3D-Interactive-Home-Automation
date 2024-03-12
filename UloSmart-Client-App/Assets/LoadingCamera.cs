//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Cinemachine;

//public class LoadingCamera : MonoBehaviour
//{
//    private void Start()
//    {
//        camera = GetComponentInChildren<CinemachineVirtualCamera>().gameObject;
//    }

//    private void LateUpdate()
//    {        
//        float deltaTimeMultiplier = 1.0f;
//        Vector3 rotationAmount = new Vector3(-input.y, input.x) * rotationSpeed * deltaTimeMultiplier;

//        camera.transform.eulerAngles += new Vector3(rotationAmount.x, 0.0f, 0.0f);
//        transform.eulerAngles += new Vector3(0.0f, rotationAmount.y, rotationAmount.z);
        
//        transform.localEulerAngles = new Vector3(Mathf.Clamp(transform.localEulerAngles.x, -maxRotationAngle, maxRotationAngle), transform.localEulerAngles.y, transform.localEulerAngles.z);
//    }


//    [SerializeField]
//    private float rotationSpeed = 30.0f;

//    [SerializeField]
//    private float maxRotationAngle = 30.0f;

//    private new GameObject camera = null;
//}