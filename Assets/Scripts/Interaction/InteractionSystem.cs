using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public float interactDistance = 3f;
    public LayerMask interactMask;
    private Camera cam;

    private Interactable lastHovered;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, interactDistance, interactMask))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                // �������� ��������� ������ �������
                //if (lastHovered != interactable)
                //{
                    //if (lastHovered != null)
                    //    lastHovered.SetOutline(false);

                    interactable.SetOutline(interactable.CanDrag);
                    lastHovered = interactable;
                //}

                // ��������������
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
        }
        else
        {
            // ��������� �������, ���� �� �� ��� �� ������
            if (lastHovered != null)
            {
                lastHovered.SetOutline(false);
                lastHovered = null;
            }
        }
    }
}
