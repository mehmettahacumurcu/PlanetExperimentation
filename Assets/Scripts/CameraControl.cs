using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float cameraSpeed = 5f;
    [SerializeField] float thresholdX = 1f;
    [SerializeField] float cameraThresholdX = 25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.x > thresholdX && this.transform.position.x < cameraThresholdX)
        {
            Vector3 newPosition = transform.position + Vector3.right * cameraSpeed * Time.deltaTime;
            transform.position = newPosition;
        }
    }
}
