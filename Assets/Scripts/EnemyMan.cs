using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMan : MonoBehaviour {
    public PlayerReference playerReference;
    public float minMomentumToKnock = .01f;
    public Rigidbody hipBody;
    public float knockBack = 1f;
    public float knockBackUp = 1f;
    private NavMeshAgent agent;
    private Animator animator;
    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        var bodies = GetComponentsInChildren<Rigidbody>();
        foreach (var body in bodies) {
            body.isKinematic = true;
        }
    }
    private void Update() {
        var player = playerReference.playerController;
        if (player == null) return;
        var target = player.transform.position;
        agent.SetDestination(target);
    }
    private void OnTriggerEnter(Collider other) {
        var player = other.transform.parent.GetComponent<PlayerController>();
        if (player != null) {
            if (player.momentum.magnitude >= minMomentumToKnock) {
                player.Hit(this);
                Die(player.momentum);
            }
        }

    }
    private void Die(Vector3 momentum) {
        enabled = false;
        agent.enabled = false;
        animator.enabled = false;
        var bodies = GetComponentsInChildren<Rigidbody>();
        foreach (var body in bodies) {
            body.isKinematic = false;
        }
        GetComponent<CapsuleCollider>().enabled = false;
        hipBody.AddForce(momentum * knockBack, ForceMode.Impulse);
        hipBody.AddForce(Vector3.up * knockBackUp, ForceMode.Impulse);
    }
}
