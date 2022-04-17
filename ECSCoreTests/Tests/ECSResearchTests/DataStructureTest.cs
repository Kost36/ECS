using LibForPerformanceTests.ForTestPerformance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECSCoreLibTests.Tests.ECSResearchTests
{
    [TestClass()]
    public class TestPerformanceDotNet
    {
        static int ThreadNumb = 0;
        public List<string> results = new List<string>();
        //Тест 00: 2 списков структур из 1000000 объектов циклом for Ср: 50,2 Макс 79,0 Мин 31,6 Память: 55мб
        //Тест 01: 2 массива структур из 1000000 объектов циклом for Ср: 25,6 Макс 49,6 Мин 15,2 Память: 55мб
        //Тест 02: 1 группа структур из 1000000 объектов циклом for Ср: 23,2 Макс 40,0 Мин 12,4 Память: 56мб
        //Тест 03: 1 группа структур из 1000000 объектов циклом for Ср: 17.8 Макс 38,3 Мин 12,7 Память: 183мб проверка локальности данных

        //Тест 04: 2 списков класса из 1000000 объектов циклом for Ср: 52,8 Макс 90,5 Мин 39,2 Память: 113мб
        //Тест 05: 2 массива класса из 1000000 объектов циклом for Ср: 26,2 Макс 41,1 Мин 15,8 Память: 113мб
        //Тест 06: 1 группа класса из 1000000 объектов циклом for Ср: 31,4 Макс 52,2 Мин 22,8 Память: 138мб
        //Тест 07: 1 группа класса из 1000000 объектов циклом for Ср: 84 Макс 133 Мин 70 Память: 1200мб проверка локальности данных
        //Тест 08: 1 словарь класса из 1000000 объектов циклом for Ср: 164 Макс 207 Мин 171 Память: 164мб
        //Тест 09: 1 словарь класса из 1000000 объектов циклом forach Ср: 34 Макс 52,5 Мин 23 Память: 165мб
        //Тест 10: 1 словарь класса из 1000000 объектов циклом forach Ср: 73,5 Макс 128,8 Мин 58,5 Память: мб проверка локальности данных (Без уборки мусора)
        //Тест 11: 1 словарь класса из 1000000 объектов циклом forach Ср: 34  Макс 52,5 Мин 23 Память: мб проверка локальности данных (С уборкой мусора) (в словаре нету локальности данных!!!)

        //Тесты 12-16 на основе теста 09: 1 словарь класса из 1000000 объектов циклом forach                        Ср: 34,8 Макс 54,0 Мин 23,0 Память: 164мб
        //Тест 12: 1 словарь класса из 1000000 объектов циклом forach через чистый вызов метода                     Ср: 37,1 Макс 60,0 Мин 24,5 Память: 164мб
        //Тест 13: 1 словарь класса из 1000000 объектов циклом forach через абстрактный вызов метода                Ср: 40,3 Макс 60,6 Мин 25,1 Память: 164мб
        //Тест 14: 1 словарь класса из 1000000 объектов циклом forach через интерфейсный вызов метода               Ср: 38,7 Макс 60,2 Мин 23,6 Память: 164мб
        //Тест 15: 1 словарь класса из 1000000 объектов циклом forach через виртуальный вызов метода                Ср: 39,5 Макс 58,7 Мин 25,0 Память: 164мб
        //Тест 16: 1 словарь класса из 1000000 объектов циклом forach через абстрактный - интерфейсный вызов метода Ср: 38,5 Макс 61,1 Мин 24,2 Память: 164мб

        //Тест 17: 1 словарь класса из 1000000 объектов циклом forach через абстрактный - интерфейсный вызов метода в 8 потоках       Ср: 124,6 Макс 217,0 Мин 85,1 Память: 300мб
        //Тест 18: 1 словарь класса из 1000000 объектов циклом forach через абстрактный - интерфейсный вызов метода в ParallelForache Ср:  53,7 Макс 107,8 Мин 34,7 Память: 169мб
        //Тест 19:                                                                                                                                                           Память: 300мб
        //         Поток 1: 1 словарь класса из 1000000 объектов циклом forach через абстрактный - интерфейсный вызов метода в ParallelForache Ср:  37,7 Макс 70,8 Мин 25,7  
        //         Поток 2: 1 словарь класса из 1000000 объектов циклом forach через абстрактный - интерфейсный вызов метода в Потоках Ср:  36,9 Макс 72,7 Мин 25,7 

        //Тест 20: Потоков 20: 1 словарь класса из 1000000 объектов циклом forach через абстрактный - интерфейсный вызов метода в 20 Потоках                                Ср:  150 Макс 320 Мин 35,7 Память: 2,7гб сумарное время теста: 56,4 в сумме 10000000 объектов
        //Сравнение с непараллельной обработкой тогоже кол-ва значений: 1 словарь класса из 1000000 объектов циклом forach через абстрактный - интерфейсный вызов метода в  Ср:  260 Макс 301 Мин 247  Память: 1,3гб сумарное время теста: 53,7 в сумме 10000000 объектов

        //Тест распараллеливания одной системы на несколько потоков

        //60FPS -> 16.6 мс
        //30FPS -> 33.3 мс

        [TestMethod()]
        public void Test_00_Test_For_List_Structure()
        {
            int count = 1000000;

            //Создали
            List<PozitionStruct> list1 = new List<PozitionStruct>(count); //11.5 мб
            List<SpeedStruct> list2 = new List<SpeedStruct>(count); //11.5 мб

            //Заполнили
            for (int i = 0; i < count; i++)
            {
                list1.Add(new PozitionStruct());
                list2.Add(new SpeedStruct(1, 1, 1));
            }

            //Тест
            Stopwatch stopwatch = new Stopwatch();
            float cnt = 1;
            float cntMax = 100;
            float sum = 0;
            float aver = 0;
            float max = 0;
            float min = 100000;
            while (cnt <= cntMax)
            {
                stopwatch.Restart();
                float x;
                float y;
                float z;
                for (int i = 0; i < list1.Count; i++)
                {
                    x = list1[i].X + list2[i].DX;
                    y = list1[i].Y + list2[i].DY;
                    z = list1[i].Z + list2[i].DZ;
                    list1[i] = new PozitionStruct() { X = x, Y = y, Z = z };
                }
                stopwatch.Stop();
                float time = stopwatch.ElapsedTicks / (float)TimeSpan.TicksPerMillisecond;
                Debug.WriteLine($"Обработка: {count} шт. произведена за {time} мс");
                if (time > max) { max = time; }
                if (time < min) { min = time; }
                sum = sum + time;
                aver = sum / cnt;
                Debug.WriteLine($"Среднее: {aver} Максимальное: {max} Минимальное: {min}");
                Thread.Sleep(200);
                cnt++;
            }
        }
        [TestMethod()]
        public void Test_01_Test_For_Array_Structure()
        {
            int count = 1000000;

            //Создали
            PozitionStruct[] list1 = new PozitionStruct[count]; //11.5 мб
            SpeedStruct[] list2 = new SpeedStruct[count]; //11.5 мб

            //Заполнили
            for (int i = 0; i < count; i++)
            {
                list1[i] = new PozitionStruct();
                list2[i] = new SpeedStruct(1, 1, 1);
            }

            //Тест
            Stopwatch stopwatch = new Stopwatch();
            float cnt = 1;
            float cntMax = 100;
            float sum = 0;
            float aver = 0;
            float max = 0;
            float min = 100000;
            while (cnt <= cntMax)
            {
                stopwatch.Restart();
                float x;
                float y;
                float z;
                for (int i = 0; i < list1.Length; i++)
                {
                    x = list1[i].X + list2[i].DX;
                    y = list1[i].Y + list2[i].DY;
                    z = list1[i].Z + list2[i].DZ;
                    list1[i] = new PozitionStruct() { X = x, Y = y, Z = z };
                }
                stopwatch.Stop();
                float time = stopwatch.ElapsedTicks / (float)TimeSpan.TicksPerMillisecond;
                Debug.WriteLine($"Обработка: {count} шт. произведена за {time} мс");
                if (time > max) { max = time; }
                if (time < min) { min = time; }
                sum = sum + time;
                aver = sum / cnt;
                Debug.WriteLine($"Среднее: {aver} Максимальное: {max} Минимальное: {min}");
                Thread.Sleep(200);
                cnt++;
            }
        }
        [TestMethod()]
        public void Test_02_Test_For_Array_GroupStructure()
        {
            int count = 1000000;

            //Создали
            GroupStruct[] list1 = new GroupStruct[count]; //23 мб

            //Заполнили
            for (int i = 0; i < count; i++)
            {
                list1[i].PozitionStruct = new PozitionStruct();
                list1[i].SpeedStruct = new SpeedStruct(1, 1, 1);
            }

            //Тест
            Stopwatch stopwatch = new Stopwatch();
            float cnt = 1;
            float cntMax = 100;
            float sum = 0;
            float aver = 0;
            float max = 0;
            float min = 100000;
            while (cnt <= cntMax)
            {
                stopwatch.Restart();
                float x;
                float y;
                float z;
                for (int i = 0; i < list1.Length; i++)
                {
                    list1[i].PozitionStruct.X = list1[i].PozitionStruct.X + list1[i].SpeedStruct.DX;
                    list1[i].PozitionStruct.Y = list1[i].PozitionStruct.Y + list1[i].SpeedStruct.DY;
                    list1[i].PozitionStruct.Z = list1[i].PozitionStruct.Z + list1[i].SpeedStruct.DZ;
                }
                stopwatch.Stop();
                float time = stopwatch.ElapsedTicks / (float)TimeSpan.TicksPerMillisecond;
                Debug.WriteLine($"Обработка: {count} шт. произведена за {time} мс");
                if (time > max) { max = time; }
                if (time < min) { min = time; }
                sum = sum + time;
                aver = sum / cnt;
                Debug.WriteLine($"Среднее: {aver} Максимальное: {max} Минимальное: {min}");
                Thread.Sleep(200);
                cnt++;
            }
        }
        [TestMethod()]
        public void Test_03_Test_For_Array_GroupStructure_locality()
        {
            int count = 1000000;

            //Создали
            GroupStruct[] list1 = new GroupStruct[count]; //23 мб
            GroupStruct[] list2 = new GroupStruct[count]; //23 мб
            GroupStruct[] list3 = new GroupStruct[count]; //23 мб
            GroupStruct[] list4 = new GroupStruct[count]; //23 мб
            GroupStruct[] list5 = new GroupStruct[count]; //23 мб
            GroupStruct[] list6 = new GroupStruct[count]; //23 мб

            //Заполнили
            for (int i = 0; i < count; i++)
            {
                list1[i].PozitionStruct = new PozitionStruct();
                list1[i].SpeedStruct = new SpeedStruct(1, 1, 1);
                list2[i].PozitionStruct = new PozitionStruct();
                list2[i].SpeedStruct = new SpeedStruct(1, 1, 1);
                list3[i].PozitionStruct = new PozitionStruct();
                list3[i].SpeedStruct = new SpeedStruct(1, 1, 1);
                list4[i].PozitionStruct = new PozitionStruct();
                list4[i].SpeedStruct = new SpeedStruct(1, 1, 1);
                list5[i].PozitionStruct = new PozitionStruct();
                list5[i].SpeedStruct = new SpeedStruct(1, 1, 1);
                list6[i].PozitionStruct = new PozitionStruct();
                list6[i].SpeedStruct = new SpeedStruct(1, 1, 1);
            }

            //Тест
            Stopwatch stopwatch = new Stopwatch();
            float cnt = 1;
            float cntMax = 100;
            float sum = 0;
            float aver = 0;
            float max = 0;
            float min = 100000;
            while (cnt <= cntMax)
            {
                stopwatch.Restart();
                float x;
                float y;
                float z;
                for (int i = 0; i < list1.Length; i++)
                {
                    list1[i].PozitionStruct.X = list1[i].PozitionStruct.X + list1[i].SpeedStruct.DX;
                    list1[i].PozitionStruct.Y = list1[i].PozitionStruct.Y + list1[i].SpeedStruct.DY;
                    list1[i].PozitionStruct.Z = list1[i].PozitionStruct.Z + list1[i].SpeedStruct.DZ;
                }
                stopwatch.Stop();
                float time = stopwatch.ElapsedTicks / (float)TimeSpan.TicksPerMillisecond;
                Debug.WriteLine($"Обработка: {count} шт. произведена за {time} мс");
                if (time > max) { max = time; }
                if (time < min) { min = time; }
                sum = sum + time;
                aver = sum / cnt;
                Debug.WriteLine($"Среднее: {aver} Максимальное: {max} Минимальное: {min}");
                Thread.Sleep(200);
                cnt++;
            }
        }

        [TestMethod()]
        public void Test_04_Test_For_List_Class()
        {
            int count = 1000000;

            //Создали
            List<PozitionTest> list1 = new List<PozitionTest>(count); // мб
            List<SpeedTest> list2 = new List<SpeedTest>(count); // мб

            //Заполнили
            for (int i = 0; i < count; i++)
            {
                list1.Add(new PozitionTest());
                list2.Add(new SpeedTest());
            }

            //Тест
            Stopwatch stopwatch = new Stopwatch();
            float cnt = 1;
            float cntMax = 100;
            float sum = 0;
            float aver = 0;
            float max = 0;
            float min = 100000;
            while (cnt <= cntMax)
            {
                stopwatch.Restart();
                float x;
                float y;
                float z;
                for (int i = 0; i < list1.Count; i++)
                {
                    list1[i].X = list1[i].X + list2[i].DX;
                    list1[i].Y = list1[i].Y + list2[i].DY;
                    list1[i].Z = list1[i].Z + list2[i].DZ;
                }
                stopwatch.Stop();
                float time = stopwatch.ElapsedTicks / (float)TimeSpan.TicksPerMillisecond;
                Debug.WriteLine($"Обработка: {count} шт. произведена за {time} мс");
                if (time > max) { max = time; }
                if (time < min) { min = time; }
                sum = sum + time;
                aver = sum / cnt;
                Debug.WriteLine($"Среднее: {aver} Максимальное: {max} Минимальное: {min}");
                Thread.Sleep(200);
                cnt++;
            }
        }
        [TestMethod()]
        public void Test_05_Test_For_Array_Class()
        {
            int count = 1000000;

            //Создали
            PozitionTest[] list1 = new PozitionTest[count]; // мб
            SpeedTest[] list2 = new SpeedTest[count]; // мб

            //Заполнили
            for (int i = 0; i < count; i++)
            {
                list1[i] = new PozitionTest();
                list2[i] = new SpeedTest();
            }

            //Тест
            Stopwatch stopwatch = new Stopwatch();
            float cnt = 1;
            float cntMax = 100;
            float sum = 0;
            float aver = 0;
            float max = 0;
            float min = 100000;
            while (cnt <= cntMax)
            {
                stopwatch.Restart();
                float x;
                float y;
                float z;
                for (int i = 0; i < list1.Length; i++)
                {
                    list1[i].X = list1[i].X + list2[i].DX;
                    list1[i].Y = list1[i].Y + list2[i].DY;
                    list1[i].Z = list1[i].Z + list2[i].DZ;
                }
                stopwatch.Stop();
                float time = stopwatch.ElapsedTicks / (float)TimeSpan.TicksPerMillisecond;
                Debug.WriteLine($"Обработка: {count} шт. произведена за {time} мс");
                if (time > max) { max = time; }
                if (time < min) { min = time; }
                sum = sum + time;
                aver = sum / cnt;
                Debug.WriteLine($"Среднее: {aver} Максимальное: {max} Минимальное: {min}");
                Thread.Sleep(200);
                cnt++;
            }
        }
        [TestMethod()]
        public void Test_06_Test_For_Array_GroupClass()
        {
            int count = 1000000;

            //Создали
            GroupClass[] list1 = new GroupClass[count]; // мб

            //Заполнили
            for (int i = 0; i < count; i++)
            {
                list1[i] = new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() };
            }

            //Тест
            Stopwatch stopwatch = new Stopwatch();
            float cnt = 1;
            float cntMax = 100;
            float sum = 0;
            float aver = 0;
            float max = 0;
            float min = 100000;
            while (cnt <= cntMax)
            {
                stopwatch.Restart();
                float x;
                float y;
                float z;
                for (int i = 0; i < list1.Length; i++)
                {
                    list1[i].PozitionTest.X = list1[i].PozitionTest.X + list1[i].SpeedTest.DX;
                    list1[i].PozitionTest.Y = list1[i].PozitionTest.Y + list1[i].SpeedTest.DY;
                    list1[i].PozitionTest.Z = list1[i].PozitionTest.Z + list1[i].SpeedTest.DZ;
                }
                stopwatch.Stop();
                float time = stopwatch.ElapsedTicks / (float)TimeSpan.TicksPerMillisecond;
                Debug.WriteLine($"Обработка: {count} шт. произведена за {time} мс");
                if (time > max) { max = time; }
                if (time < min) { min = time; }
                sum = sum + time;
                aver = sum / cnt;
                Debug.WriteLine($"Среднее: {aver} Максимальное: {max} Минимальное: {min}");
                Thread.Sleep(200);
                cnt++;
            }
        }
        [TestMethod()]
        public void Test_07_Test_For_Array_GroupClass_locality()
        {
            int count = 1000000;

            //Создали
            GroupClass[] list1 = new GroupClass[count];
            GroupClass[] list2 = new GroupClass[count];
            GroupClass[] list3 = new GroupClass[count];
            GroupClass[] list4 = new GroupClass[count];
            GroupClass[] list5 = new GroupClass[count];
            GroupClass[] list6 = new GroupClass[count];
            GroupClass[] list7 = new GroupClass[count];
            GroupClass[] list8 = new GroupClass[count];
            GroupClass[] list9 = new GroupClass[count];
            GroupClass[] list10 = new GroupClass[count];
            GroupClass[] list11 = new GroupClass[count];

            //Заполнили
            for (int i = 0; i < count; i++)
            {
                list1[i] = new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() };
                list2[i] = new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() };
                list3[i] = new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() };
                list4[i] = new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() };
                list5[i] = new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() };
                list6[i] = new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() };
                list7[i] = new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() };
                list8[i] = new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() };
                list9[i] = new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() };
                list10[i] = new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() };
                list11[i] = new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() };
            }

            //Тест
            Stopwatch stopwatch = new Stopwatch();
            float cnt = 1;
            float cntMax = 500;
            float sum = 0;
            float aver = 0;
            float max = 0;
            float min = 100000;
            while (cnt <= cntMax)
            {
                stopwatch.Restart();
                float x;
                float y;
                float z;
                for (int i = 0; i < list1.Length; i++)
                {
                    list1[i].PozitionTest.X = list1[i].PozitionTest.X + list1[i].SpeedTest.DX;
                    list1[i].PozitionTest.Y = list1[i].PozitionTest.Y + list1[i].SpeedTest.DY;
                    list1[i].PozitionTest.Z = list1[i].PozitionTest.Z + list1[i].SpeedTest.DZ;
                }
                stopwatch.Stop();
                float time = stopwatch.ElapsedTicks / (float)TimeSpan.TicksPerMillisecond;
                Debug.WriteLine($"Обработка: {count} шт. произведена за {time} мс");
                if (time > max) { max = time; }
                if (time < min) { min = time; }
                sum = sum + time;
                aver = sum / cnt;
                Debug.WriteLine($"Среднее: {aver} Максимальное: {max} Минимальное: {min}");
                Thread.Sleep(200);
                cnt++;

                list2 = null;
                list3 = null;
                list4 = null;
                list5 = null;
                list6 = null;
                list7 = null;
                list8 = null;
                list9 = null;
                list10 = null;
                list11 = null;
            }
        }
        [TestMethod()]
        public void Test_08_Test_For_Dictonary_GroupClass()
        {
            int count = 1000000;

            //Создали
            Dictionary<int, GroupClass> list1 = new Dictionary<int, GroupClass>(count);

            //Заполнили
            for (int i = 0; i < count; i++)
            {
                list1.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
            }

            //Тест
            Stopwatch stopwatch = new Stopwatch();
            float cnt = 1;
            float cntMax = 100;
            float sum = 0;
            float aver = 0;
            float max = 0;
            float min = 100000;
            while (cnt <= cntMax)
            {
                stopwatch.Restart();
                for (int i = 0; i < list1.Count; i++)
                {
                    list1[i].PozitionTest.X = list1[i].PozitionTest.X + list1[i].SpeedTest.DX;
                    list1[i].PozitionTest.Y = list1[i].PozitionTest.Y + list1[i].SpeedTest.DY;
                    list1[i].PozitionTest.Z = list1[i].PozitionTest.Z + list1[i].SpeedTest.DZ;
                }
                stopwatch.Stop();
                float time = stopwatch.ElapsedTicks / (float)TimeSpan.TicksPerMillisecond;
                Debug.WriteLine($"Обработка: {count} шт. произведена за {time} мс");
                if (time > max) { max = time; }
                if (time < min) { min = time; }
                sum = sum + time;
                aver = sum / cnt;
                Debug.WriteLine($"Среднее: {aver} Максимальное: {max} Минимальное: {min}");
                Thread.Sleep(200);
                cnt++;
            }
        }
        [TestMethod()]
        public void Test_09_Test_Forache_Dictonary_GroupClass()
        {
            int count = 1000000;

            //Создали
            Dictionary<int, GroupClass> list1 = new Dictionary<int, GroupClass>(count);

            //Заполнили
            for (int i = 0; i < count; i++)
            {
                list1.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
            }

            //Тест
            Stopwatch stopwatch = new Stopwatch();
            float cnt = 1;
            float cntMax = 100;
            float sum = 0;
            float aver = 0;
            float max = 0;
            float min = 100000;
            while (cnt <= cntMax)
            {
                stopwatch.Restart();
                foreach (GroupClass groupClass in list1.Values)
                {
                    groupClass.PozitionTest.X = groupClass.PozitionTest.X + groupClass.SpeedTest.DX;
                    groupClass.PozitionTest.Y = groupClass.PozitionTest.Y + groupClass.SpeedTest.DY;
                    groupClass.PozitionTest.Z = groupClass.PozitionTest.Z + groupClass.SpeedTest.DZ;
                }
                stopwatch.Stop();
                float time = stopwatch.ElapsedTicks / (float)TimeSpan.TicksPerMillisecond;
                Debug.WriteLine($"Обработка: {count} шт. произведена за {time} мс");
                if (time > max) { max = time; }
                if (time < min) { min = time; }
                sum = sum + time;
                aver = sum / cnt;
                Debug.WriteLine($"Среднее: {aver} Максимальное: {max} Минимальное: {min}");
                Thread.Sleep(200);
                cnt++;
            }
        }
        [TestMethod()]
        public void Test_10_Test_Forache_Dictonary_GroupClass_Locality()
        {
            int count = 1000000;

            //Создали
            Dictionary<int, GroupClass> list1 = new Dictionary<int, GroupClass>(count);
            Dictionary<int, GroupClass> list2 = new Dictionary<int, GroupClass>(count);
            Dictionary<int, GroupClass> list3 = new Dictionary<int, GroupClass>(count);
            Dictionary<int, GroupClass> list4 = new Dictionary<int, GroupClass>(count);
            Dictionary<int, GroupClass> list5 = new Dictionary<int, GroupClass>(count);
            Dictionary<int, GroupClass> list6 = new Dictionary<int, GroupClass>(count);
            Dictionary<int, GroupClass> list7 = new Dictionary<int, GroupClass>(count);
            Dictionary<int, GroupClass> list8 = new Dictionary<int, GroupClass>(count);
            Dictionary<int, GroupClass> list9 = new Dictionary<int, GroupClass>(count);

            //Заполнили
            for (int i = 0; i < count; i++)
            {
                list1.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
                list2.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
                list3.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
                list4.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
                list5.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
                list6.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
                list7.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
                list8.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
                list9.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
            }

            //Тест
            Stopwatch stopwatch = new Stopwatch();
            float cnt = 1;
            float cntMax = 100;
            float sum = 0;
            float aver = 0;
            float max = 0;
            float min = 100000;
            while (cnt <= cntMax)
            {
                stopwatch.Restart();
                float x;
                float y;
                float z;
                foreach (GroupClass groupClass in list1.Values)
                {
                    groupClass.PozitionTest.X = groupClass.PozitionTest.X + groupClass.SpeedTest.DX;
                    groupClass.PozitionTest.Y = groupClass.PozitionTest.Y + groupClass.SpeedTest.DY;
                    groupClass.PozitionTest.Z = groupClass.PozitionTest.Z + groupClass.SpeedTest.DZ;
                }
                stopwatch.Stop();
                float time = stopwatch.ElapsedTicks / (float)TimeSpan.TicksPerMillisecond;
                Debug.WriteLine($"Обработка: {count} шт. произведена за {time} мс");
                if (time > max) { max = time; }
                if (time < min) { min = time; }
                sum = sum + time;
                aver = sum / cnt;
                Debug.WriteLine($"Среднее: {aver} Максимальное: {max} Минимальное: {min}");
                Thread.Sleep(200);
                cnt++;

            }
        }
        [TestMethod()]
        public void Test_11_Test_Forache_Dictonary_GroupClass_Locality_WithClear()
        {
            int count = 1000000;

            //Создали
            Dictionary<int, GroupClass> list1 = new Dictionary<int, GroupClass>(count);
            Dictionary<int, GroupClass> list2 = new Dictionary<int, GroupClass>(count);
            Dictionary<int, GroupClass> list3 = new Dictionary<int, GroupClass>(count);
            Dictionary<int, GroupClass> list4 = new Dictionary<int, GroupClass>(count);
            Dictionary<int, GroupClass> list5 = new Dictionary<int, GroupClass>(count);
            Dictionary<int, GroupClass> list6 = new Dictionary<int, GroupClass>(count);
            Dictionary<int, GroupClass> list7 = new Dictionary<int, GroupClass>(count);
            Dictionary<int, GroupClass> list8 = new Dictionary<int, GroupClass>(count);
            Dictionary<int, GroupClass> list9 = new Dictionary<int, GroupClass>(count);

            //Заполнили
            for (int i = 0; i < count; i++)
            {
                list1.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
                list2.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
                list3.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
                list4.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
                list5.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
                list6.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
                list7.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
                list8.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
                list9.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
            }

            list2.Clear();
            list3.Clear();
            list4.Clear();
            list5.Clear();
            list6.Clear();
            list7.Clear();
            list8.Clear();
            list9.Clear();
            list2 = null;
            list3 = null;
            list4 = null;
            list5 = null;
            list6 = null;
            list7 = null;
            list8 = null;
            list9 = null;

            Thread.Sleep(5000);
            GC.Collect();

            //Тест
            Stopwatch stopwatch = new Stopwatch();
            float cnt = 1;
            float cntMax = 100;
            float sum = 0;
            float aver = 0;
            float max = 0;
            float min = 100000;
            while (cnt <= cntMax)
            {
                stopwatch.Restart();
                float x;
                float y;
                float z;
                foreach (GroupClass groupClass in list1.Values)
                {
                    groupClass.PozitionTest.X = groupClass.PozitionTest.X + groupClass.SpeedTest.DX;
                    groupClass.PozitionTest.Y = groupClass.PozitionTest.Y + groupClass.SpeedTest.DY;
                    groupClass.PozitionTest.Z = groupClass.PozitionTest.Z + groupClass.SpeedTest.DZ;
                }
                stopwatch.Stop();
                float time = stopwatch.ElapsedTicks / (float)TimeSpan.TicksPerMillisecond;
                Debug.WriteLine($"Обработка: {count} шт. произведена за {time} мс");
                if (time > max) { max = time; }
                if (time < min) { min = time; }
                sum = sum + time;
                aver = sum / cnt;
                Debug.WriteLine($"Среднее: {aver} Максимальное: {max} Минимальное: {min}");
                Thread.Sleep(200);
                cnt++;

            }
        }


        [TestMethod()]
        public void Test_12_Test_CallClear()
        {
            int count = 1000000;

            //Создали
            Dictionary<int, GroupClass> list1 = new Dictionary<int, GroupClass>(count);

            //Заполнили
            for (int i = 0; i < count; i++)
            {
                list1.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
            }

            //Тест
            Stopwatch stopwatch = new Stopwatch();
            float cnt = 1;
            float cntMax = 100;
            float sum = 0;
            float aver = 0;
            float max = 0;
            float min = 100000;
            while (cnt <= cntMax)
            {
                ActionClear actionClear = new ActionClear();
                stopwatch.Restart();
                foreach (GroupClass groupClass in list1.Values)
                {
                    actionClear.Action(groupClass.PozitionTest, groupClass.SpeedTest);
                }
                stopwatch.Stop();
                float time = stopwatch.ElapsedTicks / (float)TimeSpan.TicksPerMillisecond;
                Debug.WriteLine($"Обработка: {count} шт. произведена за {time} мс");
                if (time > max) { max = time; }
                if (time < min) { min = time; }
                sum = sum + time;
                aver = sum / cnt;
                Debug.WriteLine($"Среднее: {aver} Максимальное: {max} Минимальное: {min}");
                Thread.Sleep(200);
                cnt++;
            }
        }
        [TestMethod()]
        public void Test_13_Test_CallAbstract()
        {
            int count = 1000000;

            //Создали
            Dictionary<int, GroupClass> list1 = new Dictionary<int, GroupClass>(count);

            //Заполнили
            for (int i = 0; i < count; i++)
            {
                list1.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
            }

            //Тест
            Stopwatch stopwatch = new Stopwatch();
            float cnt = 1;
            float cntMax = 100;
            float sum = 0;
            float aver = 0;
            float max = 0;
            float min = 100000;
            while (cnt <= cntMax)
            {
                ActionAbstract action = new ActionFromAbstract();
                stopwatch.Restart();
                foreach (GroupClass groupClass in list1.Values)
                {
                    action.Action(groupClass.PozitionTest, groupClass.SpeedTest);
                }
                stopwatch.Stop();
                float time = stopwatch.ElapsedTicks / (float)TimeSpan.TicksPerMillisecond;
                Debug.WriteLine($"Обработка: {count} шт. произведена за {time} мс");
                if (time > max) { max = time; }
                if (time < min) { min = time; }
                sum = sum + time;
                aver = sum / cnt;
                Debug.WriteLine($"Среднее: {aver} Максимальное: {max} Минимальное: {min}");
                Thread.Sleep(200);
                cnt++;
            }
        }
        [TestMethod()]
        public void Test_14_Test_CallInterface()
        {
            int count = 1000000;

            //Создали
            Dictionary<int, GroupClass> list1 = new Dictionary<int, GroupClass>(count);

            //Заполнили
            for (int i = 0; i < count; i++)
            {
                list1.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
            }

            //Тест
            Stopwatch stopwatch = new Stopwatch();
            float cnt = 1;
            float cntMax = 100;
            float sum = 0;
            float aver = 0;
            float max = 0;
            float min = 100000;
            while (cnt <= cntMax)
            {
                IAction action = new ActionFromIAction();
                stopwatch.Restart();
                foreach (GroupClass groupClass in list1.Values)
                {
                    action.Action(groupClass.PozitionTest, groupClass.SpeedTest);
                }
                stopwatch.Stop();
                float time = stopwatch.ElapsedTicks / (float)TimeSpan.TicksPerMillisecond;
                Debug.WriteLine($"Обработка: {count} шт. произведена за {time} мс");
                if (time > max) { max = time; }
                if (time < min) { min = time; }
                sum = sum + time;
                aver = sum / cnt;
                Debug.WriteLine($"Среднее: {aver} Максимальное: {max} Минимальное: {min}");
                Thread.Sleep(200);
                cnt++;
            }
        }
        [TestMethod()]
        public void Test_15_Test_CallVirtual()
        {
            int count = 1000000;

            //Создали
            Dictionary<int, GroupClass> list1 = new Dictionary<int, GroupClass>(count);

            //Заполнили
            for (int i = 0; i < count; i++)
            {
                list1.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
            }

            //Тест
            Stopwatch stopwatch = new Stopwatch();
            float cnt = 1;
            float cntMax = 100;
            float sum = 0;
            float aver = 0;
            float max = 0;
            float min = 100000;
            while (cnt <= cntMax)
            {
                ActionVirtual action = new ActionFromActionVirtual();
                stopwatch.Restart();
                foreach (GroupClass groupClass in list1.Values)
                {
                    action.Action(groupClass.PozitionTest, groupClass.SpeedTest);
                }
                stopwatch.Stop();
                float time = stopwatch.ElapsedTicks / (float)TimeSpan.TicksPerMillisecond;
                Debug.WriteLine($"Обработка: {count} шт. произведена за {time} мс");
                if (time > max) { max = time; }
                if (time < min) { min = time; }
                sum = sum + time;
                aver = sum / cnt;
                Debug.WriteLine($"Среднее: {aver} Максимальное: {max} Минимальное: {min}");
                Thread.Sleep(200);
                cnt++;
            }
        }
        [TestMethod()]
        public void Test_16_Test_CallAbstract_Interface()
        {
            int threadNumb = ThreadNumb;
            ThreadNumb++;

            int count = 1000000;

            //Создали
            Dictionary<int, GroupClass> list1 = new Dictionary<int, GroupClass>(count);

            //Заполнили
            for (int i = 0; i < count; i++)
            {
                list1.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
            }

            //Тест
            Stopwatch stopwatch = new Stopwatch();
            float cnt = 1;
            float cntMax = 100;
            float sum = 0;
            float aver = 0;
            float max = 0;
            float min = 100000;
            float time = 0;
            while (cnt <= cntMax)
            {
                IAction action = new ActionFromActionAbstractFromIAction();
                stopwatch.Restart();
                foreach (GroupClass groupClass in list1.Values)
                {
                    action.Action(groupClass.PozitionTest, groupClass.SpeedTest);
                }
                stopwatch.Stop();
                time = stopwatch.ElapsedTicks / (float)TimeSpan.TicksPerMillisecond;
                if (time > max) { max = time; }
                if (time < min) { min = time; }
                sum = sum + time;
                aver = sum / cnt;
                Debug.WriteLine($"Поток: {threadNumb} Обработка: {count} шт. произведена за {time} мс");
                Debug.WriteLine($"Среднее: {aver} Максимальное: {max} Минимальное: {min}");

                Thread.Sleep(0);
                cnt++;
            }
            string result = $"Поток: {threadNumb} \r\n";
            result += $"Среднее: {aver} Максимальное: {max} Минимальное: {min}\r\n";
            results.Add(result);
        }


        [TestMethod()]
        public void Test_17_Test_CallVirtual_Parallel()
        {
            int count = 1000000;
            int countThreads = 8;

            //Создали
            Dictionary<int, GroupClass> list1 = new Dictionary<int, GroupClass>(count);

            //Заполнили
            for (int i = 0; i < count; i++)
            {
                list1.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
            }

            //Тест
            Stopwatch stopwatch = new Stopwatch();
            float cnt = 1;
            float cntMax = 100;
            float sum = 0;
            float aver = 0;
            float max = 0;
            float min = 100000;
            while (cnt <= cntMax)
            {
                IAction action = new ActionFromActionAbstractFromIAction();
                stopwatch.Restart();
                int maxCountOnThread = (int)Math.Ceiling(list1.Count / (float)countThreads); //Считаем количество объектов на один поток
                int i = 0; //Количество элементов для пропуска
                while (true)
                {
                    List<KeyValuePair<int, GroupClass>> items = list1.Skip(i).Take(maxCountOnThread).ToList(); //Получим часть коллекции
                    ThreadPool.QueueUserWorkItem(o => { RunPart(items, action); }); //Вызываем в отдельном потоке
                    if (i > list1.Count)
                    {
                        break;
                    } //Если вся коллекция обработана
                    i += maxCountOnThread; //Вычисляем кол-во элементов для пропуска
                } //Делим коллекцию и обрабатываем части коллекции в отдельных потоках
                stopwatch.Stop();
                float time = stopwatch.ElapsedTicks / (float)TimeSpan.TicksPerMillisecond;
                Debug.WriteLine($"Обработка: {count} шт. произведена за {time} мс");
                if (time > max) { max = time; }
                if (time < min) { min = time; }
                sum = sum + time;
                aver = sum / cnt;
                Debug.WriteLine($"Среднее: {aver} Максимальное: {max} Минимальное: {min}");
                Thread.Sleep(200);
                cnt++;
            }

            void RunPart(List<KeyValuePair<int, GroupClass>> items, IAction action)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    action.Action(items[i].Value.PozitionTest, items[i].Value.SpeedTest);
                }
            }
        }
        [TestMethod()]
        public void Test_18_Test_CallVirtual_Parallel()
        {
            int count = 1000000;

            //Создали
            Dictionary<int, GroupClass> list1 = new Dictionary<int, GroupClass>(count);

            //Заполнили
            for (int i = 0; i < count; i++)
            {
                list1.Add(i, new GroupClass() { PozitionTest = new PozitionTest(), SpeedTest = new SpeedTest() });
            }

            //Тест
            Stopwatch stopwatch = new Stopwatch();
            float cnt = 1;
            float cntMax = 100;
            float sum = 0;
            float aver = 0;
            float max = 0;
            float min = 100000;
            while (cnt <= cntMax)
            {
                IAction action = new ActionFromActionAbstractFromIAction();
                stopwatch.Restart();
                Parallel.ForEach(list1, item => action.Action(item.Value.PozitionTest, item.Value.SpeedTest));
                stopwatch.Stop();
                float time = stopwatch.ElapsedTicks / (float)TimeSpan.TicksPerMillisecond;
                Debug.WriteLine($"Обработка: {count} шт. произведена за {time} мс");
                if (time > max) { max = time; }
                if (time < min) { min = time; }
                sum = sum + time;
                aver = sum / cnt;
                Debug.WriteLine($"Среднее: {aver} Максимальное: {max} Минимальное: {min}");
                Thread.Sleep(200);
                cnt++;
            }
        }
        [TestMethod()]
        public void Test_19_Test_ParallelSystems_2Thread()
        {
            Thread thread1 = new Thread(Test_16_Test_CallAbstract_Interface);
            Thread thread2 = new Thread(Test_16_Test_CallAbstract_Interface);
            thread1.Start();
            thread2.Start();
            while (true)
            {
                if (thread1.ThreadState == System.Threading.ThreadState.Stopped)
                {
                    if (thread2.ThreadState == System.Threading.ThreadState.Stopped)
                    {
                        break;
                    }
                }
                Thread.Sleep(100);
            }
        }
        [TestMethod()]
        public void Test_20_Test_ParallelSystems_20Thread()
        {
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < 20; i++)
            {
                threads.Add(new Thread(Test_16_Test_CallAbstract_Interface));
            }
            for (int i = 0; i < 20; i++)
            {
                threads[i].Start();
            }
            while (true)
            {
                bool notReady = false;
                for (int i = 0; i < 20; i++)
                {
                    if (threads[i].ThreadState != System.Threading.ThreadState.Stopped)
                    {
                        notReady = true;
                        break;
                    }
                }
                if (notReady == false)
                {
                    break;
                }
                Thread.Sleep(100);
            }
            foreach (string str in results)
            {
                Debug.WriteLine(str);
            }
        }
    }
}