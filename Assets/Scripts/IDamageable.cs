using UnityEngine;

public interface IDamageable
{
    void onDamage(float damage,Vector3 hitpoint , Vector3 hitNormal);

}
