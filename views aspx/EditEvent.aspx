<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CalendarWithBaseASPMVC.Models.DayEventForm>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditEvent
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>EditEvent</h2>

    <% 
        var hourItemsList = new List<SelectListItem>();
        for (var i = 0; i < 24; i++) {
            hourItemsList.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
        }
        
        var hourList = new SelectList(hourItemsList, "Value", "Text");

        var minuteItemsList = new List<SelectListItem>();
        for (var i = 0; i < 60; i++)
        {
            minuteItemsList.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
        }

        var minuteList = new SelectList(minuteItemsList, "Value", "Text");
    %>

    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                <legend>Day Event Information</legend>
                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.Description) %>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(m => m.Description) %>
                    <%: Html.ValidationMessageFor(m => m.Description) %>
                </div>
                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.StartHour) %>
                </div>

                <%: @Html.DropDownListFor(m => m.StartHour, hourList) %>

                <div class="editor-label">
                    <%: Html.LabelFor(m => m.StartMinute) %>
                </div>

                <%: @Html.DropDownListFor(m => m.StartMinute, minuteList) %>

                <div class="editor-label">
                    <%: Html.LabelFor(m => m.EndHour) %>
                </div>

                <%: @Html.DropDownListFor(m => m.EndHour, hourList) %>

                <div class="editor-label">
                    <%: Html.LabelFor(m => m.EndMinute) %>
                </div>

                <%: @Html.DropDownListFor(m => m.EndMinute, minuteList) %>

                <p></p>

                <p >
                    <input name="submitButton" type="submit" value="Edit event" />
                </p>

                <p></p>

                <p>
                    <input name="submitButton" type="submit" value="Remove event" />
                </p>
            </fieldset>
        </div>
    <% } %>

</asp:Content>
