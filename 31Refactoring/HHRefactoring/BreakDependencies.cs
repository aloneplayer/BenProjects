using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//指对部分不满足我们要求的类和方法进行依赖分解，通过装饰器来达到我们需要的功能。

//如果你要在你的代码中加入单元测试但有一部分代码是你不想测试的，那么你应用使用这个的重构。
//下面的例子中我们应用静态类来完成某些工作，但问题是在单元测试时我们无法mock静态类，
//所以我们只能引入静态类的装饰接口来分解对静态类的依赖。
//从而我们使我们的调用类只需要依赖于装饰接口就能完成这个操作。 
namespace LosTechies.DaysOfRefactoring.BreakDependencies.Before
{
    public class AnimalFeedingService
    {
        private bool FoodBowlEmpty { get; set; }

        public void Feed()
        {
            if (FoodBowlEmpty)
                Feeder.ReplenishFood();

            // more code to feed the animal
        }
    }

    public static class Feeder
    {
        public static void ReplenishFood()
        {
            // fill up bowl
        }
    }
}

//在应用了分解依赖模式后，我们就可以在单元测试的时候mock一个IFeederService对象并通
//过AnimalFeedingService的构造函数传递给它。
namespace LosTechies.DaysOfRefactoring.BreakDependencies.After
{
    public class AnimalFeedingService
    {
        public IFeederService FeederService { get; set; }

        public AnimalFeedingService(IFeederService feederService)
        {
            FeederService = feederService;
        }

        private bool FoodBowlEmpty { get; set; }

        public void Feed()
        {
            if (FoodBowlEmpty)
                FeederService.ReplenishFood();

            // more code to feed the animal
        }
    }

    public interface IFeederService
    {
        void ReplenishFood();
    }

    public class FeederService : IFeederService
    {
        public void ReplenishFood()
        {
            Feeder.ReplenishFood();
        }
    }

    public static class Feeder
    {
        public static void ReplenishFood()
        {
            // fill up bowl
        }
    }
}