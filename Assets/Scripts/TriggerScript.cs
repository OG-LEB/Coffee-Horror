using UnityEngine;
using UnityEngine.Events;

public class TriggerScript : MonoBehaviour
{
    public UnityEvent action;
    public bool DestroyAfter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            action.Invoke();
            if (DestroyAfter)
                Destroy(gameObject);
        }
    }
}
