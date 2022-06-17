using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public static bool on = true;
    public static float speed = 1;

    [SerializeField] Vector3 direction;

    Vector3 pos;
    Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        pos = body.position;
    }

    void Update()
    {
        //Mueve la cinta en su direccion y la vuelve a teletransportar atras para dar la sensacion
        //de movimiento de los objetos que hay encima
        body.position += speed * direction * Time.deltaTime;
        body.MovePosition(pos);
    }
}
