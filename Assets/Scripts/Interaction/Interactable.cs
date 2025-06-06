using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string promptText;
    public bool CanDrag = true;
    public abstract void Interact();

    public virtual void SetOutline(bool state)
    {
        var outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = state;
        }

    }
}
