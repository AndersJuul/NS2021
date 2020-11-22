using System;
using Domain.Interfaces;

namespace Domain.Model.Entities
{
    public class Request : BaseEntity, IAggregateRoot
    {
        public Request(string id, string contactName, string contactPhone, string contactEmail, string schoolInstitutionName, string participantsGroup, string participantsAge, string participantsNumber, string suggestedEvent, string suggestedLocation, string suggestedTime, string suggestedDate, string remarks, string institutionOrSchool)
        {
            Id = id;
            ContactName = contactName;
            ContactPhone = contactPhone;
            ContactEmail = contactEmail;
            SchoolInstitutionName = schoolInstitutionName;
            ParticipantsGroup = participantsGroup;
            ParticipantsAge = participantsAge;
            ParticipantsNumber = participantsNumber;
            SuggestedEvent = suggestedEvent;
            SuggestedLocation = suggestedLocation;
            SuggestedTime = suggestedTime;
            SuggestedDate = suggestedDate;
            Remarks = remarks;
            InstitutionOrSchool = institutionOrSchool;
            //ContactEmail,
            //request.SchoolInstitutionName,
            //request.ParticipantsGroup,
            //request.ParticipantsAge,
            //request.ParticipantsNumber,
            //request.SuggestedEvent,
            //request.SuggestedLocation,
            //request.SuggestedTime,
            //request.SuggestedDate,
            //request.Remarks,
            //request.InstitutionOrSchool

        }

        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string SchoolInstitutionName { get; set; }
        public string ParticipantsGroup { get; set; }
        public string ParticipantsAge { get; set; }
        public string ParticipantsNumber { get; set; }
        public string SuggestedEvent { get; set; }
        public string SuggestedLocation { get; set; }
        public string SuggestedTime { get; set; }
        public string SuggestedDate { get; set; }
        public string Remarks { get; set; }
        public string InstitutionOrSchool { get; set; }
    }
}