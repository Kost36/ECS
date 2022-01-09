using ECSCore.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCore.BaseObjects
{
    /// <summary>
    /// Класс репозитория сохранений
    /// </summary>
    internal class Repository
    {
        #region Конструткоры
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pathFolder"> Путь к папке сохранения </param>
        /// <param name="typeFileSave"> Тип фаилов сохранения </param>
        public Repository(string pathFolder, TypeFileSave typeFileSave)
        {
            if (String.IsNullOrEmpty(pathFolder))
            {
                pathFolder = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
            } //Если путь не задан
            if (Directory.Exists(pathFolder) == false)
            {
                throw new DirectoryNotFoundException();
            } //Если пути не существует
            _pathFolder = pathFolder;
            _typeFileSave = typeFileSave;
        }
        #endregion

        #region Поля
        /// <summary>
        /// Путь к папке с сохранениями
        /// </summary>
        private readonly string _pathFolder;
        /// <summary>
        /// Тип фаилов сохранения
        /// </summary>
        private readonly TypeFileSave _typeFileSave;
        #endregion

        #region Свойства

        #endregion

        #region Публичные методы
        /// <summary>
        /// Получить все сохранения
        /// </summary>
        public List<string> GetSaves()
        {
            TypeFileSave typeFileSave = _typeFileSave;
            return Directory.GetFiles(_pathFolder).ToList();
        }
        /// <summary>
        /// Сохранить состояние мира
        /// </summary>
        /// <param name="fileName"> Имя фаила сохранения </param>
        public void Save(string fileName = "")
        {

        }
        /// <summary>
        /// Загрузить состояние мира
        /// </summary>
        /// <param name="pathFile"> Путь к фаилу сохранения </param>
        public void Load(string pathFile = "")
        {

        }
        #endregion
    }
}