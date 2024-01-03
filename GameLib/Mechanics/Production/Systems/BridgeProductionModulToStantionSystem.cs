﻿using ECSCore.Attributes;
using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Datas;
using GameLib.Mechanics.Production.Components;
using GameLib.Mechanics.Stantion.Components;
using System;

namespace GameLib.Mechanics.Production.Systems
{
    [SystemCalculate(SystemCalculateInterval.Min1Once)]
    [SystemPriority(50)]
    [SystemEnable]
    public class BridgeProductionModulToStantionSystem : SystemExistComponents<BridgeProductionModulToStantion, WarehouseProductionModule>, ISystemActionAdd, ISystemAction
    {
        public override void ActionAdd(BridgeProductionModulToStantion _, WarehouseProductionModule warehouseProductionModul, Entity entity)
        {
            MoveProducts(entity, warehouseProductionModul);
        }

        public override void Action(Guid entityId, BridgeProductionModulToStantion _, WarehouseProductionModule warehouseProductionModule, float deltaTime)
        {
            //Должна быть отдельная система для изменения процента заполнения склада Raw материалами (при добавлении компонента должно присвоиться значение процента и рассчитаться значения по каждому компоненту)
            //Вызывается при измнении настроек и при добавлении (инициализации)

            if (!IsMoveNeed(warehouseProductionModule))
            {
                return;
            }

            if (IECS.GetEntity(entityId, out var entity))
            {
                MoveProducts(entity, warehouseProductionModule);
            } //Получаем сущьность производственного модуля
        }

        /// <summary>
        /// Необходимо ли перемещение
        /// </summary>
        /// <returns></returns>
        private bool IsMoveNeed(WarehouseProductionModule warehouseProductionModule)
        {
            return true; //Проверить необходимость перемещений, что бы не тянуть сущьности раз в секунду (IECS.GetEntity)
        }

        /// <summary>
        /// Перемещение товаров между складами станции и производственного модуля
        /// </summary>
        /// <param name="entity"> Сущьность производственного модуля </param>
        /// <param name="warehouseModul"> Склад производственного модуля </param>
        private void MoveProducts(Entity entity, WarehouseProductionModule warehouseModule)
        {
            if (entity.ExternalEntity != null)
            {
                if (entity.ExternalEntity.TryGetComponent<Warehouse>(out var warehouseStantion))
                {
                    MoveProduct(warehouseModule, warehouseStantion);
                    MoveRaws(warehouseStantion, warehouseModule);
                }
            }
        }

        /// <summary>
        /// Переместить продукт со склада производственного модуля на склад станции
        /// </summary>
        /// <param name="warehouseModule"> Склад производственного модуля </param>
        /// <param name="warehouseStantion"> Склад станции </param>
        private void MoveProduct(WarehouseProductionModule warehouseModule, Warehouse warehouseStantion)
        {
            if(warehouseModule.Product.Value.Value == 0)
            {
                return;
            }

            if (warehouseStantion.Products.TryGetValue(warehouseModule.Product.Key, out var count))
            {
                Move(warehouseModule.Product.Value, count); //Переместим продукт на склад станции
            }
            else
            {
                warehouseStantion.Products.Add(warehouseModule.Product.Key, new Count()); //Добавим продукт на склад станции
                if (warehouseStantion.Products.TryGetValue(warehouseModule.Product.Key, out count))
                {
                    Move(warehouseModule.Product.Value, count); //Переместим продукт на склад станции
                }
            }
        }

        /// <summary>
        /// Переместить сырье со склада станции на склад производственного модуля
        /// </summary>
        /// <param name="warehouseStantion"> Склад станции </param>
        /// <param name="warehouseModule"> Склад производственного модуля </param>
        private void MoveRaws(Warehouse warehouseStantion, WarehouseProductionModule warehouseModule)
        {
            foreach (var raw in warehouseModule.Raws)
            {
                if (warehouseStantion.Products.TryGetValue(raw.Key, out var count))
                {
                    if (count.Value == 0)
                    {
                        continue;
                    }

                    Move(count, raw.Value); //Переместить товар на склад производственного модуля
                }
            }
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
            }
            else
            {
                destination.Value += availableСapacity;
                source.Value -= availableСapacity;
            }
        }
    }
}