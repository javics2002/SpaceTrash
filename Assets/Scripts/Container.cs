using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] Trash.TrashType type;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PortableObject"))
        {
            if (type != Trash.TrashType.special &&
                collision.gameObject.GetComponentInChildren<Trash>().IsType(type))
                GameManager.GetInstance().AddPoints(100);
            else if (type == Trash.TrashType.special &&
                collision.gameObject.GetComponentInChildren<Trash>().IsType(Trash.TrashType.radioactive))
                GameManager.GetInstance().badEnding = true;

                Destroy(collision.gameObject);
        }
    }
}
