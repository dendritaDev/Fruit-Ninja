using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerFollow : MonoBehaviour
{

    public Camera _camera;
   
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);//cogemos donde esta el raton por pantalla, lo pasamos a posiciones en el mundo del juego
        mousePos = new Vector3(mousePos.x, mousePos.y); //si no escribimos nada en 0 es que z = 0, ya que por defecto se pone en la posicion de la camara en z y esta tan cerca que no se llega a ver

        transform.position = mousePos; //le da al trail la pos dell raton
    }
}
