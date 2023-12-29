using ECSCore.Attributes;
using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Components;
using GameLib.Datas;
using GameLib.Mechanics.Production.Components;
using System;

namespace GameLib.Mechanics.Production.Systems
{
    [SystemCalculate(SystemCalculateInterval.Min1Once)]
    [SystemPriority(50)]
    [SystemEnable]
    public class BridgeProductionModulToStantionSystem : SystemExistComponents<BridgeProductionModulToStantion, WarehouseProductionModul>, ISystemActionAdd, ISystemAction
    {
        public override void ActionAdd(BridgeProductionModulToStantion _, WarehouseProductionModul warehouseProductionModul, Entity entity)
        {
            MoveProducts(entity, warehouseProductionModul);
        }

        public override void Action(Guid entityId, BridgeProductionModulToStantion _, WarehouseProductionModul warehouseProductionModul, float deltaTime)
        {
            //Должна быть отдельная система для изменения процента заполнения склада Raw материалами (при добавлении компонента должно присвоиться значение процента и рассчитаться значения по каждому компоненту)
            //Вызывается при измнении настроек и при добавлении (инициализации)

            if (IECS.GetEntity(entityId, out var entity))
            {
                MoveProducts(entity, warehouseProductionModul);
            } //Получаем сущьность производственного модуля
        }

        /// <summary>
        /// Перемещение товаров между складами станции и производственного модуля
        /// </summary>
        /// <param name="entity"> Сущьность производственного модуля </param>
        /// <param name="warehouseModul"> Склад производственного модуля </param>
        private void MoveProducts(Entity entity, WarehouseProductionModul warehouseModul)
        {
            if (entity.ExternalEntity != null)
            {
                if (entity.ExternalEntity.TryGetComponent<Warehouse>(out var warehouseStantion))
                {
                    MoveProduct(warehouseModul, warehouseStantion);
                    MoveRaws(warehouseStantion, warehouseModul);
                } //Если у станции есть склад
            } //Если у производственного модуля есть родительская сущьность станции
        }

        /// <summary>
        /// Переместить продукт со склада производственного модуля на склад станции
        /// </summary>
        /// <param name="warehouseModule"> Склад производственного модуля </param>
        /// <param name="warehouseStantion"> Склад станции </param>
        private void MoveProduct(WarehouseProductionModul warehouseModule, Warehouse warehouseStantion)
        {
            if(warehouseModule.Product.Value.Value == 0)
            {
                return;
            } //Если продукт отсутствует на складе производственного модуля

            Count count;
            if (warehouseStantion.Products.TryGetValue(warehouseModule.Product.Key, out count))
            {
                Move(warehouseModule.Product.Value, count); //Переместим продукт на склад станции
            } //Если на складе станции есть продукт
            else
            {
                warehouseStantion.Products.Add(warehouseModule.Product.Key, new Count()); //Добавим продукт на склад станции
                if (warehouseStantion.Products.TryGetValue(warehouseModule.Product.Key, out count))
                {
                    Move(warehouseModule.Product.Value, count); //Переместим продукт на склад станции
                }
            } //Если на складе станции нету продукта
        }

        /// <summary>
        /// Переместить сырье со склада станции на склад производственного модуля
        /// </summary>
        /// <param name="warehouseStantion"> Склад станции </param>
        /// <param name="warehouseModule"> Склад производственного модуля </param>
        private void MoveRaws(Warehouse warehouseStantion, WarehouseProductionModul warehouseModule)
        {
            foreach (var raw in warehouseModule.Raws)
            {
                if (warehouseStantion.Products.TryGetValue(raw.Key, out var count))
                {
                    if (count.Value == 0)
                    {
                        continue;
                    } //Если продукт отсутствует на складе станции

                    Move(count, raw.Value); //Переместить товар на склад производственного модуля
                } //Если на складе станции есть сырьевой товар для производственного модуля
            } //Перемещаем на склад производственного модуля сырье со склада станции
        }

        /// <summary>
        /// Переместить продукт
        /// </summary>
        /// <param name="source"> Источник </param>
        /// <param name="destination"> Приемник </param>
        private void Move(Count source, Count destination)
        {
            var availableСapacity = destination.MaxValue - destination.Value; //Разрешенное количество товара для перемещения на склад
            if (availableСapacity > source.Value)
            {
                destination.Value += source.Value;
                source.Value = 0;
            } //Если на складе хватает места для перемещения товара
            else
            {
                destination.Value += availableСapacity;
                source.Value -= availableСapacity;
            } //Если на складе не хватает места для перемещения товара
        }
    }
}

//TODO При добавлении моста есть смысл включить закупку сырья? Или этим должны заниматься AI станции \ Игрок? 