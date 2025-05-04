using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> dirtSteps;
    public List<AudioClip> woodSteps;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (dirtSteps != null)
        {
            StartCoroutine(PlayDirtStepSound());
        }

        if (woodSteps != null)
        {
            StartCoroutine(PlayWoodStepSound());
        }


    }
    private void OnTriggerExit(Collider other)
    {
        StopAllCoroutines();

    }

    public IEnumerator PlayDirtStepSound()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var ctrl = player.GetComponent<PlayerController>();
        float delay = 0.5f;

        while (true)                   // keep this coroutine alive forever
        {
            // 1) wait here until the player starts moving
            yield return new WaitUntil(() => ctrl.isMoving);

            // 2) as soon as we’re moving, cycle through the clips…
            if (dirtSteps != null)
            {
                for (int i = 0; i < dirtSteps.Count; i++)
                {
                    // if we stopped mid-cycle, break out to outer while and wait again
                    if (!ctrl.isMoving)
                        break;

                    AudioSource.PlayClipAtPoint(dirtSteps[i], player.transform.position);
                    yield return new WaitForSeconds(delay);
                }
            }
            // loop back: if still moving, it'll play the list again;
            // if stopped, it'll hit the WaitUntil above
        }
    }

    public IEnumerator PlayWoodStepSound()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var ctrl = player.GetComponent<PlayerController>();
        float delay = 0.5f;
        while (true)                   
        {
            
            yield return new WaitUntil(() => ctrl.isMoving);
            
            if (woodSteps != null)
            {
                for (int i = 0; i < woodSteps.Count; i++)
                {
                    
                    if (!ctrl.isMoving)
                        break;
                    AudioSource.PlayClipAtPoint(woodSteps[i], player.transform.position);
                    Debug.Log("Playing wood step sound");
                    yield return new WaitForSeconds(delay);
                }
            }
            
        }
    }


}
