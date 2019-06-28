using Unity.Entities;

public struct PlayerInputComponent : IComponentData {
    public float horizontalAxis;
    public float verticalAxis;
    public BlittableBool isCrouching;
    public BlittableBool isSprinting;
}
