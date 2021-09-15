using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
    public class TagRepository : DatabaseConnector, IRepository<Tag>
    {
        public TagRepository(string connectionString) : base(connectionString) { }

        public List<Tag> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id, Name FROM Tag";
                    List<Tag> tags = new List<Tag>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Tag tag = new Tag()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                        };
                        tags.Add(tag);
                    }

                    return tags;
                }
            }
        }

        public Tag Get(int id)
        {

            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT t.Id AS TagID,
                                              t.Name
                                         FROM Tag t";

                    cmd.Parameters.AddWithValue("@id", id);


                    Tag tag = null;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (tag == null)
                        {
                            tag = new Tag()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("TagId")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                            };
                        }
                    }
                    return tag;
                }

            }
        }
        public void Insert(Tag tag)
        {
            throw new NotImplementedException();
        }

        public void Update(Tag tag)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
