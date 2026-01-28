<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="GerenciamentoEditores.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoEditores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxFormLayout ID="ASPxFormLayoutPrincipal" runat="server" Width="50%" Theme="Office365">
        <Items>
            <dx:LayoutGroup Caption="Cadastro de Editor" ColCount="2" SettingsItemCaptions-Location="Top">
                <Items>
                    <%-- Campo Nome --%>
                    <dx:LayoutItem Caption="Nome">
                        <ParentContainerStyle Paddings-PaddingRight="12"></ParentContainerStyle>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxCadastroNomeEditor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="Digite o nome do Editor!" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <%-- Campo Url --%>
                    <dx:LayoutItem Caption="URL">
                        <ParentContainerStyle Paddings-PaddingLeft="0" Paddings-PaddingRight="12"></ParentContainerStyle>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxCadastroUrlEditor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="true" ErrorText="Digite a URL do Editor!" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <%-- Campo E-Mail --%>
                    <dx:LayoutItem Caption="E-Mail" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxTextBox ID="tbxCadastroEmailEditor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="Digite o email do Editor!" />
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
                                <dx:ASPxButton ID="btnSalvar" runat="server" Text="Salvar" AutoPostBack="true" Width="100%" OnClick="BtnNovoEditor_Click" ValidationGroup="MyGroup" />
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
    <dx:ASPxGridView ID="gvGerenciamentoEditores" runat="server" Width="100%" AllowEditing="True" KeyFieldName="edi_id_editor" Theme="Office365" CssClass="gridStyle" OnRowUpdating="gvGerenciamentoEditores_RowUpdating" OnRowDeleting="gvGerenciamentoEditores_RowDeleting" OnCustomButtonCallback="gvGerenciamentoEditores_CustomButtonCallback">
        <ClientSideEvents EndCallback="OnEndCallback" />
        <Columns>
            <dx:GridViewDataTextColumn FieldName="edi_id_editor" Caption="Id" Visible="false" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="15" FieldName="edi_nm_nome" Caption="Nome" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="50" FieldName="edi_ds_email" Caption="Email" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="50" FieldName="edi_ds_url" Caption="URL" />

            <dx:GridViewCommandColumn ShowEditButton="True" ShowDeleteButton="True">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="btnLivros" Text="Livros"/>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
        <Settings ShowFilterRow="False" ShowGroupPanel="True" />
        <SettingsEditing Mode="Batch" />
    </dx:ASPxGridView>
</asp:Content>
