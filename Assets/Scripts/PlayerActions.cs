using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerActions : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro UseText;
    [SerializeField]
    private Transform Camera;
    [SerializeField]
    private float MaxUseDistance = 5f;
    [SerializeField]
    private LayerMask UseLayers;

    public void OnUse()
    {
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers))
        {
            if (hit.collider.TryGetComponent<DoorController>(out DoorController door))
            {
                if (door.isOpen)
                {
                    door.Close();
                }
                else
                {
                    door.Open();
                }
            }
        }
    }

    private void Update()
    {
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers)
            && hit.collider.TryGetComponent<DoorController>(out DoorController door))
        {
            if(!door.isLockedDoor)
            {
                if (door.isOpen)
                {
                    UseText.SetText("Close \"F\"");
                }
                else
                {
                    UseText.SetText("Open \"F\"");
                }
            }
            else
            {
                if (door.isOpen)
                {
                    UseText.SetText("Close \"F\"");
                }
                else
                {
                    UseText.SetText("Door is Locked Collect 7 Keys");
                }
            }
            UseText.gameObject.SetActive(true);
            UseText.transform.position = hit.point - (hit.point - Camera.position).normalized * 0.5f;
            UseText.transform.rotation = Quaternion.LookRotation((hit.point - Camera.position).normalized);
        }
        else
        {
            UseText.gameObject.SetActive(false);
        }
    }
}
