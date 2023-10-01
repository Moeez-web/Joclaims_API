using BAL.IManager;
using BAL.Manager;
using MODEL.Models;
using ShubeddakAPI.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;

namespace ShubeddakAPI.Controllers
{
    public class NotificationController : ApiController
    {
        private readonly INotificationManager _notificationManager;
        internal static SqlDependency dependency = null;

        public NotificationController()
        {
            _notificationManager = new NotificationManger();
        }
        public static void DependencyTrigger()
        {
            List<Notification> notifications = new List<Notification>();

            using (var sCon = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (var sCmd = new SqlCommand("[getAllNotifications]", sCon))
                {
                    sCon.Open();
                    sCmd.CommandTimeout = 300;
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Notification = null;
                    SqlDependency.Start(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

                    dependency = null;
                    ////here w'll set up our sql dependency
                    if (dependency == null)
                    {
                        dependency = new SqlDependency(sCmd);

                        //// Subscribe to the SqlDependency event.
                        dependency.OnChange += new
                           OnChangeEventHandler(dependency_OnChange);
                    }


                    //var sParameter = new List<SqlParameter>
                    //{
                    //    new SqlParameter { ParameterName = "@UserID", Value = UserID},

                    //};

                    //if (null != sParameter)
                    //{
                    //    sCmd.Parameters.AddRange(sParameter.ToArray());
                    //}

                    using (var sDAdapter = new SqlDataAdapter(sCmd))
                    {
                        using (var ds = new DataSet())
                        {
                            sDAdapter.Fill(ds);

                            notifications = ds.Tables[0].AsEnumerable().Select(n => new Notification
                            {
                                NotificationID = n.Field<int>("NotificationID"),
                                RecipientID = n.Field<int?>("RecipientID"),
                                TextData = n.Field<string>("TextData"),
                                RequestID = n.Field<int?>("RequestID"),
                                QuotationID = n.Field<int?>("QuotationID"),
                                DemandID = n.Field<int?>("DemandID"),
                                CreatedOn = n.Field<DateTime>("CreatedOn"),
                                CreatedBy = n.Field<int>("CreatedBy"),
                                RedirectURL = n.Field<string>("RedirectURL"),
                                MobileScreenName = n.Field<string>("MobileScreenName"),
                                MobileNotificationToken = n.Field<string>("MobileNotificationToken"),
                                TextArabicData = n.Field<string>("TextArabicData"),
                                SupplierID = n.Field<int?>("SupplierID"),
                                MobileTypeID = n.Field<Int16?>("MobileTypeID"),
                            }).ToList();

                            foreach (var item in notifications)
                            {
                                if (item.MobileNotificationToken != null && item.MobileNotificationToken != "")
                                {
                                    if(item.MobileTypeID == 64)
                                    {
                                        SendMobilePushNotification(item);
                                    }

                                    if (item.MobileTypeID == 65)
                                    {
                                        SendHuaweiPushNotification(item);
                                    }

                                    if (item.MobileTypeID == 66)
                                    {
                                        SendApplePushNotification(item);
                                    }
                                }
                            }

                            if(notifications!= null && notifications.Count > 0)
                            {
                                NotificationManger manager = new NotificationManger();
                                var resulta = manager.MarkNotificationsSent(notifications);
                            }

                        }
                    }
                }
            }

        }

        private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                if (dependency != null)
                {
                    dependency.OnChange -= dependency_OnChange;
                    dependency = null;
                }

                NotificationManger manager = new NotificationManger();
                var result = manager.GetSingleNotification();

                foreach (var item in result)
                {
                    ServerHub.NotifyClient(item.RecipientID.ToString(), item);
                }

                DependencyTrigger();
            }

        }

