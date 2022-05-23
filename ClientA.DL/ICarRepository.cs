using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClientA.Models;

namespace ClientA.DL
{
    public interface ICarRepository
    {
        Task Add(Car car);

        Task<IEnumerable<Car>> GetAll();

        Task<IEnumerable<Car>> GetAllByYear(int Year);
    }
}
