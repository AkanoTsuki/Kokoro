using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public float timer = 1f;
    // Use this for initialization
    void Start()
    {
        Screen.SetResolution(1366, 768, false);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            // Application.LoadLevel(1);
            SceneManager.LoadScene("A1_StartMenu");
        }
    }
}
