using System;

namespace IRobotAlina.Web.Services.PrepareExcelFile
{
    /// <summary>
    /// Информация, содержащаяся в одной ячейке Excel-файла из письма-рассылки, содержащего консолидированную информацию по всем тендерам.
    /// </summary>
    [Serializable]
    public class TenderAdditionalPartExcelCellInfo
    {
        /// <summary>
        /// Номер строки
        /// </summary>
        public int rowNumber { get; set; }

        /// <summary>
        /// Наименование секции        
        /// </summary>
        public string sectionName { get; set; }

        /// <summary>
        /// Наименование параметра тендера
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Значение параметра тендера
        /// </summary>
        public string value { get; set; }
    }
}
