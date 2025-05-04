using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI getAwayText;
    [SerializeField] Canvas winScreen;
    [SerializeField] GameObject player;

    bool isClose;
    bool hasWon;
    // Update is called once per frame
    void Update()
    {
        Win();
    }

    public void CloseSetter()
    {
        // prevent player from seeing the prompt to get away if already won
        if (hasWon)
        {
            return;
        }
        
        if (Vector3.Distance(player.transform.position, transform.position) < 5f)
        {
            getAwayText.gameObject.SetActive(true);
            isClose = true;
        }
        else
        {
            getAwayText.gameObject.SetActive(false);
            isClose = false;
        }
    }

    void Win()
    {
        if(Input.GetKeyDown(KeyCode.E) && isClose)
        {
            isClose = false;
            hasWon = true;
            winScreen.gameObject.SetActive(true);
        }
    }
}
