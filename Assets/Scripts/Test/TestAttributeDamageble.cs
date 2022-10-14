using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttributeDamageble : MonoBehaviour, IDamageble
{
    public void GetDamage(int damage)
    {
        Debug.Log(gameObject.name);
        Debug.Log(damage);
    }
}
