using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerObj;
    [SerializeField] private Transform PlayerSpawnPoint;

    private void Start()
    {
        SpawnPlayer();
    }
    private void SpawnPlayer() 
    {
        PlayerObj.transform.position = PlayerSpawnPoint.position;
    }
}
