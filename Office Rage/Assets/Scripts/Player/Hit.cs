using UnityEngine;

public class Hit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("People")) {
            other.gameObject.GetComponentInParent<People>().RemoveOneHP();
            if (other.gameObject.GetComponentInParent<People>().GetHP() <= 0)
            {
                // Ragdoll
            }
        }
    }
}