        #region NotificationSent
        [HttpGet]
        [Route("api/Notification/NotificationSent")]
        public IHttpActionResult NotificationSent(int notificationID)
        {
            try
            {
                string result = null;
                result = _notificationManager.NotificationSent(notificationID);
                if (result.Equals("Success"))
                {
                    return Ok(result);
                }
                return BadRequest(result.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        #endregion

        #region ViewAllNotification
        [HttpGet]
        [Route("api/Notification/ViewAllNotification")]
        public IHttpActionResult ViewAllNotification(int UserID, int PageNo)
        {
            try
            {
                CommonMeta result;
                result = _notificationManager.ViewAllNotification(UserID, PageNo);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest(result.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        #endregion

        #region MarkAllRead
        [HttpGet]
        [Route("api/Notification/MarkAllRead")]
        public IHttpActionResult MarkAllRead(int UserID)
        {
            try
            {
                string result = null;
                result = _notificationManager.MarkAllRead(UserID);
                if (result.Equals("Success"))
                {
                    return Ok(result);
                }
                return BadRequest(result.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        #endregion

        #region MarkNotificationRead
        [HttpGet]
        [Route("api/Notification/MarkNotificationRead")]
        public IHttpActionResult MarkNotificationRead(int NotificationID)
        {
            try
            {
                string result = null;
                result = _notificationManager.MarkNotificationRead(NotificationID);
                if (result.Equals("Success"))
                {
                    return Ok(result);
                }
                return BadRequest(result.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        #endregion

        #region SendMobilePushNotification
        static void SendMobilePushNotification(Notification notificationObj) {
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAA9AMYlK0:APA91bECHZ_P_MVU0JvuA1U7gyJe-8TvnhT6TsduLxOd4y4g4PAFdJezDar5Nzr6QUmnTwbaxheSdiOsx6yafoTd4blNAgaKiIz7XJvK8btDHHfwzUjwTCwTAggRXjN4kbA2Hj_FbgVV"));
            //Sender Id - From firebase project setting  
            tRequest.Headers.Add(string.Format("Sender: id={0}", "1048023962797"));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = notificationObj.MobileNotificationToken,
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = notificationObj.TextData,
                    title = "JoClaims",
                    badge = 1,
                    sound = "default",
                    volume = 1
                },
                data = new
                {
                    RequestID = notificationObj.RequestID != null ? notificationObj.RequestID.ToString() : " ",
                    QuotationID = notificationObj.QuotationID != null ? notificationObj.QuotationID.ToString() : " ",
                    DemandID = notificationObj.DemandID != null ? notificationObj.DemandID.ToString() : " ",
                    MobileScreenName = notificationObj.MobileScreenName != null ?notificationObj.MobileScreenName.ToString() : " ",
                    NotificationID= notificationObj.MobileScreenName != null ? notificationObj.NotificationID.ToString():"",
                   
                }

            };

            string postbody = JsonConvert.SerializeObject(payload).ToString();
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                //result.Response = sResponseFromServer;
                            }
                    }
                }
            }
        }

        #endregion

        #region SendHuaweiPushNotification
        static void SendHuaweiPushNotification(Notification notificationObj)
        {
            WebRequest tRequest = WebRequest.Create("https://push-api.cloud.huawei.com/v1"+ string.Format("/{0}/messages:send", "103455901"));
            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            tRequest.Headers.Add(string.Format("Authorization: key={0}", "0f8a90fe81d86514b46cab0d1eae513a607a6d1d650435682d51dc082fdf650f"));
            //Sender Id - From firebase project setting  
            //tRequest.Headers.Add(string.Format("/{0}/messages:send", "513783486092412096"));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = notificationObj.MobileNotificationToken,
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = notificationObj.TextData,
                    title = "JoClaims",
                    badge = 1
                },
                data = new
                {
                    RequestID = notificationObj.RequestID != null ? notificationObj.RequestID.ToString() : " ",
                    QuotationID = notificationObj.QuotationID != null ? notificationObj.QuotationID.ToString() : " ",
                    DemandID = notificationObj.DemandID != null ? notificationObj.DemandID.ToString() : " ",
                    MobileScreenName = notificationObj.MobileScreenName != null ? notificationObj.MobileScreenName.ToString() : " ",
                    NotificationID = notificationObj.MobileScreenName != null ? notificationObj.NotificationID.ToString() : ""
                }

            };

            string postbody = JsonConvert.SerializeObject(payload).ToString();
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                //result.Response = sResponseFromServer;
                            }
                    }
                }
            }
        }

        #endregion

        #region SendApplePushNotification
        static void SendApplePushNotification(Notification notificationObj)
        {
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAA9AMYlK0:APA91bFei_YhQzudC-E-ixIEmrlBHc23Omzj3Idk8zxDlPe4AOKyJrUDhWU0AhLtPfRJWT0DiYi8XPsCgCR2f0bNp0Km3so9ageBZXb0cLYjp5ESFAq750NbURmgX80Z6qS6J_-8D2tH"));
            //Sender Id - From firebase project setting  
            tRequest.Headers.Add(string.Format("Sender: id={0}", "1048023962797"));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = notificationObj.MobileNotificationToken,
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = notificationObj.TextData,
                    title = "JoClaims",
                    badge = 1,
                    sound = "default",
                    volume = 1
                },
                data = new
                {
                    RequestID = notificationObj.RequestID != null ? notificationObj.RequestID.ToString() : " ",
                    QuotationID = notificationObj.QuotationID != null ? notificationObj.QuotationID.ToString() : " ",
                    DemandID = notificationObj.DemandID != null ? notificationObj.DemandID.ToString() : " ",
                    MobileScreenName = notificationObj.MobileScreenName != null ? notificationObj.MobileScreenName.ToString() : " ",
                    NotificationID = notificationObj.MobileScreenName != null ? notificationObj.NotificationID.ToString() : ""
                }

            };

            string postbody = JsonConvert.SerializeObject(payload).ToString();
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                //result.Response = sResponseFromServer;
                            }
                    }
                }
            }
        }

        #endregion

        #region ViewAllNotification
        [HttpGet]
        [Route("api/Notification/ViewAllNotificationTN")]
        public IHttpActionResult ViewAllNotificationTN(int UserID, int PageNo)
        {
            try
            {
                CommonMeta result;
                result = _notificationManager.ViewAllNotificationTN(UserID, PageNo);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest(result.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        #endregion
    }
}
