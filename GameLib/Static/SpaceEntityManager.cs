using GameLib.Collections.QuadTree;
using GameLib.Components;
using GameLib.Constants;
using GameLib.Structures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLib.Static
{
    /// <summary>
    /// Менеджер сущностей в пространстве
    /// </summary>
    public static class SpaceEntityManager
    {
        private static readonly object _lockObject = new object();
        private static readonly QuadTree<Position> _quadTree = new QuadTree<Position>(
            splitCount: 50, 
            depthLimit: 20, 
            region: new Quad(
                min: new Point2d(SectorSize.StartPozitionX, SectorSize.StartPozitionY), 
                max: new Point2d(SectorSize.StartPozitionX + SectorSize.Width, SectorSize.StartPozitionY + SectorSize.Height)));

        /// <summary>
        /// Добавить сущность или обновить позицию сущности
        /// </summary>
        /// <param name="position"></param>
        public static void AddOrUpdatePosition(Position position)
        {
            lock (_lockObject)
            {
                _quadTree.AddOrUpdate(position, new Point2d((long)position.X, (long)position.Y));
            }
        }

        /// <summary>
        /// Удалить сущность
        /// </summary>
        public static void Remove(Position position)
        {
            lock (_lockObject)
            {
                _quadTree.Remove(position);
            }
        }

        /// <summary>
        /// Получить идентификаторы сущностей, которые находятся в радиусе
        /// </summary>
        public static List<Guid> GetIdEntitesInRadius(Position position, long radius)
        {
            lock (_lockObject)
            {
                _quadTree.FindСollisionsForCircle(
                new Circle(
                    new Point2d(
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
//1) Сущность пропадает из пространства (к примеру произошла стыковка корабля со станцией) 
//2) Сущность уничтожена
//3) Сущность переместилась
//4) Уйти от статики
//5) Потокобезопасность
