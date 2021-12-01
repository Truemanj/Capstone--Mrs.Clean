using System;
using System.Collections.Generic;
namespace MrsCleanCapstone.Actions
{
    public class AdminActions
    {
        public static KeyValuePair<string, string> ManageProducts = new KeyValuePair<string, string>("Manage Products", "Add, Edit, or Delete products from the application");
        public static KeyValuePair<string, string> ManageDeals = new KeyValuePair<string, string>("Manage Deals", "Add, Edit, or Delete deals from the application");
        public static KeyValuePair<string, string> ManageBookings = new KeyValuePair<string, string>("Manage Bookings", "Add, Edit, or Delete booked appointments from the application");
        public static KeyValuePair<string, string> ViewFeedbacks = new KeyValuePair<string, string>("View Feedback", "Add, Edit, or Delete feedback from the application");

        private AdminActions(KeyValuePair<string, string> kvp)
        {
            Name = kvp.Key;
            Description = kvp.Value;
        }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public static List<AdminActions> Actions = new List<AdminActions>(){

        new AdminActions(ManageProducts),
        new AdminActions(ManageDeals),
        new AdminActions(ManageBookings),
        new AdminActions(ViewFeedbacks)
    };

    }
}
