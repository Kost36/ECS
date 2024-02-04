using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Components;
using GameLib.Components.Energy;
using GameLib.Mechanics.Company.Components;
using GameLib.Mechanics.Scanning.Components;
using GameLib.Static;
using System;
using System.Collections.Generic;

namespace GameLib.Mechanics.Scanning.Systems
{
    /// <summary>
    /// Система сканирования пространства сектора
    /// </summary>
    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(10)]
    [SystemEnable]
    public class ScannerSystem : SystemExistComponents<Energy, Position, ShipModuleScanner, RefOwnerCompany>, ISystemAction
    {
        public override void Action(Guid entityId, Energy energy, Position position, ShipModuleScanner shipModuleScanner, RefOwnerCompany refCompany, float deltatime)
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

            if (refCompany.Company == null)
            {
                return;
                //throw new Exception($"Company not set for entity [{energy.Id}]");
            }

            if (!refCompany.Company.TryGetComponent(out KnownInformations knownInformations))
            {
                return;
                //throw new Exception($"Company not has component [{nameof(KnownInformations)}]");
            }

            Scan(energy, position, shipModuleScanner, knownInformations);
        }

        private void Scan(Energy energy, Position position, ShipModuleScanner shipModuleScanner, KnownInformations knownInformations)
        {
            energy.Fact -= shipModuleScanner.EnergyConsumption;
            shipModuleScanner.PreviousScanTicks = DateTimeOffset.UtcNow.Ticks;
            shipModuleScanner.NextScanTimeInTicks = DateTimeOffset.UtcNow.Ticks + shipModuleScanner.IntervalScanInSec * TimeSpan.TicksPerSecond;

            var entityIds = SpaceEntityManager.GetIdEntitesInRadius(position, shipModuleScanner.ScanerRange);
            entityIds.Remove(energy.Id);

            AddEntityIds(knownInformations, entityIds);
        }

        private void AddEntityIds(KnownInformations knownInformations, List<Guid> entityIds)
        {
            foreach (var entityId in entityIds)
            {
                if (IECS.GetEntity(entityId, out var entity))
                {
                    knownInformations.AvailableInformations.AddOrUpdate(entity);
                };
            }
        }
    }
}