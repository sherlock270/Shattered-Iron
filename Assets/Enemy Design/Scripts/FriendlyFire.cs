using UnityEngine;

public class FriendlyFire : MonoBehaviour
{
    Rigidbody m_Rigidbody;

    void Start()
    {
        Physics.IgnoreLayerCollision(10, 9);
    }
}
