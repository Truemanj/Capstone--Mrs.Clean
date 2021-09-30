using System;
using System.Collections.Generic;
namespace MrsCleanCapstone.Actions
{
    public class AdminActions
    {
        public static KeyValuePair<string, string> ManageProducts = new KeyValuePair<string, string>("Manage Products", "Lorem Ipsum");
        public static KeyValuePair<string, string> ManageDeals = new KeyValuePair<string, string>("Manage Deals", "Lorem Ipsum");
        //public static KeyValuePair<string, string> ManageServices = new KeyValuePair<string, string>("Manage Services", "Lorem Ipsum");
        public static KeyValuePair<string, string> ManageBookings = new KeyValuePair<string, string>("Manage Bookings", "Lorem Ipsum");
        public static KeyValuePair<string, string> ViewFeedbacks = new KeyValuePair<string, string>("View Feedbacks", "Lorem Ipsum");

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
        //new AdminActions(ManageServices),
        new AdminActions(ManageBookings),
        new AdminActions(ViewFeedbacks)
    };

    }
}
