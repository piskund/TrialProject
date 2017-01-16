// -------------------------------------------------------------------------------------------------------------
//  HomeController.cs created by DEP on 2017/01/16
// -------------------------------------------------------------------------------------------------------------

using System.Linq;
using System.Web.Mvc;
using Backup.Admin.WebApp.Models;
using Backup.DAL.Interfaces;

namespace Backup.Admin.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IScheduledBackupRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public HomeController(IScheduledBackupRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = _repository.GetAll().Select(b => (BackupInfoViewModel)b).ToList();
            return View(model);
        }
    }
}