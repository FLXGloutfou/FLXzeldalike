using UnityEngine;
using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerControl : MonoBehaviour
{
    public GameObject settingsPanel; // Référence au panneau des paramètres dans l'interface utilisateur
    private Player player; // Référence au joueur Rewired

    public bool useController = true; // Paramètre pour contrôler l'utilisation du contrôleur

    public int vitesse;

    public GameObject crossHair;

    public GameObject arrowPrefab;
    public int bulletLifetime;
    public float bulletSpeed;

    Vector3 movement;
    Vector3 aim;
    bool isAiming;
    bool endOfAiming;
    bool isAimActive; // Indicateur pour indiquer si le joueur est en train de viser


    public int health = 4;
    public int maxHealth = 4;
    public float invulnerabilityDuration = 1f; // Durée d'invulnérabilité en secondes
    public int enemyDamageAmount = 1;

    public Image healthBar;
    public Sprite healthbar_0;
    public Sprite healthbar_1;
    public Sprite healthbar_2;
    public Sprite healthbar_3;
    public Sprite healthbar_4;

    public Transform respawnPoint;

    private bool isInvulnerable = false; // Indique si le joueur est actuellement invulnérable


    //START//
    void Start()
    {
        player = ReInput.players.GetPlayer(0); // Récupère le joueur 0 (ou tout autre joueur que tu utilises)

        respawnPoint = GameObject.Find("RespawnPoint").transform;

        DontDestroyOnLoad(gameObject);      
    }




    //UPDATE//

    void Update()
    {
        ProcessInputs();
        Move();

        // Vérifie si l'input "setting" est détecté et inverse l'état du panneau des paramètres
        if (player.GetButtonDown("Setting"))
        {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }

        // Si le panneau des paramètres est actif, ne pas exécuter les autres actions
        if (settingsPanel.activeSelf)
            return;

        crossHair.SetActive(isAimActive);

        if (isAimActive)
        {
            AimAndShoot();
        }
    }


    //PROCESSINPUT//

    void ProcessInputs()
    {
        if (useController)
        {
            movement = new Vector3(player.GetAxis("MoveHorizontal"), player.GetAxis("MoveVertical"), 0.0f);
            aim = new Vector3(player.GetAxis("AimHorizontal"), player.GetAxis("AimVertical"), 0.0f);
            aim.Normalize();
            isAimActive = player.GetButton("Aim");
            isAiming = player.GetButton("Fire") && isAimActive; // Le tir n'est possible que lorsque le joueur vise
            endOfAiming = player.GetButtonUp("Fire");
        }
        else
        {
            movement = new Vector3(player.GetAxis("Horizontal"), player.GetAxis("Vertical"), 0.0f);
            Vector3 mouseMovement = new Vector3(player.GetAxis("AimHorizontal"), player.GetAxis("AimVertical"), 0.0f);
            aim = aim + mouseMovement;
            if (aim.magnitude > 1.0f)
            {
                aim.Normalize();
            }
            isAimActive = player.GetButton("Aim");
            isAiming = player.GetButton("FireM") && isAimActive; // Le tir n'est possible que lorsque le joueur vise
            endOfAiming = player.GetButtonUp("FireM");
        }
        if (movement.magnitude > 1.0f)
        {
            movement.Normalize();
        }
    }


    //MOVE//
    void Move()
    {
        transform.position = transform.position + movement * Time.deltaTime * vitesse;
    }

    //AIMANDSHOOT//
    void AimAndShoot()
    {
        if (isAimActive && aim.magnitude > 0.0f)
        {
            Vector2 shootingDirection = new Vector2(aim.x, aim.y);
            aim.Normalize();
            crossHair.transform.localPosition = aim * 2f;
            crossHair.SetActive(true);

            shootingDirection.Normalize();
            if (endOfAiming)
            {
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * bulletSpeed;
                arrow.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                Destroy(arrow, bulletLifetime);
            }
        }
        else
        {
            crossHair.SetActive(false);
        }
    }


    //COLLSIONENEMY//
    void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifier si le joueur entre en collision avec un ennemi
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Collision avec un ennemi détectée");

            // Infliger des dégâts au joueur en utilisant la fonction TakeDamage existante
            TakeDamage(enemyDamageAmount); // Utiliser enemyDamageAmount comme montant de dégâts
        }
    }


    //TAKE DAMAGE//
    public void TakeDamage(int damageAmount)
    {
        if (!isInvulnerable)
        {
            health -= damageAmount;

            if (health <= 0)
            {
                Die();
            }
            else if (health == 4)
            {
                Debug.Log("Le personnage a 4hp");
                healthBar.sprite = healthbar_4;
            }
            else if (health == 3)
            {
                Debug.Log("Le personnage a 3hp");
                healthBar.sprite = healthbar_3;
            }
            else if (health == 2)
            {
                Debug.Log("Le personnage a 2hp");
                healthBar.sprite = healthbar_2;
            }
            else if (health == 1)
            {
                Debug.Log("Le personnage a 1hp");
                healthBar.sprite = healthbar_1;
            }

            // Activer la période d'invulnérabilité
            StartCoroutine(InvulnerabilityPeriod());
        }
    }

    IEnumerator InvulnerabilityPeriod()
    {
        isInvulnerable = true;

        // Attendre la durée d'invulnérabilité
        yield return new WaitForSeconds(invulnerabilityDuration);

        isInvulnerable = false;
    }

    //MORT//
    void Die()
    {
        healthBar.sprite = healthbar_0;
        Debug.Log("Le personnage est mort !");

        // Réinitialiser la vie
        health = maxHealth;

        // Placer le joueur à la position de respawn
        transform.position = respawnPoint.position;

        healthBar.sprite = healthbar_4;
    }
}
