using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerControl : MonoBehaviour
{

    public float vitesseDeplacement = 5f;

    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);



        transform.position = transform.position + movement * Time.deltaTime * vitesseDeplacement;
    }
}
