using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;

[DataObject(false)]
public class ContactDAL : DALBase
{
    //metoder:
    //radera kontakt:
    public void DeleteContact(int contactId)
    {
        //skapar anslutningsobjekt:
        using (SqlConnection conn = CreateConnection())
        {
            try
            {
                //lagrade proceduren:
                var cmd = new SqlCommand("Person.uspRemoveContact", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //skickar med parametrar:
                cmd.Parameters.Add("ContactID", SqlDbType.Int, 4).Value = contactId;

                //öppnar anslutning:
                conn.Open();

                //exekverar:
                cmd.ExecuteNonQuery();

            }
            catch
            {
                throw new ApplicationException("Ett fel uppstod i dataåtkomstlagret");
            }

        }

    }

    //hämta kontakt via ID:
    public Contact GetContactById(int contactId)
    {
        using (SqlConnection conn = CreateConnection())
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Person.uspGetContact", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // lägger till parameter:
                cmd.Parameters.AddWithValue("@CustomerId", contactId);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Om det finns en post att läsa returnerar Read true. Finns ingen post returnerar
                    // Read false.
                    if (reader.Read())
                    {
                        var contactIdIndex = reader.GetOrdinal("ContactId");
                        var firstNameIndex = reader.GetOrdinal("FirstName");
                        var lastNameIndex = reader.GetOrdinal("LastName");
                        var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                        // Returnerar referensen till de skapade Contact-objektet.
                        return new Contact
                        {
                            ContactId = reader.GetInt32(contactIdIndex),
                            FirstName = reader.GetString(firstNameIndex),
                            LastName = reader.GetString(lastNameIndex),
                            EmailAddress = reader.GetString(emailAddressIndex)
                        };
                    }
                }

                // om kontakt inte hittas:
                return null;
            }
            catch
            {
                throw new ApplicationException("An error occured in the data access layer.");
            }
        }
    }

	public List<Contact> GetContacts()
    {
        var contacts = new List<Contact>(100);

        //skapar anslutningsobjekt:
        using (var conn = CreateConnection())
        {
            try
            {

                var cmd = new SqlCommand("Person.uspGetContacts", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var contactIdIndew = reader.GetOrdinal("ContactId");
                    var firstNameIndex = reader.GetOrdinal("FirstName");
                    var lastNameIndex = reader.GetOrdinal("LastName");
                    var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                    while (reader.Read())
                    {
                        contacts.Add(new Contact
                        {
                            ContactId = reader.GetInt32(contactIdIndew),
                            FirstName = reader.GetString(firstNameIndex),
                            LastName = reader.GetString(lastNameIndex),
                            EmailAddress = reader.GetString(emailAddressIndex)
                        });

                    }
                }
            }
            catch
            {
                throw new ApplicationException("Ett fel uppstod i dataåtkomstlagret");
            }

        }

        contacts.TrimExcess();

        return contacts;
    }

    public void InsertContact(Contact contact)
    {
       
        //skapar anslutningsobjekt:
        using (var conn = CreateConnection())
        {
            try
            {
                //lagrade proceduren:
                var cmd = new SqlCommand("Person.uspAddContact", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //skickar med parametrar:
                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = contact.FirstName;
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = contact.LastName;
                cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = contact.EmailAddress;
                //primärnyckeln med dess värde: (tas om hand efter exekveringen)
                cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                
                //öppnar anslutning:
                conn.Open();

                //exekverar:
                cmd.ExecuteNonQuery();

                //tar hand om objektet och typomvandlar till int:
                contact.ContactId = (int)cmd.Parameters["@ContactID"].Value;

            }
            catch
            {
                throw new ApplicationException("Ett fel uppstod i dataåtkomstlagret");
            }

        }

    }

    public void UpdateContact(Contact contact)
    {
        //skapar anslutningsobjekt:
        using (var conn = CreateConnection())
        {
            try
            {
                //lagrade proceduren:
                var cmd = new SqlCommand("Person.uspUpdateContact", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //skickar med parametrar:
                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = contact.FirstName;
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = contact.LastName;
                cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = contact.EmailAddress;
                //primärnyckeln med dess värde ändras inte:
                cmd.Parameters.Add("ContactID", SqlDbType.Int, 4).Value = contact.ContactId;

                //öppnar anslutning:
                conn.Open();

                //exekverar:
                cmd.ExecuteNonQuery();

            }
            catch
            {
                throw new ApplicationException("Ett fel uppstod i dataåtkomstlagret");
            }

        }

    }

}