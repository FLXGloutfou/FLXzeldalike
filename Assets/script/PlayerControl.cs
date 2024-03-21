using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerControl : MonoBehaviour
{

    public float vitesseDeplacement = 5f;
    public float speed ;

    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f).normalized;


        transform.position = transform.position + movement * Time.deltaTime * vitesseDeplacement;
    }
}
