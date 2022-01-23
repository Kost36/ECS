using ECSCore.BaseObjects;
using GameLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Components.ShipModules
{
    /// <summary>
    /// Модули корабля
    /// </summary>
    public abstract class ShipModule : ComponentBase
    {
        #region Параметры
        /// <summary>
        /// Размер модуля
        /// </summary>
        public ShipModuleSize ShipModuleSize;
        /// <summary>
        /// Занимаемый объем модулем;
        /// М.куб
        /// </summary>
        public float Volume;
        /// <summary>
        /// Вес модуля;
        /// Кг.
        /// </summary>
        public float Weight;
        #endregion 

        #region Типы модулей
        /// <summary>
        /// Двигатели полета
        /// </summary>
        public abstract class ShipModuleEngine : ShipModule
        {
            #region Параметры
            /// <summary>
            /// Максимальная скорость полета;
            /// М/Сек
            /// </summary>
            public float MaxSpeed;
            /// <summary>
            /// Ускорение / Замедление;
            /// dм/сек
            /// </summary>
            public float Accelerate;
            /// <summary>
            /// Использование энергии, при ускорении / замедлении;
            /// 
            /// </summary>
            public float UseEnargyOnAccelerate;
            /// <summary>
            /// Скорость вращения;
            /// %
            /// </summary>
            public float RotationSpeed;
            /// <summary>
            /// Использование энергии, при вращении
            /// </summary>
            public float UseEnargyOnRotation;
            #endregion

            #region Модули

            #region Модули размера S
            public abstract class ShipModuleEngineS : ShipModuleEngine
            {

                #region Конкретные модули
                public class ShipModuleEngineSDefault : ShipModuleEngineS
                {
                    public ShipModuleEngineSDefault()
                    {
                        ShipModuleSize = ShipModuleSize.S;
                        Volume = 1;
                        Weight = 50;

                        MaxSpeed = 50;
                        Accelerate = 5;
                        UseEnargyOnAccelerate = 1;
                        RotationSpeed = 5;
                        UseEnargyOnRotation = 1;
                    }
                }
                #endregion
            }
            #endregion

            #region Модули размера M
            public abstract class ShipModuleEngineM : ShipModuleEngine
            {

                #region Конкретные модули
                public class ShipModuleEngineMDefault : ShipModuleEngineM
                {
                    public ShipModuleEngineMDefault()
                    {
                        ShipModuleSize = ShipModuleSize.M;
                        Volume = 5;
                        Weight = 250;

                        MaxSpeed = 100;
                        Accelerate = 10;
                        UseEnargyOnAccelerate = 2;
                        RotationSpeed = 10;
                        UseEnargyOnRotation = 2;
                    }
                }
                #endregion
            }

            #endregion

            #region Модули размера L

            public abstract class ShipModuleEngineL : ShipModuleEngine
            {

                #region Конкретные модули
                public class ShipModuleEngineLDefault : ShipModuleEngineL
                {
                    public ShipModuleEngineLDefault()
                    {
                        ShipModuleSize = ShipModuleSize.L;
                        Volume = 10;
                        Weight = 500;

                        MaxSpeed = 90;
                        Accelerate = 8;
                        UseEnargyOnAccelerate = 3;
                        RotationSpeed = 8;
                        UseEnargyOnRotation = 3;
                    }
                }
                #endregion
            }

            #endregion

            #region Модули размера XL

            public abstract class ShipModuleEngineXL : ShipModuleEngine
            {

                #region Конкретные модули
                public class ShipModuleEngineXLDefault : ShipModuleEngineXL
                {
                    public ShipModuleEngineXLDefault()
                    {
                        ShipModuleSize = ShipModuleSize.XL;
                        Volume = 100;
                        Weight = 1500;

                        MaxSpeed = 40;
                        Accelerate = 2;
                        UseEnargyOnAccelerate = 10;
                        RotationSpeed = 2;
                        UseEnargyOnRotation = 10;
                    }
                }
                #endregion
            }

            #endregion

            #endregion
        }

        /// <summary>
        /// Энергетическая установка
        /// </summary>
        public abstract class ShipPowerUnit : ShipModule
        {

        }

        /// <summary>
        /// Щиты
        /// </summary>
        public abstract class ShipShild : ShipModule
        {

        }

        /// <summary>
        /// Оружие
        /// </summary>
        public abstract class ShipGun : ShipModule
        {

        }

        /// <summary>
        /// Турели
        /// </summary>
        public abstract class ShipTurret : ShipModule
        {

        }
        #endregion
    }
}