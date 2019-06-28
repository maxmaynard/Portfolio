using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Burst;

public class PlayerInputSystem : JobComponentSystem {
    protected override JobHandle OnUpdate(JobHandle inputDeps) {
        var job = new PlayerInputJob {
            horizontalAxis = Input.GetAxisRaw("Horizontal"),
            verticalAxis = Input.GetAxisRaw("Vertical"),
            isCrouching = Input.GetButton("Crouch"),
            isSprinting = Input.GetButton("Sprint")
        };

        return job.Schedule(this, inputDeps);
    }

    #region Input Job
    [BurstCompile]
    struct PlayerInputJob : IJobForEach<PlayerInputComponent> {
        public float horizontalAxis;
        public float verticalAxis;
        public BlittableBool isCrouching;
        public BlittableBool isSprinting;

        public void Execute(ref PlayerInputComponent component) {
            component.horizontalAxis = horizontalAxis;
            component.verticalAxis = verticalAxis;
            component.isCrouching = isCrouching;
            component.isSprinting = isSprinting;
        }
    }
    #endregion
}