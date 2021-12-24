using ECSCore.BaseObjects;
using ECSCore.Filters;
using ECSCore.Interfaces;
using Game.Components.ObjectStates;
using Game.Components.Propertys;
using Game.Components.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Filters
{
    public class FilterMove : Filter<Pozition, Speed> { }
    public class FilterControlSpeed : Filter<Way, Speed, SpeedSV> { }
    public class FilterControlWay : Filter<PozitionSV, Pozition, Way> { }
    public class FilterAccelerate : Filter<SpeedSV, Acceleration, Speed> { }
}