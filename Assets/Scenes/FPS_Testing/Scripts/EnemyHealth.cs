using UnityEngine;
public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public void Damage(float damageAmount)
    {
        health-=damageAmount;
        if (health<=0)
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        Debug.Log(health);
    }
}