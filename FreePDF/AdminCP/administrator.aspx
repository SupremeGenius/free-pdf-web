<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminCP.Master" AutoEventWireup="true" CodeBehind="administrator.aspx.cs" Inherits="FreePDF.AdminCP.administrator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="js_embed" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" runat="server">
    <% if (Session["AdminUser"] != null) { Response.Write("Welcome, " + Session["AdminUser"].ToString()); } %>
</asp:Content>