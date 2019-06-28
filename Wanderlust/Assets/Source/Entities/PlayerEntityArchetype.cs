using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

// ReSharper disable once InconsistentNaming
[RequiresEntityConversion]
public class PlayerEntityArchetype : MonoBehaviour, IConvertGameObjectToEntity {
    // The MonoBehaviour data is converted to ComponentData on the entity.
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
        var types = new ComponentTypes(typeof(PositionComponent), typeof(RotationComponent), typeof(PlayerInputComponent));
        dstManager.AddComponents(entity, types);
    }
}
