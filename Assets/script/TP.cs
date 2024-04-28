using UnityEngine;
using UnityEngine.SceneManagement;

public class TP : MonoBehaviour
{
    public string sceneToLoad; // Nom de la sc�ne � charger


    private void OnTriggerEnter2D(Collider2D other)
    {
        // V�rifie si le joueur entre en collision avec cet objet
        if (other.CompareTag("Player"))
        {
            Debug.Log("le joueur se tp");

            // Charge la sc�ne sp�cifi�e
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
