using UnityEngine;

public class Hit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("People") && PlayerController.Instance.IsPunching()) {
            other.gameObject.GetComponentInParent<People>().RemoveOneHP();
            if (other.gameObject.GetComponentInParent<People>().GetHP() <= 0)
            {
                // Ragdoll
            }
        }
    }
}