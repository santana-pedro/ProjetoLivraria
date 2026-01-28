<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="GerenciamentoAutores.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoAutores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxFormLayout ID="ASPxFormLayoutPrincipal" runat="server" Width="50%" Theme="Office365">
        <Items>
            <dx:LayoutGroup Caption="Cadastro de Autor" ColCount="2" SettingsItemCaptions-Location="Top">
                <Items>
                    <%-- Campo Nome --%>
                    <dx:LayoutItem Caption="Nome">
                        <ParentContainerStyle Paddings-PaddingRight="12"></ParentContainerStyle>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxCadastroNomeAutor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="Digite o nome do Autor!" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <%-- Campo Sobrenome --%>
                    <dx:LayoutItem Caption="Sobrenome">
                        <ParentContainerStyle Paddings-PaddingLeft="0" Paddings-PaddingRight="12"></ParentContainerStyle>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxCadastroSobrenomeAutor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="true" ErrorText="Digite o sobrenome do Autor!" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <%-- Campo E-Mail --%>
                    <dx:LayoutItem Caption="E-Mail" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxTextBox ID="tbxCadastroEmailAutor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="Digite o email do Autor!" />
                                        <RegularExpression ErrorText="Email inválido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <%-- Botão Salvar --%>
                    <dx:LayoutItem Caption="" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton ID="btnSalvar" runat="server" Text="Salvar" AutoPostBack="true" Width="100%" OnClick="BtnNovoAutor_Click" ValidationGroup="MyGroup" />
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
    <dx:ASPxGridView ID="gvGerenciamentoAutores" runat="server" ShowInsert="True" AllowEditing="True" Width="100%" KeyFieldName="aut_id_autor"
        OnRowUpdating="gvGerenciamentoAutores_RowUpdating"
        OnRowDeleting="gvGerenciamentoAutores_RowDeleting"
        OnCustomButtonCallback="gvGerenciamentoAutores_CustomButtonCallback">
        <ClientSideEvents EndCallback="OnEndCallback" />

        <Columns>
            <dx:GridViewDataTextColumn FieldName="aut_id_autor" Caption="Id" Visible="false" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="15" FieldName="aut_nm_nome" Caption="Nome" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="50" FieldName="aut_nm_sobrenome" Caption="Sobrenome" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="50" FieldName="aut_ds_email" Caption="Email" />

            <dx:GridViewCommandColumn ShowEditButton="True" ShowDeleteButton="True">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="btnLivros" Text="Livros"/>
                    <dx:GridViewCommandColumnCustomButton ID="btnAutorInfo" Text="Informação"/>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
        <SettingsEditing Mode="Batch" />
    </dx:ASPxGridView>
</asp:Content>