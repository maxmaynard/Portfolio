using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

public class PlayerMovementSystem: JobComponentSystem {
    protected override JobHandle OnUpdate(JobHandle inputDeps) {
        var job = new PlayerMovementJob {
            deltaTime = Time.deltaTime
        };

        return job.Schedule(this, inputDeps);
    }

    #region Input Job
    [BurstCompile]
    struct PlayerMovementJob : IJobForEach<Translation, PlayerInputComponent> {
        public float deltaTime;

        public void Execute(ref Translation translation, [ReadOnly] ref PlayerInputComponent playerInputComponent) {
            translation.Value.x += playerInputComponent.horizontalAxis * deltaTime;
            translation.Value.z += playerInputComponent.verticalAxis * deltaTime;
        }
    }
    #endregion
}