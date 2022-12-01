using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAmmo : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentAmmo = 10;
    public static int maxAmmo = 10;
    [SerializeField] private Animator anim;

    public static bool reloding = false;
    public static bool relod = false;

    void Update()
    {   
        //If the current ammo is less than or equle to 0 then we trigger the relod animation and reset to max ammo
        if((currentAmmo <= 0 && !reloding) || relod)
        {
            anim.SetTrigger("Relod");
            StartCoroutine(RelodGun_Aysnc());
            reloding = true;     
            relod = false;       
        }

        UIController.uIControllerInstance.bulletText.text = "" + currentAmmo;

    }

    IEnumerator RelodGun_Aysnc()
    {
        yield return new WaitForSeconds(4f);
        currentAmmo = maxAmmo;
        reloding = false;
    }
}
