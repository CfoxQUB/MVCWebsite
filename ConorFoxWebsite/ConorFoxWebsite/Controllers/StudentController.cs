using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ConorFoxWebsite.WebServiceReference;
using ConorFoxWebsite.Models;

namespace ConorFoxWebsite.Controllers
{
    public class StudentController : Controller
    {
        //Webservice functionality exposed from service reference
        private readonly WebsiteServiceClient _wsClient = new WebsiteServiceClient();

        /// <summary>
        /// Timetable json returned for timetables and invitational events
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public JsonResult ReturnTimetableJsonResult(int user)
        {
            //events list returned for user and timetable object list created
            var eventsList = _wsClient.StudentsEvents(user);
            var returnList = new List<TimetableDisplayModel>();
            //As long as events exist function generates new list of timetableDisplayModel objects
            if (eventsList != null)
            {
                //For each event a new timetabledisplaymodel object generated
                foreach (var e in eventsList)
                {
                    var startTime = "";
                    var endTime = "";
                    switch (e.Time)
                    {
                            //Times for the timetable display model generated for jsoon fullCalander Format
                        case (10):
                            startTime = e.StartDate.AddHours(9).ToString("yyyy-MM-dd HH:mm:ss");
                            endTime = e.StartDate.AddHours(9).AddMinutes(e.Duration).ToString("yyyy-MM-dd HH:mm:ss");
                            break;
                        case (11):
                            startTime = e.StartDate.AddHours(10).ToString("yyyy-MM-dd HH:mm:ss");
                            endTime = e.StartDate.AddHours(10).AddMinutes(e.Duration).ToString("yyyy-MM-dd HH:mm:ss");
                            break;
                        case (12):
                            startTime = e.StartDate.AddHours(11).ToString("yyyy-MM-dd HH:mm:ss");
                            endTime = e.StartDate.AddHours(11).AddMinutes(e.Duration).ToString("yyyy-MM-dd HH:mm:ss");
                            break;
                        case (13):
                            startTime = e.StartDate.AddHours(12).ToString("yyyy-MM-dd HH:mm:ss");
                            endTime = e.StartDate.AddHours(12).AddMinutes(e.Duration).ToString("yyyy-MM-dd HH:mm:ss");
                            break;
                        case (14):
                            startTime = e.StartDate.AddHours(13).ToString("yyyy-MM-dd HH:mm:ss");
                            endTime = e.StartDate.AddHours(13).AddMinutes(e.Duration).ToString("yyyy-MM-dd HH:mm:ss");
                            break;
                        case (15):
                            startTime = e.StartDate.AddHours(14).ToString("yyyy-MM-dd HH:mm:ss");
                            endTime = e.StartDate.AddHours(14).AddMinutes(e.Duration).ToString("yyyy-MM-dd HH:mm:ss");
                            break;
                        case (16):
                            startTime = e.StartDate.AddHours(15).ToString("yyyy-MM-dd HH:mm:ss");
                            endTime = e.StartDate.AddHours(15).AddMinutes(e.Duration).ToString("yyyy-MM-dd HH:mm:ss");
                            break;
                        case (17):
                            startTime = e.StartDate.AddHours(16).ToString("yyyy-MM-dd HH:mm:ss");
                            endTime = e.StartDate.AddHours(16).AddMinutes(e.Duration).ToString("yyyy-MM-dd HH:mm:ss");
                            break;

                    }
                    //Timtable display model object created for event
                    var item = new TimetableDisplayModel
                    {
                        AllDay = false,
                        Id = e.EventId.ToString("D"),
                        Start = startTime,
                        End = endTime,
                        Title = e.EventTitle
                    };
                    returnList.Add(item);
                }
                //events for FullCalanader returned
                return Json(returnList, JsonRequestBehavior.AllowGet);
            }
            //no events exist
            return null;
        }


        public JsonResult ReturnStudentInvites(int user)
        {
            //Invites returned and table generation objcet list craeted for response
            var invitesList = _wsClient.StudentsEvents(user);
            var returnList = new List<InvitesModel>();
            //Invites exist in databse
            if (invitesList.Any())
            {
                //For each invite a new display object is created
                foreach (var i in invitesList)
                {
                    //return event detail for invite
                    var events = _wsClient.ReturnEventDetail(i.EventId);
                    if (events != null)
                    {//new dipsplay model generated and populated with event detail
                        var inviteEv = new InvitesModel
                        {
                            Id = events.EventId,
                            Start = events.StartDate.ToString("dd/MM/yyyy"),
                            Duration = events.Duration.ToString("D")
                        };
                        //Times for the event generated
                        switch (events.Time)
                        {
                            case (10):
                                inviteEv.Time = events.StartDate.AddHours(9).ToString("hh:mm");
                                break;
                            case (11):
                                inviteEv.Time = events.StartDate.AddHours(10).ToString("hh:mm");
                                break;
                            case (12):
                                inviteEv.Time = events.StartDate.AddHours(11).ToString("hh:mm");
                                break;
                            case (13):
                                inviteEv.Time = events.StartDate.AddHours(12).ToString("hh:mm");
                                break;
                            case (14):
                                inviteEv.Time = events.StartDate.AddHours(13).ToString("hh:mm");
                                break;
                            case (15):
                                inviteEv.Time = events.StartDate.AddHours(14).ToString("hh:mm");
                                break;
                            case (16):
                                inviteEv.Time = events.StartDate.AddHours(15).ToString("hh:mm");
                                break;

                        }
                        returnList.Add(inviteEv);
                    }
                }
                //Table contents returned to ajax method in view
                return Json(returnList, JsonRequestBehavior.AllowGet);
            }
            //no invites exist default contents returned
            var emptyInvite = new InvitesModel
            {
                Id = 0,
                Duration = "None",
                Start = "None",
                Time = "None",
                Title = "None"
            };
            returnList.Add(emptyInvite);
            return Json(returnList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// student response to invitation
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public JsonResult RespondToInvitation(int eventId, int user)
        {
            //Response made for invitation
            var inviteResult = _wsClient.ConfirmStudentInvite(eventId, user);

            //update response
            if (inviteResult)
            {
                //response success
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            //Response failure
            return Json(false, JsonRequestBehavior.AllowGet);
            
        }
    }
}