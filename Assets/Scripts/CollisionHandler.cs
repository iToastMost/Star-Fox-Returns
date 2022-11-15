using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadLevelDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;
    [SerializeField] AudioSource crashSFX;
   
        

    //bool isTransitioning = false;

    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    void StartSuccessSequence()
    {
        //isTransitioning = true;
    }
    
    void StartCrashSequence()
    {
        //isTransitioning = true;
        crashVFX.Play();
        crashSFX.Play();
        MeshRenderer[] renderer = GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer mesh in renderer)
        {
            mesh.enabled = false;
        }
        GetComponent<PlayerController>().enabled = false;
        BoxCollider[] boxColliders = GetComponentsInChildren<BoxCollider>();
        foreach(BoxCollider collider in boxColliders)
        {
            collider.enabled = false;
        }
        Invoke("ReloadLevel", loadLevelDelay);
    }

    void ReloadLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
    }
}
