using UnityEngine;

public class QuitApplication : MonoBehaviour
{


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed, quitting application.");
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop playing in the editor
#endif
        }
        // start a new game if the R key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R key pressed, restarting the game.");
            // Assuming you want to reload the first scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

}
