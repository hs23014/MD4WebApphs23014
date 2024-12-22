using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD1_hs23014
{
    public interface IDataManager //kā paraugs ņemts kods no lekciju repozitorija (https://github.com/ElinaKalninaLU/Lekcija3_2024/blob/master/GeometryClasses/IDataManager.cs)
    {
        string Print();
        bool Save();
        bool Load();
        bool CreateTestData();
        bool Reset();
    }
}
