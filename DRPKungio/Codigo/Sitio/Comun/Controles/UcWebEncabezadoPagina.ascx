<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcWebEncabezadoPagina.ascx.cs" Inherits="Sitio.Comun.Controles.UcWebEncabezadoPagina" %>
<%--<span class= "MenuLogo  icon-windows " > </span>--%>
<img id="idLogo" runat="server"  class="MenuLogo" src="/img/logo/logoKungio10.png" />
<span class= "MenuTitulo " > Portal Web KunGio </span>
<%--<asp:LinkButton ID="IdBotonElementosSeguridad"  ClientIDMode="Static"   CssClass="ElementoCerrarSesion icon-off" OnClick="IdBotonElementosSeguridad_Click" runat="server">  </asp:LinkButton>--%>
<div id="IdContenedorSeguridad" ClientIDMode="Static"  class= "ContenedorSeguridad"  runat="server"  >
        <div  id="ContenedorUsuario" class= "ContenedorElementoSeguridad" runat="server" ClientIDMode="Static"  >
           <span class= "ContenedorIconoSeguridad icon-user" >  </span>
            <div id="NombreUsuario"  class= "ContenedorTextoSeguridad" runat="server" ClientIDMode="Static" >
           
            </div>
        </div>
        <div  id="ContenedorPerfil" class= "ContenedorElementoSeguridad" runat="server" ClientIDMode="Static"    >
            <span class= "ContenedorIconoSeguridad icon-users" > </span>
            <div id="NombrePerfil" class= "ContenedorTextoSeguridad" runat="server" ClientIDMode="Static" >
           
            </div>
        </div>
</div>
<span id="IdBotonMenu"  class="MenuBotonActivacion icon-window-minimize">  </span>
<asp:LinkButton ID="IdBotonElementosSeguridad"  ClientIDMode="Static"   CssClass="ElementoCerrarSesion icon-off" OnClick="IdBotonElementosSeguridad_Click" runat="server">  </asp:LinkButton>