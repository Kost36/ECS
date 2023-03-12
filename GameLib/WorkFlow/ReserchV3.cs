using ECSCore.Attributes;
using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;

namespace GameLib.WorkFlow
{
    #region Components
    public abstract class ProductionModule1 : ComponentBase
    {
        public bool Enable;

        public int CountProductOfCycle;
        public int CountRaw1OfCycle;

        public int TimeCycleInSec;
        public float CycleCompletionPercentage;
    }
    public abstract class ProductionModule2 : ComponentBase
    {
        public bool Enable;

        public int CountProductOfCycle;

        public int CountRaw1OfCycle;
        public int CountRaw2OfCycle;

        public int TimeCycleInSec;
        public float CycleCompletionPercentage;
    }
    public abstract class ProductionModule3 : ComponentBase
    {
        public bool Enable;

        public int CountProductOfCycle;

        public int CountRaw1OfCycle;
        public int CountRaw2OfCycle;
        public int CountRaw3OfCycle;

        public int TimeCycleInSec;
        public float CycleCompletionPercentage;
    }
    public abstract class ProductionModule4 : ComponentBase
    {
        public bool Enable;

        public int CountProductOfCycle;

        public int CountRaw1OfCycle;
        public int CountRaw2OfCycle;
        public int CountRaw3OfCycle;
        public int CountRaw4OfCycle;

        public int TimeCycleInSec;
        public float CycleCompletionPercentage;
    }

    public class WarehouseProductionModulRaw1 : ComponentBase
    {
        public int Volume;
        public int VolumeMax;
        public int PercentFillingRaws;

        public int CountProduct;

        public int CountRaw1;
        public int MaxCountRaw1;
    }
    public class WarehouseProductionModulRaw2 : ComponentBase
    {
        public int Volume;
        public int VolumeMax;
        public int PercentFillingRaws;

        public int CountProduct;

        public int CountRaw1;
        public int MaxCountRaw1;
        public int CountRaw2;
        public int MaxCountRaw2;
    }
    public class WarehouseProductionModulRaw3 : ComponentBase
    {
        public int Volume;
        public int VolumeMax;
        public int PercentFillingRaws;

        public int CountProduct;

        public int CountRaw1;
        public int MaxCountRaw1;
        public int CountRaw2;
        public int MaxCountRaw2;
        public int CountRaw3;
        public int MaxCountRaw3;
    }
    public class WarehouseProductionModulRaw4 : ComponentBase
    {
        public int Volume;
        public int VolumeMax;
        public int PercentFillingRaws;

        public int CountProduct;

        public int CountRaw1;
        public int MaxCountRaw1;
        public int CountRaw2;
        public int MaxCountRaw2;
        public int CountRaw3;
        public int MaxCountRaw3;
        public int CountRaw4;
        public int MaxCountRaw4;
    }
    #endregion

