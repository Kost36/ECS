using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECSCoreTests
{
    [TestClass()]
    public class DotNetPerformanceTest
    {
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

        //Добавить тест с словарем и вызовом метода класса
        //Добавить тест с словарем и вызовом абстрактоно метода базового класа
        //Добавить тест с словарем и вызовом виртуального метода базового класа
        //Добавить тест с словарем и вызовом метода интерфейса

        //Добавить тесты с словарем и обобщениями

        //Добавить тесты с словарем и распараллеливанием на 8 потоков
        //Добавить тесты с словарем и распараллеливанием на 16 потоков

        [TestMethod()]
        public void Test_00_Test_For_List_Structure()
        {
            int count = 1000000;

            //Создали
            List<PozitionStruct> list1 = new List<PozitionStruct>(count); //11.5 мб
            List<SpeedStruct> list2 = new List<SpeedStruct>(count); //11.5 мб

            //Заполнили
            for (int i=0; i < count; i++)
            {
                list1.Add(new PozitionStruct());
                list2.Add(new SpeedStruct(1,1,1));
            }

            //Тест
            Stopwatch stopwatch = new Stopwatch();
            float cnt=1;
            float cntMax = 100;
            float sum=0;
            float aver=0;
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
                float time = ((float)stopwatch.ElapsedTicks) / ((float)TimeSpan.TicksPerMillisecond);
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
                float time = ((float)stopwatch.ElapsedTicks) / ((float)TimeSpan.TicksPerMillisecond);
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
        public void Test_03_Test_For_Array_GroupStructure()
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
                float time = ((float)stopwatch.ElapsedTicks) / ((float)TimeSpan.TicksPerMillisecond);
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
        public void Test_04_Test_For_Array_GroupStructure_locality()
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
                float time = ((float)stopwatch.ElapsedTicks) / ((float)TimeSpan.TicksPerMillisecond);
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
        public void Test_05_Test_For_List_Class()
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
                    list1[i].Y= list1[i].Y + list2[i].DY;
                    list1[i].Z = list1[i].Z + list2[i].DZ;
                }
                stopwatch.Stop();
                float time = ((float)stopwatch.ElapsedTicks) / ((float)TimeSpan.TicksPerMillisecond);
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
        public void Test_06_Test_For_Array_Class()
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
                float time = ((float)stopwatch.ElapsedTicks) / ((float)TimeSpan.TicksPerMillisecond);
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
        public void Test_07_Test_For_Array_GroupClass()
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
                float time = ((float)stopwatch.ElapsedTicks) / ((float)TimeSpan.TicksPerMillisecond);
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
        public void Test_08_Test_For_Array_GroupClass_locality()
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
                float time = ((float)stopwatch.ElapsedTicks) / ((float)TimeSpan.TicksPerMillisecond);
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
        public void Test_09_Test_For_Dictonary_GroupClass()
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
                float x;
                float y;
                float z;
                for (int i = 0; i < list1.Count; i++)
                {
                    list1[i].PozitionTest.X = list1[i].PozitionTest.X + list1[i].SpeedTest.DX;
                    list1[i].PozitionTest.Y = list1[i].PozitionTest.Y + list1[i].SpeedTest.DY;
                    list1[i].PozitionTest.Z = list1[i].PozitionTest.Z + list1[i].SpeedTest.DZ;
                }
                stopwatch.Stop();
                float time = ((float)stopwatch.ElapsedTicks) / ((float)TimeSpan.TicksPerMillisecond);
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
        public void Test_10_Test_Forache_Dictonary_GroupClass()
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
                float time = ((float)stopwatch.ElapsedTicks) / ((float)TimeSpan.TicksPerMillisecond);
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
        public void Test_11_Test_Forache_Dictonary_GroupClass_Locality()
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
                float time = ((float)stopwatch.ElapsedTicks) / ((float)TimeSpan.TicksPerMillisecond);
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
        public void Test_12_Test_Forache_Dictonary_GroupClass_Locality_WithClear()
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
                float time = ((float)stopwatch.ElapsedTicks) / ((float)TimeSpan.TicksPerMillisecond);
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
    }
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
}