using UnityEngine;

public class PlayerStepsSound : MonoBehaviour
{
    public float stepInterval = 0.5f; // время между шагами
    public float minSpeed = 0.1f;     // минимальная скорость для воспроизведения шагов
    public LayerMask groundMask;

    public AudioSource audioSource;
    public Transform GroundCheckPosition;

    [System.Serializable]
    public class SurfaceSound
    {
        public string surfaceTag;
        public AudioClip[] stepSounds;
    }

    public SurfaceSound[] surfaceSounds;

    private Rigidbody rb;
    private float stepTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (horizontalVelocity.magnitude > minSpeed)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                PlayStepSound();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    void PlayStepSound()
    {
        Ray ray = new Ray(GroundCheckPosition.position + Vector3.up * 0.1f, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f, groundMask))
        {
            string surfaceTag = hit.collider.tag;


            foreach (var surface in surfaceSounds)
            {
                if (surface.surfaceTag == surfaceTag && surface.stepSounds.Length > 0)
                {
                    AudioClip clip = surface.stepSounds[Random.Range(0, surface.stepSounds.Length)];
                    audioSource.PlayOneShot(clip);
                    return;
                }
            }
        }
    }
}
