using UnityEngine;
using System.Collections;

public class Ragdoll : MonoBehaviour
{
    void Start()
    {
        DeactivateRagdoll();
    }

    public void ActivateRagdoll()
    {
        SetActiveRagdoll(true);
    }

    public void DeactivateRagdoll()
    {
        SetActiveRagdoll(false);
    }

    public void SetActiveRagdoll(bool isActive)
    {
        gameObject.GetComponent<Animator>().enabled = !isActive;
        foreach (Rigidbody bone in GetComponentsInChildren<Rigidbody>())
        {
            if (bone.gameObject.CompareTag("People") || bone.gameObject.CompareTag("Hand") || bone.gameObject.CompareTag("FOV"))
                continue;
            bone.isKinematic = !isActive;
            bone.detectCollisions = isActive;
        }
        foreach (CharacterJoint joint in
        GetComponentsInChildren<CharacterJoint>())
        {
            if (joint.gameObject.CompareTag("People") || joint.gameObject.CompareTag("Hand") || joint.gameObject.CompareTag("FOV"))
                continue;
            joint.enableProjection = isActive;
        }
        foreach (Collider col in
        GetComponentsInChildren<Collider>())
        {
            if (col.gameObject.CompareTag("People") || col.gameObject.CompareTag("Hand") || col.gameObject.CompareTag("FOV"))
                continue;
            col.enabled = isActive;
        }
    }
}
