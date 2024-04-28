using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f; // Vitesse de déplacement de l'ennemi
    public int maxHealth = 100; // Santé maximale de l'ennemi
    public GameObject dropItem; // Objet lâché par l'ennemi à sa mort
    public float attackDistance = 3f; // Distance à partir de laquelle l'ennemi attaque le joueur
    public int damage = 10; // Dégâts infligés au joueur par l'ennemi

    private int currentHealth; // Santé actuelle de l'ennemi
    private Transform player; // Référence au transform du joueur

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Déplacement vers le joueur
        if (Vector3.Distance(transform.position, player.position) <= attackDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifier si l'ennemi entre en collision avec une balle du joueur
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Collision avec une balle détectée");

            // Appliquer des dégâts à l'ennemi
            TakeDamage(other.GetComponent<Bullet>().damage);

            // Détruire la balle
            Destroy(other.gameObject);
        }
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Vérifier si l'ennemi est mort
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        // Lâcher l'objet
        Instantiate(dropItem, transform.position, Quaternion.identity);

        // Détruire l'ennemi
        Destroy(gameObject);
    }

}
