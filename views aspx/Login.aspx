<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CalendarWithBaseASPMVC.Models.LoginForm>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Login</h2>

    <% 
        var userItemsList = new List<SelectListItem>();
        for (var i = 0; i < Model.PeopleArray.Count(); i++) {
            userItemsList.Add(new SelectListItem
            {
                Text = Model.PeopleArray[i].FirstName
                + " " + Model.PeopleArray[i].LastName,
                                                                  Value = i.ToString()
            });
        }
        
        var usersList = new SelectList(userItemsList, "Value", "Text");
    %>

    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                
                <h2>Chose existing person</h2>

                <p>
                <%: @Html.DropDownListFor(m => m.ChosenPersonIndex, usersList) %>
                </p>

                <p>
                    <input name="submitButton" type="submit" value="Choose person" />
                </p>

                <h2>Or create new person</h2>

                <div class="editor-label">
                    <%: Html.LabelFor(m => m.FirstName) %>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(m => m.FirstName) %>
                </div>
                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.LastName)%>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(m => m.LastName) %>
                </div>

                <p>
                    <input name="submitButton" type="submit" value="New person" />
                </p>

            </fieldset>
        </div>
    <% } %>

</asp:Content>
