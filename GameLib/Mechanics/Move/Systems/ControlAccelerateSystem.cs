using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Mechanics.Move.Components;

namespace GameLib.Mechanics.Move.Systems
{
    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(15)]
    [AttributeSystemEnable]
    [AttributeExcludeComponentSystem(typeof(Acceleration))]
    public class ControlAccelerateSystem : SystemExistComponents<SpeedSV>, ISystemAction
    {
        public override void Action(int entityId, SpeedSV speedSV, float deltatime)
        {
            if (speedSV.Update)
            {
                if (IECS.GetComponent(entityId, out Acceleration _) == false)
                {
                    IECS.AddComponent(new Acceleration() { Id = entityId });
                } //Если нету ускорения
                speedSV.Update = false;
            }
        }
    }
}
