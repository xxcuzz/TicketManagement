using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TicketManagement.BusinessLogic.EntitiesDTO;
using TicketManagement.BusinessLogic.Services.Interfaces;

namespace Web.Controllers
{
    [Authorize(Roles = "Event Manager")]
    public class ThirdPartyController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IEventService _eventService;
        private readonly IService<LayoutDto> _layoutService;
        private readonly IMapper _mapper;

        public ThirdPartyController(IWebHostEnvironment env,
            IEventService eventService,
            IService<LayoutDto> layoutService,
            IMapper mapper)
        {
            _env = env;
            _eventService = eventService;
            _layoutService = layoutService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> UploadEventsAsync(IFormFile jsonFile)
        {
            var uploadPath = _env.ContentRootPath + "\\ThirdPartyEventImports";

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var fileName = DateTime.UtcNow.ToString("yyyy-dd-M--HH-mm-ss") + ".json";

            using var fileStream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create, FileAccess.Write);
            await jsonFile.CopyToAsync(fileStream);
            fileStream.Close();

            var thirdPartyEvents = await GetEventsFromJsonAsync(fileName, uploadPath);

            int addedEvents = 0;
            foreach (var thirdPartyEvent in thirdPartyEvents)
            {
                var eventDto = _mapper.Map<ThirdPartyEventDto, EventDto>(thirdPartyEvent);
                eventDto.LayoutId = _layoutService.GetAll().FirstOrDefault().Id;
                if (await _eventService.CreateAsync(eventDto))
                {
                    addedEvents++;
                }
            }

            if (!thirdPartyEvents.Any())
            {
                return RedirectToAction("ZeroEvents", "ThirdParty", new { added = addedEvents });
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ZeroEvents(int added)
        {
            ViewBag.Added = added;
            return View();
        }

        private static async Task<List<ThirdPartyEventDto>> GetEventsFromJsonAsync(string fileName, string uploadPath)
        {
            try
            {
                using var fs = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Open, FileAccess.Read, FileShare.None);
                using var jsonStream = new StreamReader(fs);
                var json = await jsonStream.ReadToEndAsync();
                var eventList = JsonConvert.DeserializeObject<List<ThirdPartyEventDto>>(json);
                eventList ??= new List<ThirdPartyEventDto>();

                return eventList;
            }
            catch (Exception)
            {
                return new List<ThirdPartyEventDto>();
            }
        }
    }
}
