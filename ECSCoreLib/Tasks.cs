using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCoreLib
{
    //Задачи
    // TODO 1) Потокобезопастность систем!!!! Пометить компоненты в системах (как readOnly) и если данный компонент гдето пишется не вызывать его в других системах 
    // TODO 2) Добавить возможность выполнять некоторые системы в STA потоке клиентского ПО (Unity)
    // TODO 5) Убрать Id из компонента
    // TODO 6) Проверить что бы deltaTime в системе равнялась реальному deltatime (Неверно работает!!!)
    // TODO 7) Система с ручной обработкой
    // TODO 8) Уйти от виртуальных вызовов Action у систем
}