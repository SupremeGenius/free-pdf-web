<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequiredTextBox.ascx.cs" Inherits="FreePDF.UserControl.RequiredTextBox" %>
<asp:TextBox ID="input" CssClass="outerglow_textbox" runat="server"></asp:TextBox>
<asp:RequiredFieldValidator ID="validator" runat="server" ControlToValidate="input" Display="None"></asp:RequiredFieldValidator>