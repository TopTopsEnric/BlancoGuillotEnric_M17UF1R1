using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static GameObject Pause_Manager;
    public GameObject menuPausa;
    public bool isPaused = false;
    private void Awake()
    {

        if (Pause_Manager != null && Pause_Manager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
                DontDestroyOnLoad(this.gameObject); // No destruir este objeto al cargar una nueva escena
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            menuPausa.SetActive(isPaused);

            if (isPaused)
            {
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    void OnDisable()
    {
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
