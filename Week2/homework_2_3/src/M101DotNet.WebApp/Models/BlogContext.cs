using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events.Diagnostics;

namespace M101DotNet.WebApp.Models
{
    public class BlogContext
    {
        public const string CONNECTION_STRING_NAME = "Blog";
        public const string DATABASE_NAME = "blog";
        public const string POSTS_COLLECTION_NAME = "posts";
        public const string USERS_COLLECTION_NAME = "users";

        // This is ok... Normally, these or the entire BlogContext
        // would be put into an IoC container. We aren't using one,
        // so we'll just keep them statically here as they are 
        // thread-safe.
        private static readonly IMongoClient _client;
        private static readonly IMongoDatabase _database;

        static BlogContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(DATABASE_NAME);
        }

        public IMongoClient Client
        {
            get { return _client; }
        }

        public IMongoCollection<User> Users
        {
            get { return _database.GetCollection<User>(USERS_COLLECTION_NAME); }
        }

        public void InsertUser(User user)
        {
            try
            {
                IMongoCollection<User> mongoCollection = _database.GetCollection<User>(USERS_COLLECTION_NAME);
                mongoCollection.InsertOneAsync(user);
            }
            catch (Exception exception)
            {
               //catch the exception while insert user into database
            }
            
        }

        public User GetUser(string email)
        {
            User user = null;
            try
            {
               
                IMongoCollection<User> mongoCollection = _database.GetCollection<User>(USERS_COLLECTION_NAME);
                BsonDocument searchBsonDocument =new BsonDocument("Email",email);
                

                if (mongoCollection != null)
                {


                    Task<User> firstOrDefaultAsync = mongoCollection.Find(searchBsonDocument).Project<User>(Builders<User>.Projection.Exclude(item=>item._id))
                        .FirstOrDefaultAsync();

                    user =new User();
                    user.Name = firstOrDefaultAsync.Result.Name;
                    user.Email = firstOrDefaultAsync.Result.Email;
                }
            }
            catch (Exception exception)
            {
                //catch the exception
            }

            return user;
        }
    }
}