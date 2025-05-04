using UnityEngine;

public class TreeBranch : MonoBehaviour
{
    [SerializeField] SpiderTree spiderTree;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spiderTree.AlertTo(transform.position);
        }
    }
}
