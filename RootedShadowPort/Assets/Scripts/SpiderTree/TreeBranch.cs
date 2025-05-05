using UnityEngine;

public class TreeBranch : MonoBehaviour
{
    [SerializeField] SpiderTree spiderTree;
    [SerializeField] private AudioClip branchSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.spatialBlend = 1f;
        audioSource.minDistance = 1f;
        audioSource.maxDistance = 25f;
        audioSource.clip = branchSound;
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spiderTree.AlertTo(transform.position);
            if (branchSound != null)
            {
                audioSource.PlayOneShot(branchSound);
            }
        }
    }
}
