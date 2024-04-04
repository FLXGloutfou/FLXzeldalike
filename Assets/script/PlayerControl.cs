using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rewired;


public class PlayerControl : MonoBehaviour
{
    public int playerId = 0;
    public float vitesseDeplacement = 5f;
    public GameObject crossHair;
    private Player player;

    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
    }

    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f).normalized;

        MoveCrossHair();

        transform.position = transform.position + movement * Time.deltaTime * vitesseDeplacement;
    }

    private void MoveCrossHair()
    {
        Vector3 aim = new Vector3(player.GetAxis("AimHorizontal"), player.GetAxis("AimVertical"), 0.0f);

        if (crossHair == null)
        {
            Debug.LogError("Crosshair GameObject is not assigned!");
            return;
        }

        if (aim.magnitude > 0.0f)
        {
            aim.Normalize();
            crossHair.transform.localPosition = aim;
        }
    }
}
