<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="GoogleNewsApp.UI.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RSS feed of Google News</title>
    <!-- Include jQuery library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <!-- Include custom JavaScript file -->
    <script src="Scripts/Custom.js"></script>
    <link rel="stylesheet" type="text/css" href="CSS/Styles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <h1>Google News</h1>
        <div id="formContent">
            <article id="newsTopics" class="content">
                <h2>Topics</h2>
                <asp:Repeater ID="rptNews" runat="server">
                    <ItemTemplate>
                        <div class="news-topic" data-link='<%# Eval("Link") %>'>
                            <a id="topic" runat="server" href='<%# Eval("Link") %>' onclick="topicClicked(event)"><%# Eval("Title") %></a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </article>
            <article id="postContainer" runat="server" class="content">
            </article>
            <asp:Label ID="lblCache" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
