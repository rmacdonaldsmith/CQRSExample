using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CQRSSample.Domain.ServiceBus;
using Contracts.Commands;

namespace UI.Controllers
{
    public class MoveController : Controller
    {
        private readonly ISendCommands _serviceBus;

        public MoveController(ISendCommands serviceBus)
        {
            _serviceBus = serviceBus;
        }

        public ActionResult Create(Guid id)
        {
            ViewData["CustomerId"] = id;
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(MoveCustomerToNewAddress command, FormCollection collection)
        {
            try
            {
                var customerId = Guid.Parse(collection.Get("CustomerId"));
                command.CustomerId = customerId;
                _serviceBus.Send(command);
                return RedirectToAction("Index", "Customer");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Exception", ex.Message);
                return View("Create");
            }
        }

    }
}
