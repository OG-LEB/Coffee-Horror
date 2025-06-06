using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public float interactDistance = 3f;
    public LayerMask interactMask;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, interactDistance, interactMask))
        {
            Debug.Log($"{hit.transform.name}");

            if (Input.GetKeyDown(KeyCode.E))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }
}
