using IRobotAlina.Data.Entities;
using System.Collections.Generic;

namespace IRobotAlina.Web.Services.Builder
{
    //public interface ITenderBuilder
    //{
    //    public (Tender tender, List<LinkFileDto> rootFiles) GetOrCreate(Tender info);
    //}

    public interface IZakupkiKonturTenderBuilder //: ITenderBuilder
    {
        public (Tender tender, List<LinkFileDto> rootFiles) GetOrCreate(Tender info, Tender addPart);
    }
}
