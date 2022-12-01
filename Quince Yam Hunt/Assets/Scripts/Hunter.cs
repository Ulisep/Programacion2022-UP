//using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hunter : MonoBehaviour
{
    private Rigidbody rb;
    private float walkSpeed = 3f;
    private float runSpeed = 6f;
    public Vector2 sensibility;
    private bool HActive;
    private bool VActive;

    public AudioSource walk;
    public AudioSource shoot;
    public AudioSource jump;

    private Transform camara;

    public float jumpSpeed;
    private float distanceToGround;

    public LayerMask whatCanShoot;

    public Transform firePoint;
    public Transform bullet;
    public GameObject bulletPrefab;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloquear mouse 
        rb = GetComponent<Rigidbody>();
        camara = transform.Find("Camera");
        distanceToGround = GetComponent<Collider>().bounds.extents.y; // distancia entre el centro del rigidbody y el suelo
    }
    
    void Update()
    {
        Movement();      
        Look();
        Jump();
        //float sho = Input.GetAxisRaw("Fire1");
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            shoot.Play();
        }        
    }

    private void Movement()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        float spr = Input.GetAxisRaw("Sprint");

        Vector3 velocity = Vector3.zero; // Que la velocidad sea 0 cuando estás quieto

        if (hor != 0 || ver != 0)
        {
            Vector3 dir = (transform.forward * ver + transform.right * hor).normalized; //hacia adelante según la flecha que presione W S A D
            velocity = dir * walkSpeed;            
        }        

        if (spr != 0)
        {
            Vector3 dir = (transform.forward * ver + transform.right * hor).normalized; //NORMALIZED: para que cuando se presionen ambos axis se muevan a la misma velocidad en todas las direcciones
            velocity = dir * runSpeed;
        }
        else
        {
            Vector3 dir = (transform.forward * ver + transform.right * hor).normalized;
            velocity = dir * walkSpeed;
        }

        velocity.y = rb.velocity.y; // Independientemente de estar quieto o moviéndose el componente vertical de movimiento será igual a la velocidad del rigidbody (Velocidad de caída) como una gravedad
        rb.velocity = velocity;

        if (Input.GetButtonDown("Horizontal"))
        {
            if(VActive == false)
            {
                HActive = true;
                walk.Play();
            }          
        }

        if (Input.GetButtonDown("Vertical"))
        {
            if(HActive == false)
            {
                VActive = true;
                walk.Play();
            }      
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            HActive = false;
            if (HActive == false)
            {
                walk.Pause();
            }
        }

        if (Input.GetButtonUp("Vertical"))
        {
            VActive = false;
            if(VActive == false)
            {
                walk.Pause();
            }
        }
    }

    private void Look()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        if (hor != 0)
        {
            transform.Rotate(Vector3.up * hor * sensibility.x);
        }
        if (ver != 0)
        {
            Vector3 rotation = camara.localEulerAngles; // LOCALEULERANGLES: rotación local de la cámara en respecto al jugador 
            rotation.x = (rotation.x - ver * sensibility.y + 360) % 360; // para que siempre obtengamos un ángulo que vaya entre 0° y 360°
            if (rotation.x > 80 && rotation.x < 180) // máximo 80° mirando hacia arriba
            {
                rotation.x = 80;
            }
            else if (rotation.x < 280 && rotation.x > 180) // mínimo 280° mirando hacia abajo
            {
                rotation.x = 280;
            }
            camara.localEulerAngles = rotation;
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse); // FORCEMODE: añade una fuerza de impulso al rigidbody
            jump.Play();
        }
    }

    private bool IsGrounded()
    {
        return Physics.BoxCast(transform.position, new Vector3(0.4f, 0, 0.4f), Vector3.down, Quaternion.identity, distanceToGround + 0.1f);
        // espande una "caja" en la mitad del rigidbody hacia abajo limitadamente que indica si colisiona con el suelo (cuando saltamos esta caja se expande pero no llega a colisionar nada)
    }

    private void Shoot()
    {
        Vector3 hitPos;

        RaycastHit hit; // Raycast: una línea imaginaria que detecta colisiones en frente de el dependiendo de la dirección hacia donde apunta
        if (Physics.Raycast(camara.position, camara.forward, out hit, Mathf.Infinity, whatCanShoot)) // Origen es la cámara , dirección hacia adelante , una distancia infinita y que podemos disparar
        {
            if (hit.collider)
            {
                print(hit.collider.name);
            }
            hitPos = hit.point;
        }
        else
        {
            hitPos = camara.position + camara.forward * 1000;
        }
        Vector3 direction = hitPos - firePoint.position; // el punto donde vamos a disparar y su dirección

        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = firePoint.position;
        bullet.transform.forward = direction;

        bullet.GetComponent<Rigidbody>().velocity = direction.normalized * 7;

        //Destroy(bullet, 1);
    }
    
}
