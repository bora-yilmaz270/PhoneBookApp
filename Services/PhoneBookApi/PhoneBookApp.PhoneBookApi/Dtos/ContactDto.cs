﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PhoneBookApp.PhoneBookApi.Dtos
{
    public class ContactDto
    {      
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
    }


}
