using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Components.Energy;
using GameLib.Mechanics.Detection.Components;
using System;

namespace GameLib.Mechanics.Company.Systems
{
    /// <summary>
    /// Система сканирования пространства сектора
    /// </summary>
    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(10)]
    [SystemEnable]
    public class ScannerSystem : SystemExistComponents<Energy, ShipModuleScanner>, ISystemAction
    {
        public override void Action(int entityId, Energy energy, ShipModuleScanner shipModuleScanner, float deltatime)
        {
            if (!shipModuleScanner.Enable)
            {
                return;
            }
            
            if (energy.Fact < shipModuleScanner.EnergyConsumption)
            {
                return;
            }

            if (DateTimeOffset.UtcNow.Ticks < shipModuleScanner.NextScanTimeInTicks)
            {
                return;
            }

            Scan(energy, shipModuleScanner);
        }

        public void Scan(Energy energy, ShipModuleScanner shipModuleScanner)
        {
            energy.Fact -= shipModuleScanner.EnergyConsumption;
            shipModuleScanner.PreviousScanTicks = DateTimeOffset.UtcNow.Ticks;
            shipModuleScanner.NextScanTimeInTicks = DateTimeOffset.UtcNow.Ticks + shipModuleScanner.IntervalScanInSec * TimeSpan.TicksPerSecond;

            //shipModuleScanner.ScanerRange;

            //Получить все сущьности сектора в радиусе shipModuleScanner.ScanerRange
            //Добавить информацию по ним в коллекцию
        }
    }
}
