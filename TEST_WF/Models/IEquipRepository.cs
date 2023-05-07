using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEST_WF.Models;

namespace TEST_WF.Views
{
    public interface IEquipRepository
    {
        void Add(EquipModel equipModel);
        void Edit(EquipModel equipModel);
        void Delete(EquipModel equipModel);

        IEnumerable<EquipModel> GetAll();
        IEnumerable<EquipModel> GetByteValue();
    }
}
