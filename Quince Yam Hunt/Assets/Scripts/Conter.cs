using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Conter : MonoBehaviour
{
    private static float puntos1 = 0;
    private static float puntos2 = 0;
    private static TextMeshProUGUI contrillos;
    private static TextMeshProUGUI contatas;

    public Button avanzar;
    public Button avanzar2;   

    void Start()
    {
        GameObject t1 = GameObject.FindGameObjectWithTag("contrillos");
        contrillos = t1.GetComponent<TextMeshProUGUI>();
       
        GameObject t2 = GameObject.FindGameObjectWithTag("contatas");
        contatas = t2.GetComponent<TextMeshProUGUI>();
    }
   
    void Update()
    {
        Avanzar();
        Avanzar2();
    }

    public static void IncrementarC1(Collider c)
    {
        puntos1++;
        contatas.text = puntos1.ToString();
        Destroy(c.gameObject.transform.parent.gameObject);        
    }

    public static void IncrementarC2(Collider c)
    {
        puntos2++;
        contrillos.text = puntos2.ToString();
        Destroy(c.gameObject.transform.parent.gameObject);        
    }

    private void Avanzar()
    {
        if (puntos1 == 5 && puntos2 == 4)
        {
            avanzar.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            puntos1 = 0;
            puntos2 = 0;
        }
    }

    private void Avanzar2()
    {
        if (puntos1 == 10 && puntos2 == 8)
        {
            avanzar2.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}


