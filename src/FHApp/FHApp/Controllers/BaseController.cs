using FH.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FH.App.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly INotificator _notificator;

        public BaseController(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected bool ValidOperation()
        {
            return !_notificator.HasNotification();
        }
    }
}
