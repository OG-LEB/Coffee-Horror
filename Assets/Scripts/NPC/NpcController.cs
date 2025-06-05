using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public Transform player;
    public float triggerDistance = 5f;
    public float waitTime = 2f;
    public float speed;

    private NavMeshAgent agent;
    [SerializeField] private Transform startPosition;
    private bool isReturning = false;
    private bool isFollowing = false;
    private bool isCoffeOrdered = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    private void Update()
    {
        if (isFollowing || isReturning) return;

        if (isCoffeOrdered)
        {
            float currentY = transform.eulerAngles.y;
            float newY = Mathf.MoveTowardsAngle(currentY, 35f, 180f * Time.deltaTime);

            transform.rotation = Quaternion.Euler(0f, newY, 0f);
            return;
        }


        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= triggerDistance)
        {
            Debug.Log("Start follow!");
            StartCoroutine(FollowThenReturn());
        }
    }

    private IEnumerator FollowThenReturn()
    {
        isFollowing = true;

        while (Vector3.Distance(transform.position, player.position) > 2f)
        {
            agent.SetDestination(player.position);
            yield return null;
        }

        agent.ResetPath();
        Debug.Log("NPC Talking!");

        yield return new WaitForSeconds(waitTime);

        isCoffeOrdered = true;
        isFollowing = false;
        isReturning = true;

        agent.SetDestination(startPosition.position);

        while (Vector3.Distance(transform.position, startPosition.position) > 0.5f)
        {
            yield return null;
        }

        isReturning = false;
    }
}
