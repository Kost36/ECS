using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Mechanics.Move.Components;

namespace GameLib.Mechanics.Move.Systems
{
    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(20)]
    [AttributeSystemEnable]
    [AttributeExcludeComponentSystem(typeof(Acceleration))]
    public class ControlEndPositionSystem : SystemExistComponents<Way, Speed>, ISystemAction
    {
        public override void Action(int entityId, Way way, Speed speed, float deltatime)
        {
            if (way.InitOk && way.Len < 1 && speed.Fact < 0.1)
            {
                IECS.RemoveComponent<Way>(entityId); //Удалим путь
                IECS.RemoveComponent<PozitionSV>(entityId); //Удалим точку перемещения
                IECS.RemoveComponent<Speed>(entityId); //Удалим скорость
                IECS.RemoveComponent<SpeedSV>(entityId); //Удалим заданную скорость
            }
        }
    }
}
