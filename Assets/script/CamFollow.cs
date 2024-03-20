using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target; // Référence au joueur que la caméra va suivre

    void LateUpdate()
    {
        if (target != null)
        {
            // Déplacer la caméra pour qu'elle soit centrée sur le joueur
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }
}
