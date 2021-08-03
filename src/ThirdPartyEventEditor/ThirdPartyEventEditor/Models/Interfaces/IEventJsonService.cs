using System;
using System.Collections.Generic;

namespace ThirdPartyEventEditor.Models.Interfaces
{
    public interface IEventJsonService
    {
        bool Add(ThirdPartyEvent thirdPartyEvent);

        bool Delete(Guid id);

        bool Edit(ThirdPartyEvent thirdPartyEvent);

        ThirdPartyEvent GetById(Guid id);

        List<ThirdPartyEvent> GetAll();

        string GetDatabasePath();
    }
}