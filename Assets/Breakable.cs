using UnityEngine;

public class Breakable : MonoBehaviour
{
    public float minMomentumToKnock = 0f;
    public Rigidbody[] bodies;
    public float explosionForce;
    public float explosionRadius;
    public float upwardsModifier;
    public Rigidbody myBody;
    public Collider myCollider;
    
    private void Awake() {
        foreach (var body in bodies) {
            body.isKinematic = true;
        }
    }
    private void OnTriggerEnter(Collider other) {
        var player = other.transform.parent.GetComponent<PlayerController>();
        if (player != null) {
            if (player.momentum.magnitude >= minMomentumToKnock) {
                player.Hit();
                foreach (var body in bodies) {
                    body.isKinematic = false;
                    
                    body.AddExplosionForce(explosionForce, player.transform.position, explosionRadius, upwardsModifier);
                    Destroy(body.gameObject, 0.7f);
                }
                myBody.isKinematic = true;
                myCollider.enabled = false;
                
            }
        }
    }
}
