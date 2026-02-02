<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="GerenciamentoLivros.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoLivros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxFormLayout ID="ASPxFormLayoutPrincipal" runat="server" Width="50%" Theme="Office365">
        <Items>
            <dx:LayoutGroup Caption="Cadastro de Livro" ColCount="2" SettingsItemCaptions-Location="Top">
                <Items>
                    <%-- Campo Categoria --%>
                    <dx:LayoutItem Caption="Categoria">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox ID="cmbCadastroTipoLivro" runat="server" Width="100%" ValueField="til_id_tipo_livro" TextField="til_ds_descricao" ValueType="System.Decimal">
                                    <ValidationSettings ValidationGroup="MyGroup" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="Selecione a categoria!" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <%-- Campo ID Editor --%>
                    <dx:LayoutItem Caption="Editor">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox ID="cmbCadastroIdEditorLivro" runat="server" Width="100%" ValueField="edi_id_editor" TextField="edi_nm_nome" ValueType="System.Decimal">
                                    <ValidationSettings ValidationGroup="MyGroup" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="Selecione o editor!" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <%-- Campo Autor --%>
                    <dx:LayoutItem Caption="Autor">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox ID="cmbCadastroAutor" runat="server" Width="100%" ValueField="aut_id_autor" TextField="aut_nm_nome"  ValueType="System.Decimal">
                                    <ValidationSettings ValidationGroup="MyGroup" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="Selecione o autor!" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <%-- Campo Titulo --%>
                    <dx:LayoutItem Caption="Título">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxCadastroTituloLivro" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="Digite o título do Livro!" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <%-- Campo Preco --%>
                    <dx:LayoutItem Caption="Preço">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxCadastroPrecoLivro" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="true" ErrorText="Informe o preço do Livro!" />
                                        <dx:RegularExpression ErrorText="Preço inválido (ex: 29,90)" ValidationExpression="^\d+([,.]\d{1,2})?$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <%-- Campo Royalty --%>
                    <dx:LayoutItem Caption="Royalty (%)">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxCadastroRoyaltyLivro" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="Informe o royalty do Livro!" />
                                        <dx:RegularExpression ErrorText="Royalty inválido (0 a 100)" ValidationExpression="^100([,.]0{1,2})?$|^\d{1,2}([,.]\d{1,2})?$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <%-- Campo Edicao Livro --%>
                    <dx:LayoutItem Caption="Edição Livro">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxCadastroEdicaoLivro" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="true" ErrorText="Informe a edição do Livro!" />
                                        <dx:RegularExpression ErrorText="Edição inválida (números inteiros)" ValidationExpression="^\d+$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <%-- Campo Resumo --%>
                    <dx:LayoutItem Caption="Resumo" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxTextBox ID="tbxCadastroResumoLivro" runat="server" Width="100%" TextMode="MultiLine" Rows="5">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <%-- Botão Salvar --%>
                    <dx:LayoutItem Caption="" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton ID="btnSalvarLivro" runat="server" Text="Salvar" AutoPostBack="true" Width="100%" OnClick="BtnNovoLivro_Click" ValidationGroup="MyGroup" />
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
    <%-- GRID --%>
    <dx:ASPxGridView ID="gvGerenciamentoLivros" runat="server" Width="100%" AllowEditing="True" KeyFieldName="liv_id_livro" Theme="Office365" CssClass="gridStyle" OnRowUpdating="gvGerenciamentoLivros_RowUpdating" OnRowDeleting="gvGerenciamentoLivros_RowDeleting" OnCellEditorInitialize="gvGerenciamentoLivros_CellEditorInitialize" OnAutoFilterCellEditorInitialize="gvGerenciamentoLivros_AutoFilterCellEditorInitialize">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="liv_id_livro" VisibleIndex="0" Caption="ID Livro" ReadOnly="True" Visible="false">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataComboBoxColumn FieldName="liv_id_tipo_livro" VisibleIndex="1" Caption="Categoria" Width="100px">
                <PropertiesComboBox ValueField="til_id_tipo_livro" TextField="til_ds_descricao" ValueType="System.Decimal">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn FieldName="liv_id_editor" VisibleIndex="2" Caption="Editor" Width="100px">
                <PropertiesComboBox ValueField="edi_id_editor" TextField="edi_nm_nome" ValueType="System.Decimal">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataTextColumn FieldName="liv_nm_titulo" PropertiesTextEdit-MaxLength="50" VisibleIndex="3" Caption="Título">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="liv_vl_preco" VisibleIndex="4" Caption="Preço" Width="30px">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="liv_pc_royalty" VisibleIndex="5" Caption="Royalty (%)" Width="30px">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="liv_ds_resumo" VisibleIndex="6" Caption="Resumo">
                <PropertiesTextEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="False" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="liv_nu_edicao" VisibleIndex="7" Caption="Edição Livro" Width="30px">
            </dx:GridViewDataTextColumn>

            <dx:GridViewCommandColumn ShowEditButton="True" ShowDeleteButton="True">
            </dx:GridViewCommandColumn>
        </Columns>
        <Settings ShowFilterRow="True" ShowGroupPanel="True" />
        <SettingsEditing Mode="Batch" />
    </dx:ASPxGridView>
</asp:Content>
