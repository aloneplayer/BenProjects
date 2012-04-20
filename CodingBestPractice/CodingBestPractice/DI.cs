//
//Dependency Injection Demo
//

using System.Linq;
using System.Collections.Generic;

namespace CodingBestPractice
{
    #region Mock Classes
    public class Controller
    {
        public ActionResult View(object obj)
        {
            return new ActionResult(obj);
        }
    }

    public class ActionResult
    {
        private object _model;

        public ActionResult(object obj)
        {
            _model = obj;
        }

        public object Model
        {
            get { return _model; }
        }
    }
    #endregion

    public class Dinner
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }

    public class NerdDinnerEntities
    {
        /// <summary>
        /// Get dinners from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<Dinner> Query(string id)
        {
            return null;
        }

        /// <summary>
        /// Get all dinners in DB
        /// </summary>
        /// <returns></returns>
        public IQueryable<Dinner> All()
        {
            return null;
        }

        public void Add(Dinner dinner)
        {
            
        }
    }

    /// <summary>
    /// Need refactoring
    /// </summary>
    public class DinnerRepository
    {
        /// <summary>
        /// Question: How to test when there is no real DB?
        /// </summary>
        private NerdDinnerEntities _db = new NerdDinnerEntities();

        public Dinner FindDinner(string q)
        {
            return new Dinner();
        }

        public IQueryable<Dinner> FindAllDinners()
        {
            return _db.All();
        }

        public void Add(Dinner dinner)
        {
            _db.Add(dinner);
        }
    }

    public class DinnersController : Controller
    {
        /// <summary>
        /// Can we use another repository here
        /// </summary>
        private DinnerRepository dinnerRepository = new DinnerRepository();

        public ActionResult Details(string id)
        {
            Dinner dinner = dinnerRepository.FindDinner(id);

            return View(dinner);
        }
    }

    #region Better
    public interface IDinnerRepository
    {
        IQueryable<Dinner> FindAllDinners();
        Dinner FindDinner(string q);
        void Add(Dinner dinner);
    }

    public class RealDinnerRepository : IDinnerRepository
    {
        NerdDinnerEntities _db = new NerdDinnerEntities();
        public Dinner FindDinner(string q)
        {
            return new Dinner();
        }

        public IQueryable<Dinner> FindAllDinners()
        {
            return _db.All();
        }

        public void Add(Dinner dinner)
        {
            _db.Add(dinner);
        }
    }

    public class FakeDinnerRepository : IDinnerRepository
    {
        private List<Dinner> _dinnerList;

        public FakeDinnerRepository(List<Dinner> dinners)
        {
            _dinnerList = dinners;
        }

        public IQueryable<Dinner> FindAllDinners()
        {
            return _dinnerList.AsQueryable();
        }

        public Dinner FindDinner(string id)
        {
            return _dinnerList.SingleOrDefault(d => d.Id == id);
        }

        public void Add(Dinner dinner)
        {
            _dinnerList.Add(dinner);
        }
    }

    public class BetterDinnersController : Controller
    {
        IDinnerRepository dinnerRepository;

        public BetterDinnersController(IDinnerRepository repository)
        {
            dinnerRepository = repository;
        }

        /// <summary>
        /// Defaultly, the controll will use the real repository to get data from DB
        /// </summary>
        public BetterDinnersController()
            : this(new RealDinnerRepository())
        {
        }

        public ActionResult Details(string id)
        {
            Dinner dinner = dinnerRepository.FindDinner(id);

            return View(dinner);
        }
    }
    #endregion
}

