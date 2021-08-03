using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using Newtonsoft.Json;
using ThirdPartyEventEditor.Models.Interfaces;

namespace ThirdPartyEventEditor.Models
{
    public class EventJsonService : IEventJsonService
    {
        private readonly string _path;

        public EventJsonService()
        {
            _path = GetDatabasePath();
        }

        public string GetDatabasePath()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var dbName = WebConfigurationManager.AppSettings["DatabaseName"];
            return currentDirectory + @"\App_Data\" + dbName + ".json";
        }

        public bool Add(ThirdPartyEvent thirdPartyEvent)
        {
            try
            {
                string newJson;

                using (var jsonStreamReader = new StreamReader(_path))
                {
                    var json = jsonStreamReader.ReadToEnd();

                    var eventList = JsonConvert.DeserializeObject<List<ThirdPartyEvent>>(json);
                    eventList = eventList ?? new List<ThirdPartyEvent>();

                    thirdPartyEvent.PrimaryKey = Guid.NewGuid();
                    eventList.Add(thirdPartyEvent);
                    newJson = JsonConvert.SerializeObject(eventList);
                }

                using (var jsonStreamWriter = new StreamWriter(_path))
                {
                    jsonStreamWriter.Write(newJson);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<ThirdPartyEvent> GetAll()
        {
            using (var jsonStream = new StreamReader(_path))
            {
                var json = jsonStream.ReadToEnd();
                var eventList = JsonConvert.DeserializeObject<List<ThirdPartyEvent>>(json);
                eventList = eventList ?? new List<ThirdPartyEvent>();

                return eventList;
            }
        }

        public ThirdPartyEvent GetById(Guid id)
        {
            var events = GetAll();
            var result = events.FirstOrDefault(e => e.PrimaryKey == id);
            return result;
        }

        public bool Delete(Guid id)
        {
            var events = GetAll();
            if (!events.Remove(events.FirstOrDefault(e => e.PrimaryKey == id)))
            {
                return false;
            }

            var newJson = JsonConvert.SerializeObject(events);
            File.WriteAllText(_path, newJson);
            return true;
        }

        public bool Edit(ThirdPartyEvent thirdPartyEvent)
        {
            try
            {
                var partyEvent = GetById(thirdPartyEvent.PrimaryKey);

                partyEvent.Name = thirdPartyEvent.Name;
                partyEvent.Description = thirdPartyEvent.Description;
                partyEvent.StartDate = thirdPartyEvent.StartDate;
                partyEvent.EndDate = thirdPartyEvent.EndDate;
                partyEvent.PosterImage = thirdPartyEvent.PosterImage;

                Delete(thirdPartyEvent.PrimaryKey);
                Add(partyEvent);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}