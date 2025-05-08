using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    [SerializeField] GameObject getAwayText;
    [SerializeField] GameObject player;
    [SerializeField] GameObject win;
    [SerializeField] GameObject winScreen;

    bool isClose;
    public static bool hasWon = false;
    // Update is called once per frame
    void Update()
    {
        Win();
    }

    public void CloseSetter()
    {
        win.SetActive(true);
        Debug.Log("Close Setting");
        // prevent player from seeing the prompt to get away if already won
        if (hasWon)
        {
            return;
        }

        if (Vector3.Distance(player.transform.position, transform.position) < 5f)
        {
            getAwayText.SetActive(true);
            isClose = true;
        }
        else
        {
            getAwayText.SetActive(false);
            isClose = false;
        }
    }

    void Win()
    {
        if(Input.GetKeyDown(KeyCode.E) && isClose)
        {
            isClose = false;
            hasWon = true;
            winScreen.SetActive(true);
            getAwayText.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
