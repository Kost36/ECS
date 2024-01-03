﻿using GameLib.Collections.QuadTree;
using GameLib.Components;
using GameLib.Constants;
using GameLib.Structures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLib.Static
{
    /// <summary>
    /// Менеджер сущьностей в пространстве
    /// </summary>
    public static class SpaceEntityManager
    {
        private static readonly object _lockObject = new object();
        private static readonly QuadTree<Position> _quadTree = new QuadTree<Position>(
            splitCount: 50, 
            depthLimit: 20, 
            region: new Quad(
                min: new Point(SectorSize.StartPozitionX, SectorSize.StartPozitionY), 
                max: new Point(SectorSize.StartPozitionX + SectorSize.Width, SectorSize.StartPozitionY + SectorSize.Height)));

        /// <summary>
        /// Добавить сущьность или обновить позицию сущьности
        /// </summary>
        /// <param name="position"></param>
        public static void AddOrUpdatePosition(Position position)
        {
            lock (_lockObject)
            {
                _quadTree.AddOrUpdate(position, new Point((long)position.X, (long)position.Y));
            }
        }

        /// <summary>
        /// Удалить сущьность
        /// </summary>
        public static void Remove(Position position)
        {
            lock (_lockObject)
            {
                _quadTree.Remove(position);
            }
        }

        /// <summary>
        /// Получить идентификаторы сущьностей, которые находятся в радиусе
        /// </summary>
        public static List<Guid> GetIdEntitesInRadius(Position position, long radius)
        {
            lock (_lockObject)
            {
                _quadTree.FindСollisionsForCircle(
                new Circle(
                    new Point(
                        (int)position.X,
                        (int)position.Y),
                    radius),
                out List<Position> collisions);

                return collisions.Select(item => item.Id).ToList();
            }
        }
    }
}

//Todo 
//1) Сущьность пропадает из пространства (к примеру произошла стыковка корабля со станцией) 
//2) Сущьность уничтожена
//3) Сущьность переместилась
//4) Уйти от статики
//5) Потокобезопасность
