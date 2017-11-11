using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour {
    private Canvas canvas;
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unPaused;
    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.enabled = !canvas.enabled;
            Pause();
        }
	}

    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        LowPass();
        //if(Time.timeScale == 0)
        //{
        //    Time.timeScale = 1;
        //}
        //else
        //{
        //    Time.timeScale = 0;
        //}
    }

    private void LowPass()
    {
        if (Time.timeScale == 0)
        {
            paused.TransitionTo(0.01f);
        }
        else
        {
            unPaused.TransitionTo(0.01f);
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
