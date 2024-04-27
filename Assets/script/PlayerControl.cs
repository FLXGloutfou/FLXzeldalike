using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine.Tilemaps;
using System.Net.NetworkInformation;


public class PlayerControl : MonoBehaviour
{
    public int playerId = 0;
    private Player player;
    public bool useController;
    public int vitesse;

    public GameObject crossHair;

    public GameObject arrowPrefab;
    public int bulletLifetime;
    public float bulletSpeed;

    Vector3 movement;
    Vector3 aim;
    bool isAiming;
    bool endOfAiming;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        ProcessInputs();
        AimAndShoot();
        Move();
    }

    private void ProcessInputs()
    {
        if (useController)
        {
            movement = new Vector3(player.GetAxis("MoveHorizontal"), player.GetAxis("MoveVertical"), 0.0f);
            aim = new Vector3(player.GetAxis("AimHorizontal"), player.GetAxis("AimVertical"), 0.0f);
            aim.Normalize();
            isAiming = player.GetButton("Fire");
            endOfAiming = player.GetButtonUp("Fire");
        } else
        {
            movement = new Vector3(player.GetAxis("Horizontal"), player.GetAxis("vertical"), 0.0f);
            Vector3 mouseMovement = new Vector3(player.GetAxis("AimHorizontal"), player.GetAxis("AimVertical"), 0.0f);
            aim = aim + mouseMovement;
            if (aim.magnitude > 1.0f)
            {
                aim.Normalize();
            }
            isAiming = player.GetButton("FireM");
            endOfAiming = player.GetButtonUp("FireM");
        }
        if (movement.magnitude > 1.0f)
        {
            movement.Normalize();
        }
    }
    private void Move()
    {
        transform.position = transform.position + movement * Time.deltaTime * vitesse ;
    }

    private void AimAndShoot()
    {
        Vector2 shootingDirection = new Vector2(aim.x, aim.y);
        Debug.Log("Magnitude de aim : " + aim.magnitude);

        if (aim.magnitude > 0.0f)
        {
            aim.Normalize();
            crossHair.transform.localPosition = aim * 0.4f;
            crossHair.SetActive(true);

            shootingDirection.Normalize();
            if (endOfAiming)
            {
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * bulletSpeed;
                arrow.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                Destroy(arrow, bulletLifetime);
            }
        } else
        {
            crossHair.SetActive(false);
        }
    }
}
