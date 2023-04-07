using KHP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHP.Data.DataRepository.Interface
{
    public interface ICatagoryRepository
    {
        void Save(Catagory catagory);
        List<Catagory> GetCatagories();

        void Edit(Catagory catagory);   
    }
}