    #region Systems
    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(50)]
    [AttributeSystemEnable]
    public class ProductionSystem1 : SystemExistComponents<ProductionModule1, WarehouseProductionModulRaw1>, ISystemAction
    {
        public override void Action(int entityId, ProductionModule1 productionModule, WarehouseProductionModulRaw1 warehouseProductionModul, float deltatime)
        {
            if (productionModule.Enable)
            {
                if (warehouseProductionModul.CountRaw1 >= productionModule.CountRaw1OfCycle)
                {
                    //Производительность = deltatime / (цикл производства(сек) / 100%)  <= деление на 100, заменено умножением на 0.01
                    var percent = deltatime / (productionModule.TimeCycleInSec * 0.01f);
                    productionModule.CycleCompletionPercentage += percent;

                    if (productionModule.CycleCompletionPercentage >= 100)
                    {
                        productionModule.CycleCompletionPercentage -= 100;
                        warehouseProductionModul.CountRaw1 -= productionModule.CountRaw1OfCycle;
                        warehouseProductionModul.CountProduct += productionModule.CountProductOfCycle;
                    }
                } //Если сырья хватает
            } //Если модуль работает
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(50)]
    [AttributeSystemEnable]
    public class ProductionSystem2 : SystemExistComponents<ProductionModule2, WarehouseProductionModulRaw2>, ISystemAction
    {
        public override void Action(int entityId, ProductionModule2 productionModule, WarehouseProductionModulRaw2 warehouseProductionModul, float deltatime)
        {
            if (productionModule.Enable)
            {
                if (warehouseProductionModul.CountRaw1 >= productionModule.CountRaw1OfCycle
                    && warehouseProductionModul.CountRaw2 >= productionModule.CountRaw2OfCycle)
                {
                    //Производительность = deltatime / (цикл производства(сек) / 100%)  <= деление на 100, заменено умножением на 0.01
                    var percent = deltatime / (productionModule.TimeCycleInSec * 0.01f);
                    productionModule.CycleCompletionPercentage += percent;

                    if (productionModule.CycleCompletionPercentage >= 100)
                    {
                        productionModule.CycleCompletionPercentage -= 100;
                        warehouseProductionModul.CountRaw1 -= productionModule.CountRaw1OfCycle;
                        warehouseProductionModul.CountRaw2 -= productionModule.CountRaw2OfCycle;
                        warehouseProductionModul.CountProduct += productionModule.CountProductOfCycle;
                    }
                } //Если сырья хватает
            } //Если модуль работает
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(50)]
    [AttributeSystemEnable]
    public class ProductionSystem3 : SystemExistComponents<ProductionModule3, WarehouseProductionModulRaw3>, ISystemAction
    {
        public override void Action(int entityId, ProductionModule3 productionModule, WarehouseProductionModulRaw3 warehouseProductionModul, float deltatime)
        {
            if (productionModule.Enable)
            {
                if (warehouseProductionModul.CountRaw1 >= productionModule.CountRaw1OfCycle
                    && warehouseProductionModul.CountRaw2 >= productionModule.CountRaw2OfCycle
                    && warehouseProductionModul.CountRaw3 >= productionModule.CountRaw3OfCycle)
                {
                    //Производительность = deltatime / (цикл производства(сек) / 100%)  <= деление на 100, заменено умножением на 0.01
                    var percent = deltatime / (productionModule.TimeCycleInSec * 0.01f);
                    productionModule.CycleCompletionPercentage += percent;

                    if (productionModule.CycleCompletionPercentage >= 100)
                    {
                        productionModule.CycleCompletionPercentage -= 100;
                        warehouseProductionModul.CountRaw1 -= productionModule.CountRaw1OfCycle;
                        warehouseProductionModul.CountRaw2 -= productionModule.CountRaw2OfCycle;
                        warehouseProductionModul.CountRaw3 -= productionModule.CountRaw3OfCycle;
                        warehouseProductionModul.CountProduct += productionModule.CountProductOfCycle;
                    }
                } //Если сырья хватает
            } //Если модуль работает
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(50)]
    [AttributeSystemEnable]
    public class ProductionSystem4 : SystemExistComponents<ProductionModule4, WarehouseProductionModulRaw4>, ISystemAction
    {
        public override void Action(int entityId, ProductionModule4 productionModule, WarehouseProductionModulRaw4 warehouseProductionModul, float deltatime)
        {
            if (productionModule.Enable)
            {
                if (warehouseProductionModul.CountRaw1 >= productionModule.CountRaw1OfCycle
                    && warehouseProductionModul.CountRaw2 >= productionModule.CountRaw2OfCycle
                    && warehouseProductionModul.CountRaw3 >= productionModule.CountRaw3OfCycle
                    && warehouseProductionModul.CountRaw4 >= productionModule.CountRaw4OfCycle)
                {
                    //Производительность = deltatime / (цикл производства(сек) / 100%)  <= деление на 100, заменено умножением на 0.01
                    var percent = deltatime / (productionModule.TimeCycleInSec * 0.01f);
                    productionModule.CycleCompletionPercentage += percent;

                    if (productionModule.CycleCompletionPercentage >= 100)
                    {
                        productionModule.CycleCompletionPercentage -= 100;
                        warehouseProductionModul.CountRaw1 -= productionModule.CountRaw1OfCycle;
                        warehouseProductionModul.CountRaw2 -= productionModule.CountRaw2OfCycle;
                        warehouseProductionModul.CountRaw3 -= productionModule.CountRaw3OfCycle;
                        warehouseProductionModul.CountRaw4 -= productionModule.CountRaw4OfCycle;
                        warehouseProductionModul.CountProduct += productionModule.CountProductOfCycle;
                    }
                } //Если сырья хватает
            } //Если модуль работает
        }
    }
    #endregion
}