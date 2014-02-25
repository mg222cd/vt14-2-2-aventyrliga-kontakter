using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

[DataObject(false)]
[Serializable]
public class Contact : BusinessObjectBase
{
	//fält:
    private string _emailAddress;
    private string _firstName;
    private string _lastName;

    //konstruktor:
    public Contact()
    {
        this.EmailAddress = null;
        this.FirstName = null;
        this.LastName = null;
    }

    //egenskaper:
    public int ContactId { get; set; }

    public string EmailAddress 
    {
        get { return this._emailAddress; }
        set
        {
            //först - utgår från att värdet är rätt:
            this.ValidationErrors.Remove("EmailAddress");

            //undersöker inmatat värde:
            //om det är tomt:
            if (String.IsNullOrWhiteSpace(value))
            {
                this.ValidationErrors.Add("EmailAddress", "En email-adress måste anges.");
            }
            //om det är felaktigt:
            else if (!Regex.IsMatch(value, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
            {
                this.ValidationErrors.Add("EmailAddress", "Angiven email-adress är inte korrekt.");
            }
            // om det är för långt:
            else if (value.Length > 50)
            {
                this.ValidationErrors.Add("EmailAddress", "Email-adressen får ej vara längre än 50 tecken.");
            }
            //tilldelar inmatat värde, oavsett om det är rätt eller ej:
            this._emailAddress = value != null ? value.Trim() : null;
        }
    }
    
    public string FirstName 
    {
        get { return this._firstName; } 
        set
        {
            //först - utgår från att värdet är rätt:
            this.ValidationErrors.Remove("FirstName");

            //undersöker inmatat värde:
            //om det är tomt:
            if (String.IsNullOrWhiteSpace(value))
            {
                this.ValidationErrors.Add("FirstName", "Ett förnamn måste anges.");
            }
            //om det är för långt:
            else if (value.Length > 50)
            {
                this.ValidationErrors.Add("FirstName", "Förnamnet får inte vara längre än 50 tecken.");
            }
            //tilldelar inmatat värde, oavsett om det är rätt eller ej:
            this._firstName = value != null ? value.Trim() : null;
        }
    }

    public string LastName
    {
        get { return this._lastName; }
        set
        {
            //först - utgår från att värdet är rätt:
            this.ValidationErrors.Remove("LastName");

            //undersöker inmatat värde:
            //om det är tomt:
            if (String.IsNullOrWhiteSpace(value))
            {
                this.ValidationErrors.Add("LastName", "Ett efternamn måste anges");
            }
            //om det är för långt:
            else if (value.Length > 50)
            {
                this.ValidationErrors.Add("LastName", "Efternamnet får inte vara längre än 50 tecken.");
            }
            //tilldelar inmatat värde, oavsett om det är rätt eller ej:
            this._lastName = value != null ? value.Trim() : null;
        }
    }
   
}