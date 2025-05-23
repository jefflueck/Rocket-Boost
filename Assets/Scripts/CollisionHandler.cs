using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;






public class CollisionHandler : MonoBehaviour
{
    [SerializeField]
    private float sceneLoadDelay = 2f;
    [SerializeField]
    private AudioClip crashSFX;
    [SerializeField]
    private AudioClip finishSFX;
    [SerializeField]
    ParticleSystem finishParticles;
    [SerializeField]
    ParticleSystem crashParticles;


    AudioSource audioSource;
    [SerializeField]
    private bool isControllable = true;
    [SerializeField]
    private bool isCollidable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("L key pressed");
            LoadNextScene();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C key pressed");
            isCollidable = !isCollidable;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isControllable || !isCollidable) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartFinishSequence();

                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartFinishSequence()
    {
        isControllable = false;
        audioSource.Stop();
        finishParticles.Play();
        audioSource.PlayOneShot(finishSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextScene", sceneLoadDelay);

    }

    private void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", sceneLoadDelay);
    }

    private void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // Loop back to the first scene if at the end
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}

