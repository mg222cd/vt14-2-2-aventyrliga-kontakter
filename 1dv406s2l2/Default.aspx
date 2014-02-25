<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" ViewStateMode="Disabled" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Äventyrliga kontakter</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.5.2/jquery.min.js"></script>
    <script type="text/javascript" src="jquery.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="page">
        <div id="header">
            <div id="header_wrapper">
                <ul id="top">
                    <li>1DV406, ASP.NET WebForms</li>
                    <li>Äventyrliga kontakter</li>
                    <li class="last">Steg 2, labb 2</li>
                </ul>
                <h1 id="logo_text">
                    Äventyrliga kontakter
                </h1>
                <%-- UPPLADNINGS-MEDDELANDE --%>
                <asp:Label ID="LabelUpload" runat="server" Text="" Visible="false" CssClass="resultlabel">
                </asp:Label>
            </div>
                
            <div id="menu_wrapper" />
        </div>
        <div id="main">
        <%-- OBJECT DATA SOURCE --%>
        <%-- hämtar kontaktuppgifter samt deklarerar vilka metoder som anropas vid INSERT, UPDATE, DELETE --%>
        <asp:ObjectDataSource ID="ContactObjectDataSource" runat="server" SelectMethod="GetContacts" 
            TypeName="Service" DataObjectTypeName="Contact" DeleteMethod="DeleteContact"
            InsertMethod="SaveContact" UpdateMethod="SaveContact" OnSelected="ContactObjectDataSource_Selected"
                OnInserting="ContactObjectDataSource_Inserting" OnInserted="ContactObjectDataSource_Inserted"
                OnUpdating="ContactObjectDataSource_Updating" OnUpdated="ContactObjectDataSource_Updated"
                OnDeleted="ContactObjectDataSource_Deleted" />
        <%-- VALIDATION SUMMARY --%>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Fel inträffade. Korrigera och försök igen." />
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" HeaderText="Fel inträffade. Korrigera och försök igen." ValidationGroup="vgInsert" />    
        <%-- LIST VIEW --%>
        <%-- visar kontakterna och innehåller kommandoknappar --%>
        <asp:ListView ID="ContactListView" runat="server" DataSourceID="ContactObjectDataSource"
            DataKeyNames="ContactId" InsertItemPosition="FirstItem" >
            <LayoutTemplate>
                <table class="grid">
                    <tr>
                        <th>
                            Förnamn
                        </th>
                        <th>
                            Efternamn
                        </th>
                        <th>
                            E-post
                        </th>
                        <th>
                        </th>
                    
                    </tr>
                    <%-- Platshållare för nya rader --%>
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <%-- Mall för rader --%>
                <tr>
                    <td>
                        <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="EmailAddressLabel" runat="server" Text='<%# Eval("EmailAddress") %>' />
                    </td>
                    <td class="command">
                        <%-- Kommandoknappar Redigera/ Ta bort --%>
                        <%--delete --%>
                        <asp:LinkButton ID="DeleteLinkButton" runat="server" CommandName="Delete" Text="Ta bort"
                        CauseValidation="false"  OnClientClick='<%# String.Format("return confirm(\"Ta bort kunden &rsquo;{0} {1}&rsquo; permanent?\" )", Eval("FirstName"), Eval("LastName")) %>' />
                        <%--redigera --%>
                        <asp:LinkButton ID="EditLinkButton" runat="server" CommandName="Edit" Text="Redigera" CausesValidation="false" />
                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
            <%-- Visas då kontaktuppgift saknas i tabellen: --%>
                <table class="grid">
                    <tr>
                        <td>
                           Kontaktuppgifter saknas.
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <InsertItemTemplate>
                <%-- Mall för rad där man lägger till ny kontakt --%>
                <tr>
                    <td>
                        <%-- textbox --%>
                        <asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%# Bind("FirstName") %>' MaxLength="50"
                        ValidationGroup="VgInsert" />
                        <%-- validering --%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ett förnamn måste anges."
                                Text="*" ControlToValidate="FirstNameTextBox" Display="Dynamic" CssClass="field-validation-error"
                                ValidationGroup="vgInsert" />
                    </td>
                    <td>
                        <%-- textbox --%>
                        <asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>' MaxLength="50"
                        ValidationGroup="VgInsert" />
                        <%-- validering --%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Ett efternamn måste anges."
                                Text="*" ControlToValidate="LastNameTextBox" Display="Dynamic" CssClass="field-validation-error"
                                ValidationGroup="vgInsert" />
                    </td>
                    <td>
                        <%-- textbox --%>
                        <asp:TextBox ID="EmailAddressTextBox" runat="server" Text='<%# Bind("EmailAddress") %>' MaxLength="50"
                        ValidationGroup="VgInsert" />
                        <%-- validering --%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="En email-adress måste anges."
                                Text="*" ControlToValidate="EmailAddressTextBox" Display="Dynamic" CssClass="field-validation-error"
                                ValidationGroup="vgInsert" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Email-adressen verkar inte vara korrekt."
                                ControlToValidate="EmailAddressTextBox" Display="Dynamic" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                CssClass="field-validation-error" ValidationGroup="vgInsert">*</asp:RegularExpressionValidator>
                    </td>
                    <td>
                    <%-- ny kolumn med kommandoknappar: --%>
                    <asp:LinkButton ID="InsertLinkButton" runat="server" CommandName="Insert" Text="Spara" ValidationGroup="vgInsert" />
                    <asp:LinkButton ID="CancelLinkButton" runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                    </td>
                </tr>
            </InsertItemTemplate>
            <EditItemTemplate>
                <%-- Mall för rad att redigera befintlig kontakt --%>
                <tr>
                     <td>
                        <%-- textbox --%>
                        <asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%# Bind("FirstName") %>' MaxLength="50" />
                        <%-- validering --%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ett förnamn måste anges."
                                Text="*" ControlToValidate="FirstNameTextBox" Display="Dynamic" CssClass="field-validation-error" />
                    </td>
                    <td>
                        <%-- textbox --%>
                        <asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>' MaxLength="50" />
                        <%-- validering --%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Ett efterförnamn måste anges."
                                Text="*" ControlToValidate="LastNameTextBox" Display="Dynamic" CssClass="field-validation-error" />
                    </td>
                    <td>
                        <%-- textbox --%>
                        <asp:TextBox ID="EmailAddressTextBox" runat="server" Text='<%# Bind("EmailAddress") %>' MaxLength="50" />
                        <%-- validering --%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="En email-adress måste anges."
                                Text="*" ControlToValidate="EmailAddressTextBox" Display="Dynamic" CssClass="field-validation-error"
                                ValidationGroup="vgInsert" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Email-adressen verkar inte vara korrekt."
                                ControlToValidate="EmailAddressTextBox" Display="Dynamic" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                CssClass="field-validation-error" ValidationGroup="vgInsert">*</asp:RegularExpressionValidator>
                    </td>
                    <%-- kommandoknappar --%>
                    <td>
                    <asp:LinkButton ID="UpdateLinkButton" runat="server" CommandName="Update" Text="Spara" />
                    <asp:LinkButton ID="CancelLinkButtom" runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                    </td>
                </tr>
            </EditItemTemplate>
        </asp:ListView>
                    <%-- Paging --%>
                    <asp:DataPager runat="server" ID="ContactsPager" PageSize="20" PagedControlID="ContactListView">
                        <Fields>
                             <asp:NumericPagerField ButtonType="Link"  NextPageText="&gt;" PreviousPageText="&lt;" />
                        </Fields>
                     </asp:DataPager>
        </div>
        <div id="footer">
        Marike Grinde, mg222cd
        </div>
    </div>
    </form>
</body>
</html>
