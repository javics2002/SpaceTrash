using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coworker : MonoBehaviour
{
    [SerializeField] UIManager uim;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("PortableObject") && collision.gameObject.GetComponentInChildren<SpecialTrash>())
        {
            uim.ShowSpecialDialogue();
            Destroy(collision.gameObject);
        }
    }
}
