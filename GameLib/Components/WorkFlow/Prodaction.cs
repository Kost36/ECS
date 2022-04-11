using ECSCore.BaseObjects;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
