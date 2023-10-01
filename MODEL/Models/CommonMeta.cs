using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class CommonMeta
    {
        public List<City> Cities { get; set; }
        public List<Roles> Roles { get; set; }
        public List<ObjectType> ObjectTypes { get; set; }
        public List<ICWorkshop> ICWorkshops { get;  set;}
        public Employee EmployeeObj { get; set; }
        public ShubeddakUser ShubeddakUserObj { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<Permission> UserPermissions { get; set; }
        public List<FeaturePermission> featurePermissions { get; set; }
        public List<Make> Makes { get; set; }
        public List<Model> Models { get; set; }
        public List<Year> Years { get; set; }
        public int UnReadNotification { get; set; }
        // for notification screen pagination
        public int NotificationPageCount { get; set; }
        public List<DamagePoint> DamagePoints { get; set; }
        public List<VehicleGroups> GroupName { get; set; }
        public List<IndividualReturn> IndividualReturns { get; set; }
        public int CompanyID { get; set; }
        public int TrNotificationCount { get; set; }


        public List<JoclaimsSetting>  joclaimsSetting { get; set; }
        public List<Country>  Countries { get; set; }

        
    }
}
