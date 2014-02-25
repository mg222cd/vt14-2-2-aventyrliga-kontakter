using System;
using System.Collections.Generic;
using System.ComponentModel;

[DataObject(true)]
public class Service
{
    //fält:
    private ContactDAL _contactDAL;

    //egenskap:
    private ContactDAL ContactDAL
    {
        get 
        { 
            return this._contactDAL ?? (this._contactDAL = new ContactDAL());
        }
    }

    //metoder:
    //radera kontakt:
    public void DeleteContact(Contact contact)
    {
        ContactDAL.DeleteContact(contact.ContactId);
    }

    //hämta kontakt:
    public Contact GetContact(int contactId)
    {
        return ContactDAL.GetContactById(contactId);
    }

    //hämta alla kontakter:
    public List<Contact> GetContacts()
    {
        return ContactDAL.GetContacts();
    }

    //spara kontakt:
    public void SaveContact(Contact contact)
    {
        //kontrollerar valideringen:
        if (contact.IsValid)
        {
            //kollar om det ær en ny post (=0) eller om det ær en befintlig, dvs om insert eller update ska göras:
            if (contact.ContactId == 0)
            {
                ContactDAL.InsertContact(contact);
            }
            else
            {
                ContactDAL.UpdateContact(contact);
            }
        }
        //om inmatade värden inte klarar valideringen:
        else
        {
            //ett allmänt undantag kastas, med ref. till objektet som ej klarade valideringen:
            ApplicationException ex = new ApplicationException(contact.Error);
            ex.Data.Add("Contact", contact);
            throw ex;
        }
    }



   
}