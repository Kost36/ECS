using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibForPerformanceTests.ForTestPerformance
{
    #region Структуры
    public struct GroupStruct
    {
        public PozitionStruct PozitionStruct;
        public SpeedStruct SpeedStruct;
    }
    public struct PozitionStruct
    {
        public float X;
        public float Y;
        public float Z;
    }
    public struct SpeedStruct
    {
        public SpeedStruct(float x, float y, float z)
        {
            DX = 0.1f;
            DY = 0.1f;
            DZ = 0.1f;
        }
        public float DX;
        public float DY;
        public float DZ;
    }
    #endregion

    #region Классы
    public class GroupClass
    {
        public PozitionTest PozitionTest;
        public SpeedTest SpeedTest;
    }
    public class PozitionTest
    {
        public float X;
        public float Y;
        public float Z;
    }
    public class SpeedTest
    {
        public float DX = 0.1f;
        public float DY = 0.1f;
        public float DZ = 0.1f;
    }
    #endregion

    #region Тест Call

    public interface IAction
    {
        void Action(PozitionTest pozitionTest, SpeedTest speedTest);
    }
    public abstract class ActionAbstract
    {
        public abstract void Action(PozitionTest pozitionTest, SpeedTest speedTest);
    }
    public class ActionVirtual
    {
        public virtual void Action(PozitionTest pozitionTest, SpeedTest speedTest)
        {

        }
    }
    public abstract class ActionAbstractFromIAction : IAction
    {
        public abstract void Action(PozitionTest pozitionTest, SpeedTest speedTest);
    }


    public class ActionClear
    {
        public void Action(PozitionTest pozitionTest, SpeedTest speedTest)
        {
            pozitionTest.X = pozitionTest.X + speedTest.DX;
            pozitionTest.Y = pozitionTest.Y + speedTest.DY;
            pozitionTest.Z = pozitionTest.Z + speedTest.DZ;
        }
    }
    public class ActionFromIAction : IAction
    {
        public void Action(PozitionTest pozitionTest, SpeedTest speedTest)
        {
            pozitionTest.X = pozitionTest.X + speedTest.DX;
            pozitionTest.Y = pozitionTest.Y + speedTest.DY;
            pozitionTest.Z = pozitionTest.Z + speedTest.DZ;
        }
    }
    public class ActionFromAbstract : ActionAbstract
    {
        public override void Action(PozitionTest pozitionTest, SpeedTest speedTest)
        {
            pozitionTest.X = pozitionTest.X + speedTest.DX;
            pozitionTest.Y = pozitionTest.Y + speedTest.DY;
            pozitionTest.Z = pozitionTest.Z + speedTest.DZ;
        }
    }
    public class ActionFromActionVirtual : ActionVirtual
    {
        public override void Action(PozitionTest pozitionTest, SpeedTest speedTest)
        {
            pozitionTest.X = pozitionTest.X + speedTest.DX;
            pozitionTest.Y = pozitionTest.Y + speedTest.DY;
            pozitionTest.Z = pozitionTest.Z + speedTest.DZ;
        }
    }
    public class ActionFromActionAbstractFromIAction : ActionAbstractFromIAction
    {
        public override void Action(PozitionTest pozitionTest, SpeedTest speedTest)
        {
            pozitionTest.X = pozitionTest.X + speedTest.DX;
            pozitionTest.Y = pozitionTest.Y + speedTest.DY;
            pozitionTest.Z = pozitionTest.Z + speedTest.DZ;
        }
    }
    #endregion
}