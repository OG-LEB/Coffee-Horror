using UnityEngine;

public class NpcFootsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepClips;
    public float stepInterval = 0.5f;

    private float stepTimer = 0f;
    private bool isWalking = false;

    void Update()
    {
        if (!isWalking || footstepClips.Length == 0) return;

        stepTimer += Time.deltaTime;

        if (stepTimer >= stepInterval)
        {
            PlayFootstep();
            stepTimer = 0f;
        }
    }

    void PlayFootstep()
    {
        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
        audioSource.PlayOneShot(clip);
    }

    public void StartFootsteps()
    {
        isWalking = true;
        stepTimer = 0f; 
    }

    public void StopFootsteps()
    {
        isWalking = false;
    }
}
