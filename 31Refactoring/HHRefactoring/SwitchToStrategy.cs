using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;


//用Strategy Pattern来替换原来的switch case和if else语句，解耦合，同时也使维护性和系统的可扩展性大大增强。 
//以注入的形式实现功能，这使增加功能变得更加容易和简便，同样也增强了整个系统的稳定性和健壮性。
namespace LosTechies.DaysOfRefactoring.SwitchToStrategy.Before
{
    public class ClientCode
    {
        public decimal CalculateShipping()
        {
            ShippingInfo shippingInfo = new ShippingInfo();
            return shippingInfo.CalculateShippingAmount(State.Alaska);
        }
    }

    public enum State
    {
        Alaska,
        NewYork,
        Florida
    }

    public class ShippingInfo
    {
        public decimal CalculateShippingAmount(State shipToState)
        {
            switch (shipToState)
            {
                case State.Alaska:
                    return GetAlaskaShippingAmount();
                case State.NewYork:
                    return GetNewYorkShippingAmount();
                case State.Florida:
                    return GetFloridaShippingAmount();
                default:
                    return 0m;
            }
        }

        private decimal GetAlaskaShippingAmount()
        {
            return 15m;
        }

        private decimal GetNewYorkShippingAmount()
        {
            return 10m;
        }

        private decimal GetFloridaShippingAmount()
        {
            return 3m;
        }
    }
}



namespace LosTechies.DaysOfRefactoring.SwitchToStrategy.After_WithIoC
{
    public interface IShippingInfo
    {
        decimal CalculateShippingAmount(State state);
    }

    public class ClientCode
    {
        //[Inject]??
        public IShippingInfo ShippingInfo { get; set; }

        public decimal CalculateShipping()
        {
            return ShippingInfo.CalculateShippingAmount(State.Alaska);
        }
    }

    public enum State
    {
        Alaska,
        NewYork,
        Florida
    }

    public class ShippingInfo : IShippingInfo
    {
        private IDictionary<State, IShippingCalculation> ShippingCalculations { get; set; }

        public ShippingInfo(IEnumerable<IShippingCalculation> shippingCalculations)
        {
            //
            ShippingCalculations = shippingCalculations.ToDictionary(calc => calc.State);
        }

        public decimal CalculateShippingAmount(State shipToState)
        {
            return ShippingCalculations[shipToState].Calculate();
        }
    }

    public interface IShippingCalculation
    {
        State State { get; }
        decimal Calculate();
    }

    public class AlaskShippingCalculation : IShippingCalculation
    {
        public State State { get { return State.Alaska; } }

        public decimal Calculate()
        {
            return 15m;
        }
    }

    public class NewYorkShippingCalculation : IShippingCalculation
    {
        public State State { get { return State.NewYork; } }

        public decimal Calculate()
        {
            return 10m;
        }
    }

    public class FloridaShippingCalculation : IShippingCalculation
    {
        public State State { get { return State.Florida; } }

        public decimal Calculate()
        {
            return 3m;
        }
    }
}