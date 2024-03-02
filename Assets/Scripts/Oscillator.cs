using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;

    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        const float tau = Mathf.PI * 2; // continually growing over time
        if(period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; // const val 6.....
        float rawSinWave = Mathf.Sin(cycles * tau); // 1-> -1 val
        movementFactor = (rawSinWave + 1) / 2;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
