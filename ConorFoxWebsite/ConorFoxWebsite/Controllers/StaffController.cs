using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ConorFoxWebsite.WebServiceReference;
using ConorFoxWebsite.Models;
namespace ConorFoxWebsite.Controllers
{
    public class StaffController : Controller
    {
        private readonly WebsiteServiceClient _wsClient = new WebsiteServiceClient();

        /// <summary>
        /// Timetable display generated from Staff events both timetabled and Invitational
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public JsonResult ReturnTimetableJsonResult(int user)
        {
            var eventsList = _wsClient.StaffEvents(user);
            var returnList = new List<TimetableDisplayModel>();
            if (eventsList != null)
            {
                foreach (var e in eventsList)
                {
                    var startTime = "";
                    var endTime = "";
                    switch (e.Time)
                    {
                            //takes date of event that is passed in as well as the duration and
                            //generates the start and end times for the fullCalanader.js generatino
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
                    //Model used to pass information into the fullCalander json object
                    var item = new TimetableDisplayModel
                    {
                        AllDay = false,
                        Id = e.EventId.ToString("D"),
                        Start = startTime,
                        End = endTime,
                        Title = e.EventTitle
                    };
                    //timetable display object adde to list
                    returnList.Add(item);
                }
                //timtable display objects returned
                return Json(returnList, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        
        /// <summary>
        /// staff invitations pending response are returned
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public JsonResult ReturnStaffInvites(int user)
        {
            //Invitations returned and new list of Invites model created to return
            var invitesList = _wsClient.StaffInvites(user);
            var returnList = new List<InvitesModel>();

            //For each invitation a Invites Model object is created
            if (invitesList.Any())
            {
                foreach (var i in invitesList)
                {
                    //Returns detail of event that invitation represents
                    var events = _wsClient.ReturnEventDetail(i.EventId);
                    if (events != null)
                    {
                        //new object created for invitation and subsequent event
                        var inviteEv = new InvitesModel
                        {
                            Id = events.EventId,
                            Start = events.StartDate.ToString("dd/MM/yyyy"),
                            Duration = events.Duration.ToString("D")
                        };
                        //
                        switch (events.Time)
                        {
                                //times of the event in question converted into times am/pm
                            case (10):
                                inviteEv.Time = events.StartDate.AddHours(9).ToString("hh:mm tt");
                                break;
                            case (11):
                                inviteEv.Time = events.StartDate.AddHours(10).ToString("hh:mm tt");
                                break;
                            case (12):
                                inviteEv.Time = events.StartDate.AddHours(11).ToString("hh:mm tt");
                                break;
                            case (13):
                                inviteEv.Time = events.StartDate.AddHours(12).ToString("hh:mm tt");
                                break;
                            case (14):
                                inviteEv.Time = events.StartDate.AddHours(13).ToString("hh:mm tt");
                                break;
                            case (15):
                                inviteEv.Time = events.StartDate.AddHours(14).ToString("hh:mm tt");
                                break;
                            case (16):
                                inviteEv.Time = events.StartDate.AddHours(15).ToString("hh:mm tt");
                                break;
                            case (17):
                                inviteEv.Time = events.StartDate.AddHours(16).ToString("hh:mm tt");
                                break;

                        }
                        //invites added to list
                        returnList.Add(inviteEv);
                    }
                }
                //List of objects for invitations table gen returned
                return Json(returnList, JsonRequestBehavior.AllowGet);
            }
            //If no invites blank object returned
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
        /// Response to invitation by staff member
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public JsonResult RespondToInvitation(int eventId, int user)
        {
            //Invitation responded to
            var inviteResult = _wsClient.ConfirmStaffInvite(eventId, user);

            //confirmations of response
            if (inviteResult)
            {
                //attendance confirmed
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            //attendence confirmation failed
            return Json(false, JsonRequestBehavior.AllowGet); ;
        }
    }
}