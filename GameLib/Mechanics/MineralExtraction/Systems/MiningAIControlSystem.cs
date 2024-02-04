using ECSCore.Attributes;
using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Components;
using GameLib.Components.Energy;
using GameLib.Mechanics.Company.Components;
using GameLib.Mechanics.MineralExtraction.AI.Components;
using GameLib.Mechanics.MineralExtraction.AI.Enums;
using GameLib.Mechanics.MineralExtraction.Components;
using GameLib.Mechanics.MineralExtraction.Components.Commands;
using GameLib.Mechanics.MineralExtraction.Entites;
using GameLib.Mechanics.Move.Components;
using GameLib.Mechanics.Products.Enums;
using GameLib.Mechanics.Products.Extensions;
using GameLib.Mechanics.Stantion.Components;
using MathLib;
using MathLib.Structures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLib.Mechanics.MineralExtraction.Systems
{
    [SystemCalculate(SystemCalculateInterval.Min10Once)]
    [SystemPriority(50)]
    [SystemEnable]
    public sealed class MiningAIControlSystem : SystemExistComponents<MiningAI, ShipMiningSetting>, ISystemAction
    {
        private readonly int _intervalSearchMiningInSec = 60;

        public override void Action(Guid entityId, MiningAI miningAI, ShipMiningSetting shipMiningSetting, float deltaTime)
        {
            switch (miningAI.MiningAIState)
            {
                case MiningAIState.Wait:
                    //Todo Проверить состояние (возможно требуется выгрузка)
                    CheckAction(shipMiningSetting);
                    break;
                case MiningAIState.StartMoveToAsteroid:
                    break;
                case MiningAIState.MovingToAsteroid:
                    break;
                case MiningAIState.MiningFromAsteroid:
                    break;
                case MiningAIState.MovingToStantion:
                    //Todo Обработать тут
                    break;
                case MiningAIState.UnloadToStantion:
                    break;
                case MiningAIState.SearchStantionForSale:
                    break;
                case MiningAIState.SaleToStantion:
                    break;
                    
                    //Если напали, снять все промежуточные компоненты, после нормализации вернуться Wait все должно стартонуть заново
            }
        }

        private void CheckAction(ShipMiningSetting shipMiningSetting)
        {
            if (shipMiningSetting.TimeNextAnalys < DateTimeOffset.UtcNow)
            {
                shipMiningSetting.TimeNextAnalys = DateTimeOffset.UtcNow.AddSeconds(_intervalSearchMiningInSec);

                if (IECS.GetComponent<ShipCommandSupplyStantion>(shipMiningSetting.Id, out var _))
                {
                    IECS.AddComponent(new ShipCommandSearch() { Id = shipMiningSetting.Id });
                }
            }
        }
    }

    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(50)]
    [SystemEnable]
    public sealed class SetMiningAIStateSystem : SystemExistComponents<SetMiningAIState, MiningAI>, ISystemActionAdd
    {
        public override void ActionAdd(
            SetMiningAIState setMiningAIState,
            MiningAI miningAI,
            Entity shipEntity)
        {
            miningAI.MiningAIState = setMiningAIState.MiningAIState;
            shipEntity.RemoveComponent<SetMiningAIState>();
        }
    }

    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(50)]
    [SystemEnable]
    public sealed class MiningAISearchSupplyOptionSystem : SystemExistComponents<MiningAI, ShipCommandSupplyStantion, ShipCommandSearch, ShipMiningSetting, RefOwnerCompany>, ISystemActionAdd
    {
        public override void ActionAdd(
            MiningAI miningAI,
            ShipCommandSupplyStantion shipCommandSupplyStantion,
            ShipCommandSearch shipCommandSearch,
            ShipMiningSetting shipMiningSetting,
            RefOwnerCompany refOwnerCompany,
            Entity shipEntity)
        {
            if (miningAI.MiningAIState != MiningAIState.Wait)
            {
                //Todo Add component error or component msg
                return;
            }

            if (!IECS.GetEntity(shipCommandSupplyStantion.StantionId, out var stantionEntity))
            {
                //Todo Add component error or component msg
                return;
            }

            if (!stantionEntity.TryGetComponent<Position>(out var stantionPositionComponent))
            {
                //Todo Add component error or component msg
                return;
            }

            if (!stantionEntity.TryGetComponent<Warehouse>(out var stantionWarehouseComponent))
            {
                //Todo Add component error or component msg
                return;
            }

            if (refOwnerCompany == null)
            {
                //Todo Add component error or component msg
                return;
            }

            if (!refOwnerCompany.Company.TryGetComponent<KnownInformations>(out var knownInformations))
            {
                //Todo Add component error or component msg
                return;
            }

            var needRawMinerals = GetNeedMinerals(stantionWarehouseComponent, shipMiningSetting);
            var groupedAvailableAsteroids = GetAvailableAsteroids(knownInformations, needRawMinerals, shipMiningSetting, stantionPositionComponent);

            FindBestMiningOption(shipEntity, miningAI, needRawMinerals, groupedAvailableAsteroids);
        }

        private List<KeyValuePair<ProductType, WarehouseProductInfo>> GetNeedMinerals(
            Warehouse warehouse,
            ShipMiningSetting shipMiningSetting)
        {
            var needRawMinerals = warehouse.Products
                .Where(product =>
                    product.Value.IsRaw
                    && product.Key.IsMineral()
                    && product.Value.Count < product.Value.MaxLimit
                    && shipMiningSetting.AvailableMineralsForMining.Contains(product.Key.ToMineral()))
                .OrderByDescending(item => item.Value.Count)
                .ToList();

            return needRawMinerals;
        }

        private IEnumerable<IGrouping<MineralType, Tuple<Guid, AsteroidMineral, Position, float>>> GetAvailableAsteroids(
            KnownInformations knownInformations,
            List<KeyValuePair<ProductType, WarehouseProductInfo>> needRawMinerals,
            ShipMiningSetting shipMiningSetting,
            Position stantionPosition)
        {
            var asteroids = knownInformations.AvailableInformations.GetAllEntites(typeof(Asteroid));
            var asteroidInfos = new List<Tuple<Guid, AsteroidMineral, Position, float>>();

            foreach (var asteroid in asteroids)
            {
                if (!asteroid.TryGetComponent<AsteroidMineral>(out var asteroidMineral))
                {
                    continue;
                }

                if (!needRawMinerals.Exists(item => item.Key == asteroidMineral.Type.ToProduct()))
                {
                    continue;
                }

                if (!asteroid.TryGetComponent<Position>(out var asteroidPosition))
                {
                    continue;
                }

                var distanceToAsteroid = Distance.GetDistance(
                    new Point3d(asteroidPosition.X, asteroidPosition.Y, asteroidPosition.Z),
                    new Point3d(stantionPosition.X, stantionPosition.Y, stantionPosition.Z));

                if (distanceToAsteroid > shipMiningSetting.AllowedMiningDistance)
                {
                    continue;
                }

                asteroidInfos.Add(new Tuple<Guid, AsteroidMineral, Position, float>(asteroid.Id, asteroidMineral, asteroidPosition, distanceToAsteroid));
            }

            return asteroidInfos.GroupBy(item => item.Item2.Type);
        }

        private void FindBestMiningOption(
            Entity shipEntity,
            MiningAI miningAI,
            List<KeyValuePair<ProductType, WarehouseProductInfo>> needRawMinerals,
            IEnumerable<IGrouping<MineralType, Tuple<Guid, AsteroidMineral, Position, float>>> groupedAvailableAsteroids)
        {
            var bestMiningOption = new Tuple<bool, Guid, float>(false, Guid.Empty, float.MaxValue);

            foreach (var needRawMineral in needRawMinerals)
            {
                var maxNeedCount = needRawMineral.Value.MaxLimit - needRawMineral.Value.Count;
                var groupAvailableAsteroids = groupedAvailableAsteroids.Single(group => group.Key == needRawMineral.Key.ToMineral()).ToList();

                foreach (var availableAsteroid in groupAvailableAsteroids)
                {
                    if (availableAsteroid.Item4 < bestMiningOption.Item3)
                    {
                        bestMiningOption = new Tuple<bool, Guid, float>(true, availableAsteroid.Item1, availableAsteroid.Item4);
                    }
                }

                if (bestMiningOption.Item1)
                {
                    shipEntity.AddComponent(new ShipCommandMoveToAsteroid() { TargetAsteroidId = bestMiningOption.Item2 });
                    shipEntity.AddComponent(new SetMiningAIState() { MiningAIState = MiningAIState.StartMoveToAsteroid });
                    shipEntity.RemoveComponent<ShipCommandSearch>();
                    break;
                }
            }
        }
    }

    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(50)]
    [SystemEnable]
    [ExcludeComponentSystem(typeof(PositionSV))]
    public sealed class MoveCommandToAsteroidSystem : SystemExistComponents<MiningAI, ShipCommandMoveToAsteroid>, ISystemActionAdd
    {
        public override void ActionAdd(
            MiningAI miningAI,
            ShipCommandMoveToAsteroid shipCommandMoveToAsteroid,
            Entity shipEntity)
        {
            if (miningAI.MiningAIState == MiningAIState.StartMoveToAsteroid)
            {
                shipEntity.AddComponent(new StartMoveToEntity() { TargetEntityId = shipCommandMoveToAsteroid.TargetAsteroidId });
                shipEntity.AddComponent(new SetMiningAIState() { MiningAIState = MiningAIState.MovingToAsteroid });
            }
        }
    }

    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(50)]
    [SystemEnable]
    [ExcludeComponentSystem(typeof(PositionSV))]
    public sealed class StartMoveToEntitySystem : SystemExistComponents<StartMoveToEntity>, ISystemActionAdd
    {
        private readonly int _distanceStop = 20;

        public override void ActionAdd(
            StartMoveToEntity startMoveToEntity,
            Entity shipEntity)
        {
            if (!IECS.GetEntity(startMoveToEntity.TargetEntityId, out var targetEntity))
            {
                //Todo Add component error or component msg
                return;
            }

            if (!targetEntity.TryGetComponent<Position>(out var targetPositionComponent))
            {
                //Todo Add component error or component msg
                return;
            }

            //Todo Найти точку на окружности с радиусом _distanceStop от максимального края объекта targetEntity в зависимости от размера целевой сущьности
            //При этом учесть направление в сторону shipEntity и размер shipEntity
            var positionSVComponent = new PositionSV() { X = targetPositionComponent.X - _distanceStop, Y = targetPositionComponent.Y, Z = targetPositionComponent.Z };

            shipEntity.AddComponent(positionSVComponent);
            shipEntity.RemoveComponent<StartMoveToEntity>();
        }
    }

    [SystemCalculate(SystemCalculateInterval.Min10Once)]
    [SystemPriority(50)]
    [SystemEnable]
    [ExcludeComponentSystem(typeof(PositionSV))]
    public sealed class ControlStartMiningSystem : SystemExistComponents<Position, MiningAI, ShipCommandMoveToAsteroid, ShipMiningSetting>, ISystemAction
    {
        public override void Action(Guid entityId, Position position, MiningAI miningAI, ShipCommandMoveToAsteroid shipCommandMoveToAsteroid, ShipMiningSetting shipMiningSetting, float deltaTime)
        {
            if (miningAI.MiningAIState == MiningAIState.MovingToAsteroid)
            {
                if (!IECS.GetEntity(shipCommandMoveToAsteroid.TargetAsteroidId, out var asteroidEntity))
                {
                    //Todo Add component error or component msg
                    return;
                }

                if (!asteroidEntity.TryGetComponent<AsteroidMineral>(out var asteroidMineralComponent))
                {
                    //Todo Add component error or component msg
                    return;
                }

                if (!asteroidEntity.TryGetComponent<Position>(out var asteroidPositionComponent))
                {
                    //Todo Add component error or component msg
                    return;
                }

                var distanceToAsteroid = Distance.GetDistance(
                    new Point3d(asteroidPositionComponent.X, asteroidPositionComponent.Y, asteroidPositionComponent.Z),
                    new Point3d(position.X, position.Y, position.Z));

                if (distanceToAsteroid < shipMiningSetting.MaxMiningDistance)
                {
                    IECS.AddComponent(new ShipCommandMining() { Id = entityId, RefAsteroidMineral = asteroidMineralComponent });
                    IECS.AddComponent(new SetMiningAIState() { Id = entityId, MiningAIState = MiningAIState.MiningFromAsteroid });
                    IECS.RemoveComponent<ShipCommandMoveToAsteroid>(entityId);
                }
            }
        }
    }

    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(50)]
    [SystemEnable]
    public sealed class MiningSystem : SystemExistComponents<ShipModuleMining, ShipCommandMining, ShipMiningSetting, Hold, Energy>, ISystemAction
    {
        public override void Action(Guid entityId, ShipModuleMining shipModuleMining, ShipCommandMining shipCommandMining, ShipMiningSetting shipMiningSetting, Hold hold, Energy energy, float deltaTime)
        {
            if (shipCommandMining.RefAsteroidMineral.Capacity <= 0)
            {
                IECS.RemoveComponent<ShipCommandMining>(shipCommandMining.Id);
                IECS.AddComponent(new SetMiningAIState() { Id = entityId, MiningAIState = MiningAIState.MovingToStantion }); //Processing in MiningAIControlSystem
            }

            var percentageFilling = Percentage.GetPercentage(hold.Use, hold.Max);
            if (percentageFilling > shipMiningSetting.MaxPercentageFillingMaterial)
            {
                IECS.RemoveComponent<ShipCommandMining>(shipCommandMining.Id);
                IECS.AddComponent(new SetMiningAIState() { Id = entityId, MiningAIState = MiningAIState.MovingToStantion }); //Processing in MiningAIControlSystem
            }

            if (energy.Fact < shipMiningSetting.MinPercentageEnergy)
            {
                shipCommandMining.Pause = true;
                return;
            }

            if (energy.Fact < shipModuleMining.EnergyConsumptionPerSec)
            {
                shipCommandMining.Pause = true;
                return;
            }

            if (shipCommandMining.Pause && energy.Fact > shipModuleMining.EnergyConsumptionPerSec * 5)
            {
                shipCommandMining.Pause = false;
            }

            shipCommandMining.CycleCompletionPercentage += shipModuleMining.CompletionPercentagePerSec;
            energy.Fact -= shipModuleMining.EnergyConsumptionPerSec;

            if (shipCommandMining.CycleCompletionPercentage >= 100)
            {
                MiningCycleCompletion(shipCommandMining, hold, shipModuleMining);
            }
        }

        private void MiningCycleCompletion(ShipCommandMining shipCommandMining, Hold hold, ShipModuleMining shipModuleMining)
        {
            shipCommandMining.CycleCompletionPercentage -= 100;

            var availableCapacity = (int)Math.Floor(hold.Max - hold.Use);
            
            var quantityMineral = shipModuleMining.QuantityPerСycle;
            if (quantityMineral > availableCapacity)
            {
                quantityMineral = availableCapacity;
            }

            if (quantityMineral > shipCommandMining.RefAsteroidMineral.Capacity)
            {
                quantityMineral = shipCommandMining.RefAsteroidMineral.Capacity;
            }

            shipCommandMining.RefAsteroidMineral.Capacity -= quantityMineral;
            IECS.AddComponent(new AddProductToShipHold() { Id = hold.Id, ProductType = shipCommandMining .RefAsteroidMineral.Type.ToProduct(), Count = quantityMineral }); //Todo система Добавить сырье в трюм корабля (через компонент событие) => пересчитать заполнение трюма
        }
    }
}