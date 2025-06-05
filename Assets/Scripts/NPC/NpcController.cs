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
    //private bool isWatchingPlayer = false;

    private Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //if (isWatchingPlayer)
        //{
        //    transform.LookAt(player);
        //}

        if (isFollowing || isReturning) return;

        if (isCoffeOrdered)
        {
            //float currentY = transform.eulerAngles.y;
            //float newY = Mathf.MoveTowardsAngle(currentY, 35f, 180f * Time.deltaTime);

            //transform.rotation = Quaternion.Euler(0f, newY, 0f);

            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0f; // Убираем наклон вверх/вниз, только по горизонтали

            if (direction.magnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2f * Time.deltaTime);
            }

            return;
        }


        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= triggerDistance)
        {
            StartCoroutine(FollowThenReturn());
        }
    }

    private IEnumerator FollowThenReturn()
    {
        isFollowing = true;
        animator.SetBool("isWalking", true);

        while (Vector3.Distance(transform.position, player.position) > 2f)
        {
            agent.SetDestination(player.position);
            yield return null;
        }

        agent.ResetPath();
        animator.SetBool("isWalking", false);

        Debug.Log("NPC Talking!");

        yield return new WaitForSeconds(waitTime);

        isCoffeOrdered = true;
        isFollowing = false;
        isReturning = true;
        animator.SetBool("isWalking", true);

        agent.SetDestination(startPosition.position);

        while (Vector3.Distance(transform.position, startPosition.position) > 0.5f)
        {
            yield return null;
        }

        isReturning = false;
        animator.SetBool("isWalking", false);
        //isWatchingPlayer = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CoffeeStepManager.Instance.IsCurrentStep(CoffeeStepManager.Step.ServeToNPC) && other.CompareTag("CoffeeCup")) 
        {
            if (other.GetComponent<CoffeeCup>().IsCoffeeReady()) 
            {
                Debug.Log("Cutscene and screamer!");
            }
            else
            {
                Debug.Log("Empty coffee!");
            }
        }
    }
}
