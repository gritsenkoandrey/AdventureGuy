using UnityEngine;


public class Obstacles : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if(unit && unit as Character)
            unit.ReceiveDamage();
    }
}