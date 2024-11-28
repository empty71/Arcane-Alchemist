using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Tree"))
        {
            TreeHealth tree = other.GetComponent<TreeHealth>();
            if (tree != null)
            {
                tree.TakeDamage();
            }
        }
    }
}
