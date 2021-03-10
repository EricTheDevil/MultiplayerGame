using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioMixer audioMixer;
    public AudioSource clicksound;

    public void Start()
    {
        clicksound = GetComponent<AudioSource>();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame ()
    {
        
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("SetVolume", volume);
    }
    public void playsound()
    {
        clicksound.Play();
    }


}
