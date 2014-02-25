using System.Collections.Generic;
using System.ComponentModel;

public abstract class BusinessObjectBase : IDataErrorInfo
{
	//FÄLT:
    private Dictionary<string, string> _validationErrors;
    protected string CommonErrorMessage;

    //EGENSKAPER:
    //ValidationErrors
    protected Dictionary<string, string> ValidationErrors
    {
        get
        {
        //då _validationErrors refererar null skapas ett nytt objekt:
            return this._validationErrors ?? (this._validationErrors = new Dictionary<string, string>());
        }
    }

    //IsValid
    public bool IsValid
    {
        //om inga fel finns är objektet tomt:
        get
        {
            return this.ValidationErrors.Count == 0;
        }
    }

    //Error
    public string Error
    {
        get 
        { 
            //kollar om fel finns och returnerar felbeskrivning eller ingenting:
            if (!this.IsValid)
            {
                return this.CommonErrorMessage;
            }
            return null;
        }
    }

    //This, frågar vilken egenskap som är fel:
    public string this[string propertyName]
    {
        get 
        {
            //kollar om fel finns och returnerar felmeddelande eller ingenting:
            string error;
            if (this.ValidationErrors.TryGetValue(propertyName, out error))
            {
                return error;
            }
            return null;
        }
    }

    //METODER/ KONSTRUKTOR:
    public BusinessObjectBase()
        : this("Objektets status är inte giltigt")
    {
        //tom
    }

    public BusinessObjectBase(string commonErrorMessage)
    {
        this.CommonErrorMessage = commonErrorMessage;
    }

}