using UnityEngine;

public class Helico : MonoBehaviour
{
    public GameObject player;
    public Transform playerExitPoint;
    public Transform playerSitPoint;
    public GameObject vehicle;
    public GameObject playerModel;
    public GameObject pet;
    public float vehicleMoveSpeed ;
    public float interactionDistance;

    private bool isPlayerInside = false;

    void Update()
    {
        if (Vector3.Distance(player.transform.position, vehicle.transform.position) <= interactionDistance && Input.GetKeyDown(KeyCode.E))
        {     
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isPlayerInside)
                    EnterVehicle();
                else
                    ExitVehicle();
            }

            if (isPlayerInside)
            {
                // Contrôles du véhicule
                Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f).normalized;
                vehicle.transform.position += movement * Time.deltaTime * vehicleMoveSpeed;
            }
        }
    }

    void EnterVehicle()
    {
        pet.SetActive(false);
        playerModel.SetActive(false);
        player.transform.position = playerSitPoint.position;
        player.transform.parent = vehicle.transform;
        isPlayerInside = true;
    }

    void ExitVehicle()
    {
        pet.SetActive(true);
        playerModel.SetActive(true); 
        player.transform.parent = null;
        player.transform.position = playerExitPoint.position;
        isPlayerInside = false;
    }
}
