using UnityEngine;

public class WindParticle : MonoBehaviour
{
    private Vector3 moveDirection;
    private float lifetime = 3.0f;
    private float speed = 3.0f;

    public void Initialize(Vector3 direction, float life)
    {
        moveDirection = direction;
        lifetime = life;
        Destroy(gameObject, lifetime); // éıñΩÇ≈é©ìÆçÌèú
    }

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

}
