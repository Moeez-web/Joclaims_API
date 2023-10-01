using BAL.Common;
using BAL.IManager;
using DAL;
using MODEL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Manager
{
    public class NotificationManger : INotificationManager
    {
        #region NotificationSent
        public string NotificationSent(int notificationID)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                {
                      new SqlParameter { ParameterName = "@NotificationID", Value = notificationID},
                };

                var result = ADOManager.Instance.ExecuteScalar("[verifyNotificationSent]", CommandType.StoredProcedure, sParameter);
                return result.Equals("Success") ? "Success" : result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
        #region ViewAllNotification
        public CommonMeta ViewAllNotification(int UserID, int PageNo)
        {
            DataSet dt = new DataSet();
            try
            {
                CommonMeta notificationData = new CommonMeta();
                
                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@UserID" , Value = UserID},
                        new SqlParameter { ParameterName = "@PageNo" , Value = PageNo},
                };

                using (dt = ADOManager.Instance.DataSet("[viewAllNotification]", CommandType.StoredProcedure, sParameter))
                {

                    notificationData.Notifications = dt.Tables[0].AsEnumerable().Select(n => new Notification
                    {
                        NotificationID = n.Field<int>("NotificationID"),
                        RecipientID = n.Field<int?>("RecipientID"),
                        TextData = n.Field<string>("TextData"),
                        IsSent = n.Field<bool?>("IsSent"),
                        CreatedOn = n.Field<DateTime>("CreatedOn"),
                        CreatedBy = n.Field<int>("CreatedBy"),
                        RedirectURL = n.Field<string>("RedirectURL"),
                        Icon = n.Field<string>("Icon"),
                        IsRead = n.Field<bool?>("IsRead"),
                        CreatedSince = n.Field<string>("CreatedSince"),
                        CreatedSinceArabic = n.Field<string>("CreatedSinceArabic"),
                        RoleID = n.Field<byte?>("RoleID"),
                        NotificationTypeId = n.Field<short?>("NotificationTypeID"),
                        RequestID = n.Field<int?>("RequestID"),
                        QuotationID = n.Field<int?>("QuotationID"),
                        DemandID = n.Field<int?>("DemandID"),
                        MobileScreenName = n.Field<string>("MobileScreenName"),
                        MobileNotificationToken = n.Field<string>("MobileNotificationToken"),
                        SupplierID = n.Field<int?>("SupplierID"),
                        TextArabicData = n.Field<string>("TextArabicData"),
                        // for navigation to offer detail screen decision in mobile view.
                        IsBiddingTimeExpired = n.Field<bool?>("IsBiddingTimeExpired"),
                        AppliedQuotationID = n.Field<int?>("AppliedQuotationID"),
                        DraftID = n.Field<int?>("DraftID"),
                        VIN = n.Field<string>("VIN"),
                        ExpiredConditionNumber = n.Field<int?>("ExpiredConditionNo")
                    }).ToList();

                    notificationData.NotificationPageCount = Convert.ToInt32(dt.Tables[1].Rows[0]["NotificationPageCount"]);

                }

                return notificationData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        #endregion


        #region MarkAllRead
        public string MarkAllRead(int UserID)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                {
                      new SqlParameter { ParameterName = "@UserID", Value = UserID },
                };

                var result = ADOManager.Instance.ExecuteScalar("[markAllRead]", CommandType.StoredProcedure, sParameter);
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region GetSingleNotification
        public List<Notification> GetSingleNotification()
        {
            try
            {
                DataSet dt = new DataSet();

                List<Notification> notifications = new List<Notification>();

                using (dt = ADOManager.Instance.DataSet("[getSingleNotification]", CommandType.StoredProcedure))
                {
                    notifications = dt.Tables[0].AsEnumerable().Select(n => new Notification
                    {
                        NotificationID = n.Field<int>("NotificationID"),
                        RecipientID = n.Field<int?>("RecipientID"),
                        TextData = n.Field<string>("TextData"),
                        IsSent = n.Field<bool?>("IsSent"),
                        CreatedOn = n.Field<DateTime>("CreatedOn"),
                        CreatedBy = n.Field<int>("CreatedBy"),
                        RedirectURL = n.Field<string>("RedirectURL"),
                        NotificationTypeId = n.Field<Int16?>("NotificationTypeId"),
                        Icon = n.Field<string>("Icon"),
                        NotificationTypeName = n.Field<string>("NotificationTypeName"),
                        IsRead = n.Field<bool?>("IsRead"),
                        CreatedSince = n.Field<string>("CreatedSince"),
                        TextArabicData = n.Field<string>("TextArabicData"),
                        RequestID = n.Field<int?>("RequestID"),
                        QuotationID = n.Field<int?>("QuotationID"),
                        DemandID = n.Field<int?>("DemandID"),
                        SupplierID = n.Field<int?>("SupplierID"),

                    }).ToList();

                    return notifications;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region MarkNotificationRead
        public string MarkNotificationRead(int NotificationID)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                {
                      new SqlParameter { ParameterName = "@NotificationID", Value = NotificationID },
                };

                var result = ADOManager.Instance.ExecuteScalar("[markNotificationRead]", CommandType.StoredProcedure, sParameter);
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region MarkNotificationsSent
        public string MarkNotificationsSent(List<Notification> Notifications)
        {
            try
            {
                var XMLNotifications = Notifications.ToXML("ArrayOfNotifications");
                var sParameter = new List<SqlParameter>
                {
                      new SqlParameter { ParameterName = "@XMLNotifications", Value = XMLNotifications },
                };

                var result = ADOManager.Instance.ExecuteScalar("[markNotificationsSent]", CommandType.StoredProcedure, sParameter);
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
        #region ViewAllNotificationTN
        public CommonMeta ViewAllNotificationTN(int UserID, int PageNo)
        {
            DataSet dt = new DataSet();
            try
            {
                CommonMeta notificationData = new CommonMeta();

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@UserID" , Value = UserID},
                        new SqlParameter { ParameterName = "@PageNo" , Value = PageNo},
                };

                using (dt = ADOManager.Instance.DataSet("[viewAllNotificationTN]", CommandType.StoredProcedure, sParameter))
                {

                    notificationData.Notifications = dt.Tables[0].AsEnumerable().Select(n => new Notification
                    {
                        NotificationID = n.Field<int>("NotificationID"),
                        RecipientID = n.Field<int?>("RecipientID"),
                        TextData = n.Field<string>("TextData"),
                        IsSent = n.Field<bool?>("IsSent"),
                        CreatedOn = n.Field<DateTime>("CreatedOn"),
                        CreatedBy = n.Field<int>("CreatedBy"),
                        RedirectURL = n.Field<string>("RedirectURL"),
                        Icon = n.Field<string>("Icon"),
                        IsRead = n.Field<bool?>("IsRead"),
                        CreatedSince = n.Field<string>("CreatedSince"),
                        CreatedSinceArabic = n.Field<string>("CreatedSinceArabic"),
                        RoleID = n.Field<byte?>("RoleID"),
                        NotificationTypeId = n.Field<short?>("NotificationTypeID"),
                        RequestID = n.Field<int?>("RequestID"),
                        QuotationID = n.Field<int?>("QuotationID"),
                        DemandID = n.Field<int?>("DemandID"),
                        MobileScreenName = n.Field<string>("MobileScreenName"),
                        MobileNotificationToken = n.Field<string>("MobileNotificationToken"),
                        SupplierID = n.Field<int?>("SupplierID"),
                        TextArabicData = n.Field<string>("TextArabicData"),
                        // for navigation to offer detail screen decision in mobile view.
                        IsBiddingTimeExpired = n.Field<bool?>("IsBiddingTimeExpired"),
                        AppliedQuotationID = n.Field<int?>("AppliedQuotationID"),
                        AccidentNo = n.Field<string>("AccidentNo"),
                        ObjectTypeID = n.Field<Int16?>("ObjectTypeID")

                    }).ToList();

                    notificationData.NotificationPageCount = Convert.ToInt32(dt.Tables[1].Rows[0]["NotificationPageCount"]);

                }

                return notificationData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        #endregion

    }
}
