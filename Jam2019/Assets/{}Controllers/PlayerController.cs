using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent NavAgent;
    private GameObject CurrentPointer;
    private Rigidbody rb;

    public LayerMask Clickable;
    public GameObject PointerPrefab;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        NavAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Move();
        }

        if (CurrentPointer != null)
        {
            if (rb.position.x == CurrentPointer.transform.position.x &&
                rb.position.z == CurrentPointer.transform.position.z)
            {
                Destroy(CurrentPointer);
            }
        }
    }

    private void Move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100, Clickable))
        {
            if (CurrentPointer != null)
            {
                Destroy(CurrentPointer);
            }
            Vector3 clickPoint = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);

            CurrentPointer = Instantiate(PointerPrefab, clickPoint, Quaternion.identity);
            NavAgent.SetDestination(hitInfo.point);
        }
    }
}
