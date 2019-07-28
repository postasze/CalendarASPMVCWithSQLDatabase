<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CalendarWithBaseASPMVC.Models.Calendar>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Calendar
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Calendar</h2>
<h2><%: ViewData["Message"] %></h2>
<h2><%: ViewData["Message1"] %></h2>

<% using (Html.BeginForm()) { %>
<table width="100%">
  <colgroup>
    <col/>
    <col span="7"/>
    <col/>
  </colgroup>
  <tr>
    <td><input style="left: 0px;top: 0px;display:block;width:100%;height:100%;background-color: #98e6e6;border: solid 1px #e8eef4;   
    font-family: Arial;text-align:center" name="changeWeekButton" type="submit" value="prev" /></td>    
    <th>Monday</th>
    <th>Tuesday</th>
    <th>Wednesday</th>
    <th>Thursday</th>
    <th>Friday</th>
    <th>Saturday</th>
    <th>Sunday</th>
    <td><input style="left: 0px;top: 0px;display:block;width:100%;height:100%;background-color: #98e6e6;border: solid 1px #e8eef4;   
    font-family: Arial;text-align:center" name="changeWeekButton" type="submit" value="prev" /></td>
  </tr>
  <tr>
    <th><%: Model.WeekLabelsArray.GetValue(0) %></th>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(0) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[0].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 0, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[0][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 0}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(1) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[1].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 1, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[1][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 1}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(2) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[2].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 2, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[2][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 2}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(3) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[3].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 3, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[3][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 3}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(4) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[4].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 4, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[4][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 4}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(5) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[5].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 5, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[5][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 5}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(6) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[6].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 6, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[6][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 6}) %>'">Add Event</td></tr>
    </table>
    </td>
    <th><%: Model.WeekLabelsArray.GetValue(0) %></th>
  </tr>
  <tr>
    <th><%: Model.WeekLabelsArray.GetValue(1) %></th>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(7) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[7].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 7, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[7][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 7}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(8) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[8].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 8, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[8][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 8}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(9) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[9].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 9, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[9][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 9}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(10) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[10].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 10, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[10][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 10}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(11) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[11].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 11, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[11][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 11}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(12) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[12].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 12, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[12][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 12}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(13) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[13].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 13, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[13][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 13}) %>'">Add Event</td></tr>
    </table>
    </td>
    <th><%: Model.WeekLabelsArray.GetValue(1) %></th>
  </tr>
  <tr>
    <th><%: Model.WeekLabelsArray.GetValue(2) %></th>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(14) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[14].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 14, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[14][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 14}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(15) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[15].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 15, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[15][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 15}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(16) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[16].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 16, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[16][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 16}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(17) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[17].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 17, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[17][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 17}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(18) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[18].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 18, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[18][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 18}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(19) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[19].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 19, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[19][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 19}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(20) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[20].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 20, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[20][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 20}) %>'">Add Event</td></tr>
    </table>
    </td>
    <th><%: Model.WeekLabelsArray.GetValue(2) %></th>
  </tr>
  <tr>
    <th><%: Model.WeekLabelsArray.GetValue(3) %></th>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(21) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[21].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 21, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[21][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 21}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(22) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[22].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 22, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[22][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 22}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(23) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[23].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 23, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[23][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 23}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(24) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[24].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 24, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[24][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 24}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(25) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[25].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 25, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[25][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 25}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(26) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[26].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 26, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[26][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 26}) %>'">Add Event</td></tr>
    </table>
    </td>
    <td>
    <table>
      <tr>
        <th style="width:100%;height:100%;background-color: #adebad;font-family: Arial;font-weight:normal">
        <%: Model.DayLabelsArray.GetValue(27) %>
        </th>
      </tr>
      <% for (var i = 0; i < Model.dayEventLabelsLists[27].Count; i++) { %>
        <tr><td onclick="location.href='<%: Url.Action("EditEvent", "Calendar", new {chosenDayNumber = 27, chosenEventNumber = @i}) %>'"><%: Model.dayEventLabelsLists[27][@i]%></td></tr>
      <% } %>
      <tr><td style="width:100%;height:100%;background-color: #ffffb3;font-family: Arial;font-weight:normal" onclick="location.href='<%: Url.Action("AddEvent", "Calendar", new {chosenDayNumber = 27}) %>'">Add Event</td></tr>
    </table>
    </td>
    <th><%: Model.WeekLabelsArray.GetValue(3) %></th>
  </tr>
  <tr>
    <td><input style="left: 0px;top: 0px;display:block;width:100%;height:100%;background-color: #98e6e6;border: solid 1px #e8eef4;   
    font-family: Arial;text-align:center" name="changeWeekButton" type="submit" value="next" /></td>
    <th>Monday</th>
    <th>Tuesday</th>
    <th>Wednesday</th>
    <th>Thursday</th>
    <th>Friday</th>
    <th>Saturday</th>
    <th>Sunday</th>
    <td><input style="left: 0px;top: 0px;display:block;width:100%;height:100%;background-color: #98e6e6;border: solid 1px #e8eef4;   
    font-family: Arial;text-align:center" name="changeWeekButton" type="submit" value="next" /></td>
  </tr>
</table>

<% } %>
</asp:Content>

