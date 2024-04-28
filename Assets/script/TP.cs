using UnityEngine;
using UnityEngine.SceneManagement;

public class TP : MonoBehaviour
{
    public string sceneToLoad; // Nom de la scène à charger


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifie si le joueur entre en collision avec cet objet
        if (other.CompareTag("Player"))
        {
            Debug.Log("le joueur se tp");

            // Charge la scène spécifiée
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
