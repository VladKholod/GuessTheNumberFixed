<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GuestTheNumber._Default" %>

<asp:Content runat="server" ID="GameContent" ContentPlaceHolderID="GameContent">

    <asp:UpdatePanel ID="RegistrationPanel" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="False">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="LogInButton"/>
            <asp:AsyncPostBackTrigger ControlID="LogOutButton"/>
        </Triggers>
        
        <ContentTemplate>
            <div class="content-wrapper clear-fix">
                
                <div ID="LogIn" runat="server">
                    
                    <asp:TextBox ID="UserNameTextBox"  runat="server" ></asp:TextBox>
                    <asp:Button ID="LogInButton" OnClick="LogInButton_OnClick" Text="Log in" runat="server" />
                
                    <br/>

                    <asp:Label ID="UserNameValidationLabel" Visible="False" CssClass="validation-error field-validation-error" for="UserNameTextBox" runat="server">
                        UserName must be filled!
                    </asp:Label>
                    
                </div>
                <div ID="LogOff" class="float-right" Visible="False" runat="server">
                    <asp:Button ID="LogOutButton" CssClass="float-right" OnClick="LogOutButton_OnClick" Text="Log out"    runat="server"/>
                    <h2>Hi, <%=Session[UserName].ToString()%></h2>
                </div> 
            </div>
            
            <div class="content-wrapper clear-fix">
                <asp:UpdatePanel ID="GameBoardPanel" Visible="False" OnPreRender="GameBoard_OnPreRender" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="MainTimer"/>
                    </Triggers>
                    
                    <ContentTemplate>
                        <asp:Label ID="GameStatusLabel" runat="server"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
                <asp:UpdatePanel ID="UIControlsPanel" Visible="False" UpdateMode="Conditional" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ThinkUpNumberButton"/>
                        <asp:AsyncPostBackTrigger ControlID="GuessNumberButton"/>
                    </Triggers>
                    
                    <ContentTemplate>
                        <div>
                            <asp:TextBox ID="ThinkUpNumberTextBox" runat="server"></asp:TextBox>
                            <asp:Button ID="ThinkUpNumberButton" OnClick="ThinkUpNumberButton_OnClick" Text="ThinkUp number" runat="server"/>
                            <br/>
                            <asp:Label ID="ThinkUpNumberValidationLabel" Visible="False" CssClass="validation-error field-validation-error" runat="server">Type number, please!</asp:Label>
                        </div>
                        
                        <div>
                            <asp:Label ID="HelperLabel" runat="server"></asp:Label>
                            <asp:TextBox ID="GuessNumberTextBox" runat="server"></asp:TextBox>
                            <asp:Button ID="GuessNumberButton" OnClick="GuessNumberButton_OnClick" Text="Guess number" runat="server"/>
                            <br/>
                            <asp:Label ID="GuessNumberValidationLabel" Visible="False" CssClass="validation-error field-validation-error" runat="server">Type number, please!</asp:Label>
                        </div>
                        
                        <div>
                            <asp:Label ID="HistoryLabel" runat="server"></asp:Label>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div> 
            
            <asp:Timer runat="server" ID="MainTimer" Interval="1000"></asp:Timer>   

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>