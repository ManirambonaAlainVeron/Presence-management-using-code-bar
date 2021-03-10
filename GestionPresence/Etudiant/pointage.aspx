<%@ Page Title="" Language="C#" MasterPageFile="~/Etudiant/EtudiantMasterPage.Master" AutoEventWireup="true" CodeBehind="pointage.aspx.cs" Inherits="GestionPresence.Etudiant.pointage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-top: 50px; text-align: center">
        <asp:TextBox ID="num_pointe" runat="server" Width="200px" Height="30px" autofocus="autofocus" OnTextChanged="num_pointe_TextChanged"  AutoComplete="off" BorderStyle="Solid"></asp:TextBox>
    </div>
    <div style="margin-top: 50px; text-align: center">
        <asp:GridView ID="pointage_grid" Style="margin: 0 auto;" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="false"
            Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" HorizontalAlign="Center" CellPadding="3" BorderWidth="1px" ForeColor="Black" GridLines="Vertical">
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:TemplateField HeaderText="Nom et Prenom">
                    <ItemTemplate>
                        <asp:Label ID="Label_nom_prenom" runat="server" Text='<%# string.Concat(Eval("nom")," ",Eval("prenom"))%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Faculté">
                    <ItemTemplate>
                        <asp:Label ID="Label_Faculte" runat="server" Text='<%# Eval("faculte") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Departement">
                    <ItemTemplate>
                        <asp:Label ID="Label_Departement" runat="server" Text='<%# Eval("Departement") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Classe">
                    <ItemTemplate>
                        <asp:Label ID="Label_Classe" runat="server" Text='<%# Eval("Classe") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date ">
                    <ItemTemplate>
                        <asp:Label ID="Label_entre" runat="server" Text='<%# Convert.ToDateTime(Eval("date")).ToShortDateString() %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Heure d'entré">
                    <ItemTemplate>
                        <asp:Label ID="Label_sortie" runat="server" Text='<%# Eval("heure_entre") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Heure de sortie">
                    <ItemTemplate>
                        <asp:Label ID="Label_sortie" runat="server" Text='<%# Eval("heure_sortie")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
