using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    
    //EVENTS FÖR OBJECTDATASOURCE:
    //selected:
    protected void ContactObjectDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        //om ett undantag kastats visas ett felmeddelande och ramverket meddelas att undantaget är omhändertaget:
        if (e.Exception != null)
        {
            AddErrorMessage("Ett fel inträffade då kontakterna hämtades.");
            ContactListView.Visible = false;
            e.ExceptionHandled = true;
        }
    }

    //inserting:
    protected void ContactObjectDataSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //inmatat färde som typomvandlas till Contact-objekt:
        var contact = e.InputParameters[0] as Contact;

        //om objektet är null eller felaktigt stoppas händelsen INSERT från att fortsätta:
        if (contact == null)
        {
            AddErrorMessage("Ett oväntat fel inträffade då kontakten skulle läggas till.");
            e.Cancel = true;
        }
        else if (!contact.IsValid)
        {
            AddErrorMessage(contact);
            e.Cancel = true;
        }
    }

    //inserted:
    protected void ContactObjectDataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        //om undantag kastats visas felmeddelande och ramverket meddelas att undantaget är omhändetaget:
        if (e.Exception != null)
        {
            AddErrorMessage("Ett fel inträffade då kontakten skulle läggas till. HÄR");
            e.ExceptionHandled = true;
        }
        else
        {
            LabelUpload.Visible = true;
            LabelUpload.Text = "Ny kontakt lades till!";
        }
    }

    //updating:
    protected void ContactObjectDataSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //inmatat värde som typomvandlas till Contact-objekt:
        var contact = e.InputParameters[0] as Contact;

        //om objektet är null eller felaktigt stoppas händelsen UPDATE från att fortsätta:
        if (contact == null)
        {
            AddErrorMessage("Ett oväntat fel inträffade då kontakten skulle uppdateras");
            e.Cancel = true;
        }
        else if (!contact.IsValid)
        {
            AddErrorMessage(contact);
            e.Cancel = true;
        }
    }

    //updated:
    protected void ContactObjectDataSource_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        //om undantag kastats visas felmeddelande och ramverket meddelas att undantaget är omhändetaget:
        if (e.Exception != null)
        {
            AddErrorMessage("Ett fel inträffade då kontakten skulle uppdateras.");
            e.ExceptionHandled = true;
        }
        else
        {
            LabelUpload.Visible = true;
            LabelUpload.Text = "Kontakt uppdaterades!";
        }
    }

    //deleted:
    protected void ContactObjectDataSource_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        //om undantag kastats visas felmeddelande och ramverket meddelas att undantaget är omhändetaget:
        if (e.Exception != null)
        {
            AddErrorMessage("Ett fel inträffade då kontakten skulle raderas.");
            e.ExceptionHandled = true;
        }
        else
        {
            LabelUpload.Visible = true;
            LabelUpload.Text = "Kontakt raderades!";
        }
    }

    //HJÄLPMETODER:
    //AddErrorMessage - Skapar CustomValidator
    private void AddErrorMessage(string message)
    {
        var validator = new CustomValidator
        {
            IsValid = false,
            ErrorMessage = message
        };
        Page.Validators.Add(validator);
    }

    //AddErrorMessage - går igenom egenskaper, om felmeddelande finns läggs det till ValidationSummary
    private void AddErrorMessage(IDataErrorInfo obj)
    {
        //går igenom samtliga egenskaper och hämtar ut egenskap som ev. har felmeddelande:
        var propertyNames = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => String.IsNullOrWhiteSpace(p.Name))
            .Select(p => p.Name);
        foreach (var propertyName in propertyNames)
        {
            //överför felmeddelande till ValidatorCollection:
            AddErrorMessage(obj[propertyName]);
        }
    }

}