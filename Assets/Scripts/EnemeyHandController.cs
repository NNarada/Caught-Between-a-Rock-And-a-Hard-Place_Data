using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyHandController : MonoBehaviour
{
    [SerializeField] GameObject m_RightFist;
    public void activateFist()
    {
        m_RightFist.GetComponent<Collider>().enabled = true;
    }

    public void deactivateFist()
    {
        m_RightFist.GetComponent<Collider>().enabled = false;
    }
}
