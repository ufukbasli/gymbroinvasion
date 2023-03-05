using System;
using UnityEngine;

[CreateAssetMenu]
public class PlayerReference : ScriptableObject {
    [NonSerialized]
    public PlayerController playerController;
}
