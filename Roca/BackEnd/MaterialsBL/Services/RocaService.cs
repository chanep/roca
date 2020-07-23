using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.BL.Repositories;

namespace Cno.Roca.BackEnd.Materials.BL.Services
{
    public class RocaService : IRocaService
    {
        protected IRocaUow RocaUow { get; set; }
        public RocaService(IRocaUow rocaUow)
        {
            RocaUow = rocaUow;
        }

        private ICommonService _commonService;
        private IMaterialService _materialService;
        private IMaterialListService _materialListService;
        private ITaggableTypeService _taggableTypeService;
        private ITimeSheetService _timeSheetService;
        private IMatPipingService _matPipingService;
        private IBasService _basService;

        public virtual ICommonService CommonService
        {
            get
            {
                if (_commonService == null)
                    _commonService = new CommonService(RocaUow);
                return _commonService;
            }
        }

        public virtual IMaterialService MaterialService
        {
            get
            {
                if (_materialService == null)
                    _materialService = new MaterialService(RocaUow);
                return _materialService;
            }
        }

        public virtual IMaterialListService MaterialListService
        {
            get
            {
                if (_materialListService == null)
                    _materialListService = new MaterialListService(RocaUow);
                return _materialListService;
            }
        }

        public virtual ITaggableTypeService TaggableTypeService
        {
            get
            {
                if (_taggableTypeService == null)
                    _taggableTypeService = new TaggableTypeService(RocaUow);
                return _taggableTypeService;
            }
        }

        public ITimeSheetService TimeSheetService
        {
            get
            {
                if (_timeSheetService == null)
                    _timeSheetService = new TimeSheetService(RocaUow);
                return _timeSheetService;
            }
        }

        public IMatPipingService MatPipingService
        {
            get
            {
                if (_matPipingService == null)
                    _matPipingService = new MatPipingService(RocaUow);
                return _matPipingService;
            }
        }

        public IBasService BasService
        {
            get
            {
                if (_basService == null)
                    _basService = new BasService(RocaUow);
                return _basService;
            }
        }

    }
}
