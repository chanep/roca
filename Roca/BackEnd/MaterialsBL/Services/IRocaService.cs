using System;

namespace Cno.Roca.BackEnd.Materials.BL.Services
{
    public interface IRocaService
    {
        ICommonService CommonService { get; }
        IMaterialService MaterialService { get; }
        IMaterialListService MaterialListService { get; }
        ITaggableTypeService TaggableTypeService { get; }
        ITimeSheetService TimeSheetService { get; }
        IMatPipingService MatPipingService { get; }
        IBasService BasService { get; }
    }
}