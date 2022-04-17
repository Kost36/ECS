using ECSCore.BaseObjects;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using System.Collections.Generic;

namespace GameLib.Components.WorkFlow
{
    public class Ore : ComponentBase
    {
        public int Count;
        public int Volume;
    }

    public class Warehouse : ComponentBase
    {
        public int Volume;
        public int VolumeMax;
    }

    public class WarehouseOverFlow : ComponentBase
    {

    }

    public class Enargy : ComponentBase
    {
        public int Count;
        public int Volume;
    }
    
    public class Metal : ComponentBase
    {
        public int Count;
        public int Volume;
    }

    public class Body : ComponentBase
    {
        public int Count;
        public int Volume;
    }

    public class ProdactionMetal : ComponentBase
    {
        public float Percent;
        public float Performance;
        public int CountEnargyRaw;
        public int CountOreRaw;
        public int CountMetalProd;
    }

    public class ProdactionMetalSystem : SystemExistComponents<ProdactionMetal, Enargy, Ore, Metal>, ISystemActionAdd, ISystemAction, ISystemActionRemove
    {
        public override void Action(int entityId, ProdactionMetal prodactionMetal, Enargy enargy, Ore ore, Metal metal, float _)
        {
            if (enargy.Count < prodactionMetal.CountEnargyRaw)
            {
                return;
            }
            if (ore.Count < prodactionMetal.CountOreRaw)
            {
                return;
            }
            if (prodactionMetal.Percent < 100)
            {
                prodactionMetal.Percent += prodactionMetal.Performance;
            }
            if (prodactionMetal.Percent > 100)
            {
                prodactionMetal.Percent -= 100;
                if (IECS.GetComponent(entityId, out Warehouse warehouse))
                {
                    if(warehouse.VolumeMax - warehouse.Volume > metal.Volume * metal.Count)
                    {
                        metal.Count += prodactionMetal.CountMetalProd;
                        enargy.Count += prodactionMetal.CountEnargyRaw;
                        ore.Count += prodactionMetal.CountOreRaw;
                    }
                    else
                    {
                        if (!IECS.GetComponent(entityId, out WarehouseOverFlow _))
                        {
                            IECS.AddComponent(new WarehouseOverFlow { Id = entityId });
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Коллекция продуктов
    /// </summary>
    public static class ProductCollectiont
    {
        /// <summary>
        /// Коллекция компонентов связанных с информацией о продекте
        /// </summary>
        private static Dictionary<ComponentBase, ProductInfo> KeyValuePairs = new Dictionary<ComponentBase, ProductInfo>() 
        {
            { new Enargy(), new ProductInfo("Enargy", 1, 1, 60, 100, new Dictionary<ComponentBase, int>()) },
            { new Ore(),    new ProductInfo("Ore",    1, 1, 60, 100, new Dictionary<ComponentBase, int>()) },
            { new Metal(),  new ProductInfo("Metal",  1, 1, 60, 100, new Dictionary<ComponentBase, int>() {
                { new Enargy(), 100 }, 
                { new Ore(),    50  } })},
            { new Body(),   new ProductInfo("Body",   1, 1, 60, 100, new Dictionary<ComponentBase, int>() {
                { new Enargy(), 100 },
                { new Metal(),  50  } })},
        };

        /// <summary>
        /// Получить информацию о продукте
        /// </summary>
        /// <param name="componentBase"></param>
        /// <param name="productInfo"></param>
        /// <returns></returns>
        public static bool TryGetProductInfo(ComponentBase componentBase, out ProductInfo productInfo)
        {
            return KeyValuePairs.TryGetValue(componentBase, out productInfo);
        }

        /// <summary>
        /// Добавить информацию о продукте
        /// </summary>
        /// <param name="componentBase"></param>
        /// <param name="productInfo"></param>
        public static void Add(ComponentBase componentBase, ProductInfo productInfo)
        {
            KeyValuePairs.Add(componentBase, productInfo);
        }
    }

    /// <summary>
    /// Информация о продукте
    /// </summary>
    public class ProductInfo
    {
        public ProductInfo(string name, float weight, float volume, int partyProductionTime, int partyProductionCount, Dictionary<ComponentBase, int> rawProductInfoCountPairs)
        {
            Name = name;
            Weight = weight;
            Volume = volume;
            PartyProductionTime = partyProductionTime;
            PartyProductionCount = partyProductionCount;
            RawProductInfoCountPairs = rawProductInfoCountPairs;
        }

        /// <summary>
        /// Название
        /// </summary>
        public string Name;
        /// <summary>
        /// Вес
        /// </summary>
        public float Weight;
        /// <summary>
        /// Объем
        /// </summary>
        public float Volume;

        /// <summary>
        /// Время производства партии
        /// </summary>
        public int PartyProductionTime;
        /// <summary>
        /// Количество в партии
        /// </summary>
        public int PartyProductionCount;
        /// <summary>
        /// Коллекция сырья.
        /// Материал - необходимое колличество для производства партии
        /// </summary>
        public Dictionary<ComponentBase, int> RawProductInfoCountPairs;
        /// <summary>
        /// Материалы
        /// </summary>
        public IEnumerable<ComponentBase> Raws { get => RawProductInfoCountPairs.Keys; }
    }
}