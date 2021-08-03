using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using ThirdPartyEventEditor.Models;
using ThirdPartyEventEditor.Models.Interfaces;

namespace ThirdPartyEventEditor.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventJsonService _eventJsonService;

        public HomeController(IEventJsonService eventJsonService)
        {
            _eventJsonService = eventJsonService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var circusEvent = new ThirdPartyEvent
            {
                Name = "Почти серьезно",
                EndDate = new DateTime(2021, 06, 30, 21, 00, 00),
                StartDate = new DateTime(2021, 05, 30, 15, 00, 00),
                PosterImage = await UploadSampleImage(),
                Description = @"С 15 мая по 1 августа Белгосцирк и Московский цирк Ю.Никулина на
Цветном бульваре представляют новую цирковую программу «Почти серьезно», посвященную 100-летию со Дня рождения Юрия Никулина!
В программе- дрессированные лошади, медведи, козы, бразильское колесо смелости,мото-шар,
эквилибристы на канате, акробаты на мачте, воздушные гимнасты, жонглеры и клоуны! Спешите!",
            };

            var thirdPartyEvents = _eventJsonService.GetAll();
            thirdPartyEvents.Add(circusEvent);
            return View(thirdPartyEvents);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public string Add(ThirdPartyEvent thirdPartyEvent)
        {
            var result = _eventJsonService.Add(thirdPartyEvent);

            return result ? "Event successfully created" : "Event does not created";
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var partyEvent = _eventJsonService.GetById(id);

            return View(partyEvent);
        }

        [HttpPost]
        public string Edit(ThirdPartyEvent thirdPartyEvent)
        {
            var result = _eventJsonService.Edit(thirdPartyEvent);

            return result ? "Event edited successfully" : "Error";
        }

        [HttpGet]
        public string Delete(Guid id)
        {
            _eventJsonService.Delete(id);
            return "Event deleted";
        }

        [HttpGet]
        public ActionResult Download()
        {
            var path = _eventJsonService.GetDatabasePath();
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);

            return new FileStreamResult(stream, "application/octet-stream");
        }

        private async Task<string> UploadSampleImage()
        {
            var path = Path.Combine(Server.MapPath("~/App_Data/"), "poster_circus.png");
            using (var memoryStream = new MemoryStream())
            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                await fileStream.CopyToAsync(memoryStream);
                return "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
            }
        }
    }
}