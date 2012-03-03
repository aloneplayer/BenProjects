using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//指如果要创建的对象很多，则代码会变的很复杂。一种很好的方法就是提取工厂类。
namespace LosTechies.DaysOfRefactoring.ExtractServiceClass.Before
{
    public class PoliceCar
    {
        public int Mileage { get; set; }
        public bool ServiceRequired { get; set; }
    }

    public class PoliceCarController
    {
        public PoliceCar New(int mileage, bool serviceRequired)
        {
            PoliceCar policeCar = new PoliceCar();
            policeCar.ServiceRequired = serviceRequired;
            policeCar.Mileage = mileage;

            return policeCar;
        }
    }
}

namespace LosTechies.DaysOfRefactoring.ExtractServiceClass.After
{
    public class PoliceCar
    {
        public int Mileage { get; set; }
        public bool ReadForService { get; set; }
    }

    public interface IPoliceCarFactory
    {
        PoliceCar Create(int mileage, bool serviceRequired);
    }

    public class PoliceCarFactory : IPoliceCarFactory
    {
        public PoliceCar Create(int mileage, bool serviceRequired)
        {
            PoliceCar policeCar = new PoliceCar();
            policeCar.ReadForService = serviceRequired;
            policeCar.Mileage = mileage;
            return policeCar;
        }
    }

    public class PoliceCarController
    {
        public IPoliceCarFactory PoliceCarFactory { get; set; }

        public PoliceCarController(IPoliceCarFactory policeCarFactory)
        {
            PoliceCarFactory = policeCarFactory;
        }

        public PoliceCar New(int mileage, bool serviceRequired)
        {
            return PoliceCarFactory.Create(mileage, serviceRequired);
        }
    }
}