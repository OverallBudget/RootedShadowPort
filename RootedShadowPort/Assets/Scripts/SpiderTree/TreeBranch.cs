using UnityEngine;

public class TreeBranch : MonoBehaviour
{
    [SerializeField] SpiderTree spiderTree;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[TreeBranch] Triggered by: {other.name}");
            spiderTree.AlertTo(transform.position);
        }
    }
}
