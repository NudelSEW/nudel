﻿using Nudel.BusinessObjects;
using System;
using System.Collections.Generic;

namespace Nudel.Backend
{
    public class NudelService
    {
        public NudelService() => throw new NotImplementedException();

        public void Register(string username, string email, string password, string firstName, string lastName) => throw new NotImplementedException();
        public string Authenticate(string usernameOrEmail, string password) => throw new NotImplementedException();
        public void CreateEvent(Event @event) => throw new NotImplementedException();
        public Event FindEvent(long id) => throw new NotImplementedException();
        public List<Event> FindEvents(string title) => throw new NotImplementedException();
        public void SubscribeEvent(Event @event) => throw new NotImplementedException();
        public User FindUser(long id) => throw new NotImplementedException();
        public User FindUser(string usernameOrEmail) => throw new NotImplementedException();
        public void NotifyUser(Event @event, User user) => throw new NotImplementedException();
        public void NotifyUsers(Event @event, List<User> user) => throw new NotImplementedException();
    }
}
