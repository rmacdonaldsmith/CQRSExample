using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CQRSSample.Domain.CommandHandlers;
using CQRSSample.Domain.ReadModel;
using CQRSSample.Domain.ServiceBus;
using Contracts.Commands;

namespace UI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerReadModelFacade _customerReadModel;
        private readonly ISendCommands _serviceBus;

        public CustomerController(ICustomerReadModelFacade customerReadModel, ISendCommands serviceBus)
        {
            _customerReadModel = customerReadModel;
            _serviceBus = serviceBus;
        }

        //
        // GET: /Customer/

        public ActionResult Index()
        {
            var customers = _customerReadModel.GetAll();
            return View(customers);
        }

        //
        // GET: /Customer/Details/5

        public ActionResult Details(Guid id)
        {
            var customer = _customerReadModel.Get(id);

            return View(customer);
        }

        //
        // GET: /Customer/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Customer/Create

        [HttpPost]
        public ActionResult Create(RegisterNewCustomer command, FormCollection collection)
        {
            try
            {
                command.CustomerId = Guid.NewGuid();
                _serviceBus.Send(command);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Exception", ex.Message);
                return View();
            }
        }
    }
}
