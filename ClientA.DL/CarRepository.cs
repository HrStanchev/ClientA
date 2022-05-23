using ClientA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientA.DL
{
    public class CarRepository : ICarRepository
    {
        public CarRepository()
        {

        }
        public Task Add(Car car)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Car>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Car>> GetAllByYear(int Year)
        {
            throw new NotImplementedException();
        }
    }
}
