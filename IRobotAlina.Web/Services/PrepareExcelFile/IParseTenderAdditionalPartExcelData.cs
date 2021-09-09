using IRobotAlina.Data.Entities;
using System;
using System.Collections.Generic;

namespace IRobotAlina.Web.Services.PrepareExcelFile
{
    public interface IParseTenderAdditionalPartExcelData
    {
        public List<Tender> GetTenderAdditionalPart(string tenderAdditionalPartExctractedData);
    }
}
