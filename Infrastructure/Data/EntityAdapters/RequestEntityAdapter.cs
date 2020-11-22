using System;
using System.Collections.Generic;
using Domain.Model.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Data.EntityAdapters
{
    public class RequestEntityAdapter : IEntityAdapter
    {
        public bool CanHandle(Type type)
        {
            return type == typeof(Request);
        }

        public string GetFileName()
        {
            return "Requests.xlsx";
        }

        public BaseEntity GetEntity(List<string> rowValues)
        {
            return new Request(
                rowValues[0], 
                rowValues[1], 
                rowValues[2],
                rowValues[3],
                rowValues[4],
                rowValues[5],
                rowValues[6],
                rowValues[7],
                rowValues[8],
                rowValues[9],
                rowValues[10],
                rowValues[11],
                rowValues[12],
                rowValues[13]
                );
        }

        public List<string> GetValuesFromEntity(BaseEntity entity)
        {
            var request = entity as Request;
            return new List<string>
            {
                request.Id,
                request.ContactName,
                request.ContactPhone,
                request.ContactEmail,
                request.SchoolInstitutionName,
                request.ParticipantsGroup,
                request.ParticipantsAge,
                request.ParticipantsNumber,
                request.SuggestedEvent,
                request.SuggestedLocation,
                request.SuggestedTime,
                request.SuggestedDate,
                request.Remarks,
                request.InstitutionOrSchool
        };
        }
    }
}