using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pasarAlNivel1()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void pasarAlNivel2()
    {
        SceneManager.LoadScene("Nivel2");
    }

    public void Créditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
