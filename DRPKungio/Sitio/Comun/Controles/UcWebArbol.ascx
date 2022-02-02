<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcWebArbol.ascx.cs" Inherits="Sitio.Comun.Controles.UcWebArbol" %>
<asp:Panel ID="ContenedorArbol" runat="server">
    <asp:TreeView ID="tvArbol" runat="server" ShowCheckBoxes="Leaf" Font-Names= "Arial" 
        ShowLines="true" ExpandDepth="2" ShowExpandCollapse="true" > </asp:TreeView>
</asp:Panel>

