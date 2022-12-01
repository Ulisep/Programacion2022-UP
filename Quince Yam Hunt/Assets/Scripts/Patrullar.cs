using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class Patrullar : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento; // Seralizar una variable es similar a colocarla como p�blica pero esta no se podr� modificar desde otra clase
    [SerializeField] private Transform[] puntosMovimentos;
    [SerializeField] private float distanciaMinima;
    private int numeroAleatorio;
    

    private void Start()
    {
        numeroAleatorio = Random.Range(0, puntosMovimentos.Length); // un n�mero aleatorio dependiendo de la cantidad de puntos que haiga
        
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, puntosMovimentos[numeroAleatorio].position, velocidadMovimiento * Time.deltaTime); // mover entre los puntos seg�n el n�mero aleatorio de una manera uniforme

        if (Vector3.Distance(transform.position, puntosMovimentos[numeroAleatorio].position) < distanciaMinima) // cuando est� llegando al punto cambiar� la direcci�n es decir el n�mero aleatorio
        {
            numeroAleatorio = Random.Range(0, puntosMovimentos.Length);
            
        }
      
    }

}
