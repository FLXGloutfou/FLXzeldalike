using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f; // Vitesse de d�placement de l'ennemi
    public int maxHealth = 100; // Sant� maximale de l'ennemi
    public GameObject dropItem; // Objet l�ch� par l'ennemi � sa mort
    public float attackDistance = 3f; // Distance � partir de laquelle l'ennemi attaque le joueur
    public int damage = 10; // D�g�ts inflig�s au joueur par l'ennemi

    private int currentHealth; // Sant� actuelle de l'ennemi
    private Transform player; // R�f�rence au transform du joueur

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // D�placement vers le joueur
        if (Vector3.Distance(transform.position, player.position) <= attackDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // V�rifier si l'ennemi entre en collision avec une balle du joueur
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Collision avec une balle d�tect�e");

            // Appliquer des d�g�ts � l'ennemi
            TakeDamage(other.GetComponent<Bullet>().damage);

            // D�truire la balle
            Destroy(other.gameObject);
        }
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // V�rifier si l'ennemi est mort
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        // L�cher l'objet
        Instantiate(dropItem, transform.position, Quaternion.identity);

        // D�truire l'ennemi
        Destroy(gameObject);
    }

}
