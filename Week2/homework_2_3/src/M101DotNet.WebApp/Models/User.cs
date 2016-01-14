﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace M101DotNet.WebApp.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {

        public ObjectId _id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        // XXX WORK HERE
        // create an object suitable for insertion into the user collection
        // The homework instructions will tell you the schema that the documents 
        // must follow. Make sure to include Name and Email properties.
    }
}