using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string promptText;
    public abstract void Interact();
}
