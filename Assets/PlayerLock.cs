using UnityEngine;

public class PlayerLock : MonoBehaviour {
    public PlayerReference playerReference;
    public bool playerLocked = false;
    private void Update() {
        playerReference.playerController.locked = playerLocked;
    }
}
