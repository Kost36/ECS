﻿using ECSCore.Attributes;
using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Components;
using GameLib.Components.Energy;
using GameLib.Mechanics.Company.Components;
using GameLib.Mechanics.Scanning.Components;
using GameLib.Static;
using System;

namespace GameLib.Mechanics.Scanning.Systems
{
    /// <summary>
    /// Система сканирования пространства сектора
    /// </summary>
    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(10)]
    [SystemEnable]
    public class ScannerSystem : SystemExistComponents<Energy, Position, ShipModuleScanner, OwnerСompany>, ISystemAction
    {
        public override void Action(Guid entityId, Energy energy, Position position, ShipModuleScanner shipModuleScanner, OwnerСompany ownerСompany, float deltatime)
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

            Scan(energy, position, shipModuleScanner, ownerСompany);
        }

        private void Scan(Energy energy, Position position, ShipModuleScanner shipModuleScanner, OwnerСompany ownerСompany)
        {
            energy.Fact -= shipModuleScanner.EnergyConsumption;
            shipModuleScanner.PreviousScanTicks = DateTimeOffset.UtcNow.Ticks;
            shipModuleScanner.NextScanTimeInTicks = DateTimeOffset.UtcNow.Ticks + shipModuleScanner.IntervalScanInSec * TimeSpan.TicksPerSecond;

            var entityIds = SpaceEntityManager.GetIdEntitesInRadius(position, shipModuleScanner.ScanerRange);

            if (IECS.GetEntity(ownerСompany.CompanyEntityId, out Entity entity))
            {

                //Добавить информацию по ним в коллекцию

                //TODO Добавили Id сущьности в коллекцию, потом что то произошло и из ECS пропала сущьность (ее уничтожили) => позже создалась другия сущьность под этим id !!!
            }
        }
    }
}
