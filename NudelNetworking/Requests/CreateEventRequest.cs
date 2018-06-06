﻿using Nudel.Networking.Requests.Base;
using System;
using System.Collections.Generic;

namespace Nudel.Networking.Requests
{
    public class CreateEventRequest : AuthenticatedRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public Tuple<double, double> Location { get; set; }
        public List<DateTime> Options { get; set; }

        public CreateEventRequest() { }

        public CreateEventRequest(
            string sessionToken,
            string title,
            string description,
            DateTime time,
            Tuple<double, double> location,
            List<DateTime> options
        ) : base(sessionToken)
        {
            Title = title;
            Description = description;
            Time = time;
            Location = location;
            Options = options;
        }
    }
}
