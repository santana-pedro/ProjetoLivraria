<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="GerenciamentoCategorias.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoCategorias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxFormLayout ID="ASPxFormLayoutPrincipal" runat="server" Width="50%" Theme="Office365">
        <Items>
            <dx:LayoutGroup Caption="Cadastro de Autor" ColCount="2" SettingsItemCaptions-Location="Top">
                <Items>
                    <%-- Campo Descrição --%>
                    <dx:LayoutItem Caption="Descricao">
                        <ParentContainerStyle Paddings-PaddingRight="12"></ParentContainerStyle>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxCadastroDescricaoCategoria" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="Digite a descricao da categoria!" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <%-- Botão Salvar --%>
                    <dx:LayoutItem Caption="" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton ID="btnSalvar" runat="server" Text="Salvar" AutoPostBack="true" Width="100%" OnClick="BtnNovaCategoria_Click" ValidationGroup="MyGroup" />
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
    <script type:"text/javascript">
    function OnEndCallback(s, e) {
        if (s.cpRedirectionToLivros) {
            delete s.cpRedirectionToLivros;
            window.location.href = '/Livraria/GerenciamentoLivros.aspx'
        }
    }
    </script>
    <dx:ASPxGridView ID="gvGerenciamentoCategorias" runat="server" ShowInsert="True" AllowEditing="True" Width="100%" KeyFieldName="til_id_tipo_livro" OnRowUpdating="gvGerenciamentoCategorias_RowUpdating" OnRowDeleting="gvGerenciamentoCategorias_RowDeleting" OnCustomButtonCallback="gvGerenciamentoCategorias_CustomButtonCallback">
        <ClientSideEvents EndCallback="OnEndCallback" />
        <Columns>
            <dx:GridViewDataTextColumn FieldName="til_id_tipo_livro" Caption="Id" Visible="false" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="15" FieldName="til_ds_descricao" Caption="Nome" />

            <dx:GridViewCommandColumn ShowEditButton="True" ShowDeleteButton="True">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="btnLivros" Text="Livros"/>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
        <SettingsEditing Mode="Batch" />
    </dx:ASPxGridView>
</asp:Content>
