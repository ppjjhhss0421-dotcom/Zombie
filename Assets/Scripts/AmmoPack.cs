using UnityEngine;

public class AmmoPack : MonoBehaviour, Iitem
{
    public int ammo = 30;
    public void Use(GameObject target)
    {
        Debug.Log("탄알이 증가했다 : " + ammo);
    }

   
}
